using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Data.Entity;

using Livet;
using RouteVisualizer.Properties;
using RouteVisualizer.EF;

namespace RouteVisualizer
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherHelper.UIDispatcher = Dispatcher;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            //todo: avoid global setting
            Database.DefaultConnectionFactory = new System.Data.Entity.Infrastructure.SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ModelingDatabase>());
        }

        //集約エラーハンドラ
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //TODO:ロギング処理など
#if !DEBUG
            MessageBox.Show(
                "不明なエラーが発生しました。アプリケーションを終了します。",
                "エラー",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            Environment.Exit(1);
#endif
        }

        internal static Settings Settings
        {
            get { return Settings; }
        }
    }
}
