using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Data.Entity;
using RouteVisualizer.Views;
using RouteVisualizer.ViewModels;
using RouteVisualizer.EF;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;
using SensorViewLibrary.ViewModels;

using RouteVisualizer.Models;
using Tus.Communication;
namespace RouteVisualizer.RailEditor.ViewModels
{
    public class RailDataViewModel
        : ModeledViewModel<RailData>
    {
        /*コマンド、プロパティの定義にはそれぞれ 
         * 
         *  ldcom   : DelegateCommand(パラメータ無)
         *  ldcomn  : DelegateCommand(パラメータ無・CanExecute無)
         *  ldcomp  : DelegateCommand(型パラメータ有)
         *  ldcompn : DelegateCommand(型パラメータ有・CanExecute無)
         *  lprop   : 変更通知プロパティ
         *  
         * を使用してください。
         */

        /*ViewModelからViewを操作したい場合は、
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信してください。
         */

        /*
         * UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         */

        /*
         * Modelからの変更通知などの各種イベントをそのままViewModelで購読する事はメモリリークの
         * 原因となりやすく推奨できません。ViewModelHelperの各静的メソッドの利用を検討してください。
         */

        public ObservableCollection<PathDataViewModel> pathvms { get; private set; }
        public ObservableCollection<GateDataViewModel> gates { get; private set; }


        public RailDataViewModel()
            : base()
        {
            pathvms = new ObservableCollection<PathDataViewModel>();
            gates = new ObservableCollection<GateDataViewModel>();

            this.pathvms.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(pathvms_CollectionChanged);
            this.ModelChanged += this.model_Changed;
        }

        void pathvms_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var vm in e.NewItems.Cast<PathDataViewModel>())
                {
                    if (vm.Model == null)
                    {
                        vm.Model = new PathData()
                        {
                            RailID = this.Model.ID,
                        };
                    }
                    vm.AvailableGates = this.gates;
                    vm.ParentRail = this;

                    var ps = this.Model.Pathes;
                    if (!ps.Contains(vm.Model))
                        this.Model.Pathes.Add(vm.Model);
                }
            }
            if (e.OldItems != null)
            {
                foreach (var vm in e.OldItems.Cast<PathDataViewModel>())
                {
                    var ps = this.Model.Pathes;
                    if (ps.Contains(vm.Model))
                        this.Model.Pathes.Remove(vm.Model);
                }
            }
        }

        private void model_Changed(object sender, ModelChangedArgs<RailData> e)
        {
            if (e.current != null)
            {
                this.pathvms.Clear();
                this.gates.Clear();

                if (e.current.Pathes == null)
                {
                    e.current.Pathes = new List<PathData>();
                }

                if (e.current.Gates == null)
                {
                    e.current.Gates = new List<GateData>();
                }

                foreach (var p in e.current.Pathes)
                {
                    var pvm = new PathDataViewModel()
                    {
                        Model = p,
                        ParentRail = this,
                        AvailableGates = this.gates,
                    };

                    if (!this.gates.Any(vm => vm.Model == p.GateStart))
                        this.gates.Add(new GateDataViewModel() { Model = p.GateStart });

                    if (!this.gates.Any(vm => vm.Model == p.GateEnd))
                        this.gates.Add(new GateDataViewModel() { Model = p.GateEnd });

                    this.pathvms.Add(pvm);
                }

            }
        }

        #region PathDataViewWndOpenCommand
        DelegateCommand<PathData> _PathDataViewWndOpenCommand;

        public DelegateCommand<PathData> PathDataViewWndOpenCommand
        {
            get
            {
                if (_PathDataViewWndOpenCommand == null)
                    _PathDataViewWndOpenCommand = new DelegateCommand<PathData>(PathDataViewWndOpen, CanPathDataViewWndOpen);
                return _PathDataViewWndOpenCommand;
            }
        }

        private bool CanPathDataViewWndOpen(PathData parameter)
        {
            return parameter != null;
        }

        private void PathDataViewWndOpen(PathData parameter)
        {
            var wnd = new SettingWindow();
            var pathvw = new PathDataView();
            var cache = pathvw.ToByteArray();

            pathvw.DataContext = parameter;

            wnd.Title = string.Format("Path:{0}, owned by {1}", parameter.ID, parameter.RailID);
            wnd.Content = pathvw;

            var res = wnd.ShowDialog();
            if (!res.HasValue || !res.Value)
            {
                parameter.RestoreObject(cache);
            }
        }

        #endregion


        #region RemovePathCommand
        DelegateCommand<PathDataViewModel> _RemovePathCommand;

        public DelegateCommand<PathDataViewModel> RemovePathCommand
        {
            get
            {
                if (_RemovePathCommand == null)
                    _RemovePathCommand = new DelegateCommand<PathDataViewModel>(RemovePath, CanRemovePath);
                return _RemovePathCommand;
            }
        }

        private bool CanRemovePath(PathDataViewModel parameter)
        {
            return parameter != null && this.pathvms.Contains(parameter);
        }

        private void RemovePath(PathDataViewModel parameter)
        {
            this.pathvms.Remove(parameter);
        }
        #endregion

    }
}
