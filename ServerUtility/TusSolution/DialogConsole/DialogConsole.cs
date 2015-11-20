using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DialogConsole.Features.Base;
using DialogConsole.Properties;
using DialogConsole.WebPages;
using Tus.Communication;
using Tus.Communication.Device.AvrComposed;
using Tus.Factory;
using Tus.TransControl.Base;

namespace DialogConsole
{
    internal class MainClass
    {        //        BOOL MoveWindow(
        //            HWND hWnd,      // ウィンドウのハンドル
        //            int X,          // 横方向の位置
        //            int Y,          // 縦方向の位置
        //            int nWidth,     // 幅
        //            int nHeight,    // 高さ
        //            BOOL bRepaint   // 再描画オプション
        //            );
        [DllImport("User32.dll")]
        static extern int MoveWindow(
            IntPtr hWnd,
            int x,
            int y,
            int nWidth,
            int nHeight,
            int bRepaint
            );

        private static void Main(string[] args)
        {
            // DIコンテナの初期化
            var container = new DialogConsoleContainer();
            Lazy<DialogConsoleClass> dialog = container.GetExport<DialogConsoleClass>();

            // 画面サイズの取得
            var scrsize = Screen.PrimaryScreen.Bounds;
            scrsize.Height = scrsize.Height - 30;

            // コンソールウィンドウのサイズを指定
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.WindowHeight = Console.LargestWindowHeight;
            MoveWindow(Process.GetCurrentProcess().MainWindowHandle, 0, 0, scrsize.Width/2, scrsize.Height, 1);

            // コマンドループの開始
            dialog.Value.Loop();
        }
    }

    /// <summary>
    ///    DialogConsoleで使用するDIコンテナの定義
    /// </summary>
    [Export(typeof(CompositionContainer))]
    internal class DialogConsoleContainer
        : CompositionContainer
    {
        public DialogConsoleContainer()
            : base(catalogs)
        {
        }

        /// <summary>
        /// 使用するカタログの定義
        /// </summary>
        private static AggregateCatalog catalogs
        {
            get
            {
                var catalog = new AggregateCatalog();

                // 通信・閉塞系のファクトリはTus.Factoryに定義
                catalog.Catalogs.Add(new AssemblyCatalog(".\\Tus.Factory.dll"));
                // DialogConsoleでもFeature等はDIで作るので追加
                catalog.Catalogs.Add((new AssemblyCatalog(Assembly.GetExecutingAssembly())));

                return catalog;
            }
        }
    }

    /// <summary>
    /// Featureで使うな要素を格納する
    /// </summary>
    [Export(typeof(IFeatureParameters))]
    internal class DialogConsoleParameters
        : IFeatureParameters
    {
        /// <summary>
        /// 初期化時にLayoutを生成します
        /// </summary>
        /// <param name="fact">BlockSheetのファクトリ</param>
        /// <param name="rtfact"></param>
        /// <param name="ilfact"></param>
        [ImportingConstructor]
        public DialogConsoleParameters(SheetFactory fact, RouteOrderListFactory rtfact, IlluminativeSheetFactory ilfact)
        {
            UsingLayout = new Layout(rtfact, fact.Create(), ilfact.Create());
            rtfact.Sheet = UsingLayout.Sheet;
            LockObjectPacketProcessing = new object();

            UsingLayout.Vehicles = new List<Vehicle>();
        }

        public ConcurrentQueue<IConsolePage> WebRequestsQueue { get; set; }
        public object LockObjectPacketProcessing { get; set; }

        public Layout UsingLayout { get; set; }

        public IDisposable ServingInfomation { get; set; }
        public IObservable<Unit> VehiclePipeline { get; set; }
        public IObservable<Unit> SyncDevicePipeline { get; set; }
        public IDisposable VehicleProcessing { get; set; }
        public IDisposable SyncDeviceProcessing { get; set; }
        public IObservable<Unit> SendingPacketPipeline { get; set; }
        public IObservable<DevicePacket> ReceivingPacketPipeline { get; set; }

        [ImportMany]
        public IEnumerable<Lazy<IConsolePage, ITusPageMetadata>> Pages { get; set; }

        public IScheduler SchedulerPacketProcessing { get; set; }
    }

