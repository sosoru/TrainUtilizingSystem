using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Data;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using SensorLivetView.Models;

namespace SensorLivetView.ViewModels.Converters
{
    public class AlphabetValueConverter
        : IValueConverter
    {
        static string basestring = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int v;
            try
            {
                v = (int)value;
            }
            catch { return char.MinValue; }

            if (v > basestring.Length)
                return '?';
            else
                return basestring [v];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is string) || !(value is char))
                return -1;

            var ind = basestring.IndexOf((char)value);
            if (ind < 0)
                return -1;
            else
                return basestring [ind];
        }
    }
}
