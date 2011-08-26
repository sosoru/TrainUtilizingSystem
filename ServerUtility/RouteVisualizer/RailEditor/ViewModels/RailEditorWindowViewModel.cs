using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
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
    public class RailEditorWindowViewModel
        : RailEditorViewModel
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
        public RailEditorWindowViewModel()
        {
            this.modeling = new ModelingDatabase("rail.sdf");
        }

        public class RailEditorViewNode
            : ViewModel
        {

            bool _IsExpanded;

            public bool IsExpanded
            {
                get
                { return _IsExpanded; }
                set
                {
                    if (_IsExpanded == value)
                        return;
                    _IsExpanded = value;
                    RaisePropertyChanged("IsExpanded");
                }
            }

            bool _IsSelected;

            public bool IsSelected
            {
                get
                { return _IsSelected; }
                set
                {
                    if (_IsSelected == value)
                        return;
                    _IsSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }



            public string Header { get; set; }
            public IList<RailEditorViewNode> Children { get; set; }
            public RailEditorWindowViewModel Parent { get; set; }

            public RailDataViewModel ViewModel { get; set; }
        }

        private IList<RailEditorViewNode> _previousNodes;
        public IList<RailEditorViewNode> Nodes
        {
            get
            {

                var list = this.ObservableRailDatas.GroupBy((r) => r.Manifacturer)
                                                .Select((g) => new RailEditorViewNode()
                                                        {
                                                            Parent = this,
                                                            Header = g.Key,
                                                            Children = g.Select((r) => new RailEditorViewNode()
                                                                {
                                                                    Parent = this,
                                                                    Header = r.RailName,
                                                                    ViewModel = new RailDataViewModel() { Model = r, },
                                                                }).ToList(),
                                                        })
                                                .ToList();

                if (this._previousNodes != null)
                {
                    foreach (var previousitem in this._previousNodes)
                    {
                        var befind = list.FindIndex((r) => r.Header == previousitem.Header);
                        if (befind >= 0)
                        {
                            var curitem = list [befind];
                            curitem.IsExpanded = previousitem.IsExpanded;
                            curitem.IsSelected = previousitem.IsSelected;
                        }
                    }
                }

                this._previousNodes = list;
                return list;
            }
        }


        RailDataViewModel _SelectedNode;

        public RailDataViewModel SelectedNode
        {
            get
            { return _SelectedNode; }
            set
            {
                if (_SelectedNode == value)
                    return;
                _SelectedNode = value;
                RaisePropertyChanged("SelectedNode");
            }
        }

    }
}