    [Export]
    public class DialogConsoleClass
        : IPartImportsSatisfiedNotification
    {
        private readonly object lock_writer = new object();
        private StreamWriter _logWriter;
        private StreamWriter _logWriterRecv;
        private IDisposable _receiving;
        private SynchronizationContext _syncPacketProcess;

        [Import]
        public IFeatureParameters Param { get; set; }

        [ImportMany]
        public IEnumerable<Lazy<IFeature, IFeatureMetadata>> Features { get; set; }

        private StreamWriter LogWriterSend
        {
            get
            {
                lock (lock_writer)
                {
                    return _logWriter;
                }
            }
            set
            {
                lock (lock_writer)
                {
                    _logWriter = value;
                }
            }
        }

        public void OnImportsSatisfied()
        {
            Features = Features.OrderBy(f => f.Metadata.FeatureExpression).ToList();
        }

        public void Loop()
        {
            // ----通信系のスレッドの実行-------------------------------------------------

            // PacketServerにストアされているパケットを送信する．
            Param.SendingPacketPipeline =
                Observable.Defer(() => Observable.Start(Param.UsingLayout.Sheet.Server.SendAll));
            Param.SendingPacketPipeline
                 .Repeat().Delay(TimeSpan.FromMilliseconds(Settings.Default.ContactIntervalToDevicesMillisecond))
                 .Subscribe();

            // LED系デバイスの更新．1秒おきに同期
            Observable.Timer(DateTimeOffset.MinValue, TimeSpan.FromMilliseconds(1000))
                      .SelectMany(Observable.Defer(() => Observable.Start(Param.UsingLayout.Illumination.SyncLedDuty)))
                      .Subscribe();

            // デバイスからのパケットを受信し，PacketServerにストア．20ミリ秒ごとに確認．
            IObservable<long> timer = Observable.Interval(TimeSpan.FromMilliseconds(20), Scheduler.Default);
            Param.ReceivingPacketPipeline = Observable.Defer(() => Param.UsingLayout.Sheet.Server.ReceivingObservable)
                                                      .Repeat()
                                                      .SubscribeOn(Scheduler.NewThread)
                                                      .Zip(timer, (v, _) => v);
            Param.ReceivingPacketPipeline.SelectMany(v => v.ExtractPackedPacket())
                 .Subscribe();
            // ------------------------------------------------------------------------------

            // レイアウトファイルに記載された，全てのMotorの状態をNoEffectに初期化する
            Param.UsingLayout.Sheet.Effect(new CommandFactory
                                               {
                                                   CreateCommand =
                                                       b => new CommandInfo { MotorMode = MotorMemoryStateEnum.NoEffect, },
                                               },
                                           Param.UsingLayout.Sheet.InnerBlocks);

            // Featureクラスの初期化
            foreach (var f in Features)
                f.Value.Init();

            // コマンドループ
            while (true)
            {
                // コマンドを受け付けるFeatureを列挙して，ボタンと名前を表示
                // ex) 5 - monitoring vehicles
                foreach (var f in Features.Where(f => f.Metadata.IsShown))
                    Console.WriteLine("{0} - {1}", f.Metadata.FeatureExpression, f.Metadata.FeatureName);

                // 改行してコマンド待ち
                Console.WriteLine();
                string cmd = Console.ReadLine();

                try
                {
                    // 入力されたコマンドに一致するFeatureを探す．最初に一致するFeatureを実行
                    Lazy<IFeature, IFeatureMetadata> feature =
                        Features.FirstOrDefault(f => f.Metadata.FeatureExpression == cmd);

                    // 見つからなければ抜ける
                    if (feature == default(IFeature))
                        Console.WriteLine("parse error");
                    else
                    {
                        // 一致したFeatureの実行
                        feature.Value.Execute();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public RouteOrder InputRouteOrder(BlockSheet sht, RouteOrder before)
        {
            Console.WriteLine("route?");
            string content = Console.ReadLine();

            if (content == "")
                return before;

            try
            {
                string[] spil = content.Split(',');
                var rt = new RouteOrder(sht, spil.Select(s => sht.GetBlock(s.Trim()).Name));

                return rt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return before;
            }
        }
    }
}