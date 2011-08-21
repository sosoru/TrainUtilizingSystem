using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using System.Data.Entity;
using RouteVisualizer.EF;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using RouteVisualizer.Models;

namespace RouteVisualizer.RailEditor.ViewModels
{
    public class RailEditorViewModel
        : ViewModel
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

        public ModelingDatabase modeling { get;set;}

        public RailEditorViewModel()
        {
        }

        #region RemoveRailCommand
        DelegateCommand<RailData> _RemoveRailCommand;

        public DelegateCommand<RailData> RemoveRailCommand
        {
            get
            {
                if (_RemoveRailCommand == null)
                    _RemoveRailCommand = new DelegateCommand<RailData>(RemoveRail, CanRemoveRail);
                return _RemoveRailCommand;
            }
        }

        private bool CanRemoveRail(RailData parameter)
        {
            return parameter != null && this.modeling.Rails.Any((r) => r.ID == parameter.ID);
        }

        private void RemoveRail(RailData parameter)
        {
            this.modeling.Rails.Remove(parameter);
            this.modeling.SaveChanges();
        }
        #endregion


        #region AddRailCommand
        DelegateCommand<RailData> _AddRailCommand;

        public DelegateCommand<RailData> AddRailCommand
        {
            get
            {
                if (_AddRailCommand == null)
                    _AddRailCommand = new DelegateCommand<RailData>(AddRail, CanAddRail);
                return _AddRailCommand;
            }
        }

        private bool CanAddRail(RailData parameter)
        {
            return parameter != null && !this.modeling.Rails.Any(r => r.ID == parameter.ID);
        }

        private void AddRail(RailData parameter)
        {
            this.modeling.Rails.Add(parameter);
            this.modeling.SaveChanges();
        }
        #endregion
      


        #region SaveCommand
        DelegateCommand _SaveCommand;

        public DelegateCommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                    _SaveCommand = new DelegateCommand(Save);
                return _SaveCommand;
            }
        }

        private void Save()
        {
            this.modeling.SaveChanges();
        }
        #endregion


        #region RemovePathCommand
        DelegateCommand<PathData> _RemovePathCommand;

        public DelegateCommand<PathData> RemovePathCommand
        {
            get
            {
                if (_RemovePathCommand == null)
                    _RemovePathCommand = new DelegateCommand<PathData>(RemovePath, CanRemovePath);
                return _RemovePathCommand;
            }
        }

        private bool CanRemovePath(PathData parameter)
        {
            return parameter != null && this.modeling.Pathes.Any(p => p.ID == parameter.ID);
        }

        private void RemovePath(PathData parameter)
        {
            this.modeling.Pathes.Remove(parameter);
            this.modeling.SaveChanges();
        }
        #endregion


        #region AddPathCommand
        DelegateCommand<PathData> _AddPathCommand;

        public DelegateCommand<PathData> AddPathCommand
        {
            get
            {
                if (_AddPathCommand == null)
                    _AddPathCommand = new DelegateCommand<PathData>(AddPath, CanAddPath);
                return _AddPathCommand;
            }
        }

        private bool CanAddPath(PathData parameter)
        {
            return parameter != null && !this.modeling.Pathes.Any(p => p.ID == parameter.ID);
        }

        private void AddPath(PathData parameter)
        {
            this.modeling.Pathes.Add(parameter);
            this.modeling.SaveChanges();
        }
        #endregion
      

    }
}
