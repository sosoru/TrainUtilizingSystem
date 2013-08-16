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

using RouteVisualizer.Models;
using SensorViewLibrary.ViewModels;
namespace RouteVisualizer.RailEditor.ViewModels
{
    public class PathDataViewModel
        : ModeledViewModel<PathData>
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

        public PathDataViewModel()
            : base()
        {
            this.AvailableGates = new ObservableCollection<GateDataViewModel>();
        }

        public RailDataViewModel ParentRail { get; set; }

        public bool IsStraight
        {
            get
            {
                if (this.Model != null)
                    return this.Model.IsStraight;
                else
                    return false;
            }
            set
            {
                if (this.Model != null)
                {
                    this.Model.IsStraight = value;
                    RaisePropertyChanged("IsStraight");
                }
            }
        }

        public bool IsCurved
        {
            get
            {
                if (this.Model != null)
                    return !this.Model.IsStraight;
                else
                    return true;
            }
            set
            {
                if (this.Model != null)
                {
                    this.Model.IsStraight = !value;
                    RaisePropertyChanged("IsCurved");
                }
            }
        }

        private GateDataViewModel _cache_gateStart;
        public GateDataViewModel GateStart
        {
            get
            {
                if (this.Model != null)
                {
                    if (_cache_gateStart == null)
                        _cache_gateStart = new GateDataViewModel();

                    _cache_gateStart.Model = this.Model.GateStart;

                    return this._cache_gateStart;
                }
                else
                    return new GateDataViewModel();
            }
            set
            {
                if (this.Model != null)
                {
                    if (value == null)
                        this.Model.GateStart = null;
                    else
                    {
                        this._cache_gateStart = value;
                        this.Model.GateStart = value.Model;
                    }
                    //RaisePropertyChanged("GateStart");
                    RaisePropertyChanged("");
                }
            }
        }

        private GateDataViewModel _cache_gateEnd;
        public GateDataViewModel GateEnd
        {
            get
            {
                if (this.Model != null)
                {
                    if (_cache_gateEnd == null)
                        this._cache_gateEnd = new GateDataViewModel();

                    this._cache_gateEnd.Model = this.Model.GateEnd;

                    return this._cache_gateEnd;
                }
                else
                    return new GateDataViewModel();

            }
            set
            {
                if (this.Model != null)
                {
                    if (value == null)
                        this.Model.GateEnd = null;
                    else
                    {
                        this._cache_gateEnd = value;
                        this.Model.GateEnd = value.Model;
                    }
                    //RaisePropertyChanged("GateEnd");
                    RaisePropertyChanged("");
                }
            }
        }

        public double StraightLength
        {
            get
            {
                if (this.Model != null)
                {
                    return this.Model.Length;
                }
                else
                    return double.NaN;
            }
            set
            {
                if (this.Model != null)
                {
                    this.Model.Length = value;
                    RaisePropertyChanged("Length");
                }
            }
        }

        public double Radius
        {
            get
            {
                if (this.Model != null)
                {
                    return this.Model.Length;
                }
                else
                    return double.NaN;
            }
            set
            {
                if (this.Model != null)
                {
                    this.Model.Length = value;
                    RaisePropertyChanged("ViewRadius");
                }
            }
        }

        public double Angle
        {
            get
            {
                if (this.Model != null)
                {
                    return this.Model.Angle;
                }
                else
                    return double.NaN;
            }
            set
            {
                if (this.Model != null)
                {
                    this.Model.EndAngle = value + this.Model.StartAngle;
                    RaisePropertyChanged("Angle");
                }
            }
        }

        public ObservableCollection<GateDataViewModel> AvailableGates { get; set; }


    }
}
