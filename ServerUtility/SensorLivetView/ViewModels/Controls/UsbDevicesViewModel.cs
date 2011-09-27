using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;

using LibUsbDotNet;
using LibUsbDotNet.Main;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using SensorLivetView.Models;

namespace SensorLivetView.ViewModels.Controls
{
    public class UsbDevicesViewModel : ViewModel
    {
        public UsbDevicesViewModel()
        {
        }

        #region properties

        int _VenderID;
        public int VenderID
        {
            get
            { return _VenderID; }
            set
            {
                if (_VenderID == value)
                    return;
                _VenderID = value;
                RaisePropertyChanged("");
            }
        }

        int _ProductID;
        public int ProductID
        {
            get
            { return _ProductID; }
            set
            {
                if (_ProductID == value)
                    return;
                _ProductID = value;
                RaisePropertyChanged("");
            }
        }

        public IEnumerable<UsbRegistry> DeviceCandicates
        {
            get
            {
                var cands = UsbDevice.AllDevices.FindAll(new UsbDeviceFinder(this.VenderID, this.ProductID))
                                            .ToArray();
                return cands;
            }
        }

        #region RefreshCommand
        DelegateCommand _RefreshCommand;

        public DelegateCommand RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                    _RefreshCommand = new DelegateCommand(Refresh, CanRefresh);
                return _RefreshCommand;
            }
        }

        private bool CanRefresh()
        {
            return true;
        }

        private void Refresh()
        {
            RaisePropertyChanged("DeviceCandicates");
        }
        #endregion

        UsbRegistry _SelectedDeviceReg;
        public UsbRegistry SelectedDeviceReg
        {
            get
            { return _SelectedDeviceReg; }
            set
            {
                if (_SelectedDeviceReg == value)
                    return;
                _SelectedDeviceReg = value;
                RaisePropertyChanged("SelectedDeviceReg");
            }
        }

        #endregion
    }

    public class UsbRegistryConverter
        : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            try
            {
                var res = (value as UsbRegistry).SymbolicName;
                return res;
            }
            catch (NullReferenceException) { }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            
            try
            {
                var res = UsbDevice.AllDevices.Where((dev) => dev.SymbolicName == value as string).Single();
                return res;
            }
            catch (NullReferenceException) { }
            catch (InvalidOperationException) { }
            return null;
        }
    }
}
