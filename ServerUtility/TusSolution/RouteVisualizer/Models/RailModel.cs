using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using RouteVisualizer;
using RouteVisualizer.EF;
using Livet;

namespace RouteVisualizer.Models
{
    public class RailModel
        : Model, IEquatable<RailModel>
    {
        public RailModel(RailData data)
        {
            this._baseData = data;

            this.Connections = new ObservableCollection<GateModel>(this._baseData.Gates.Select((g) => new GateModel(g)).ToList());
            
            this.Pathes = new ObservableCollection<PathModel>(this._baseData.Pathes.Select((p) => new PathModel(p)).ToList());
        }

        private RailData _baseData;
        public RailData BaseData
        {
            get
            {
                return this._baseData;
            }
        }

        public ObservableCollection<PathModel> Pathes { get; private set; }

        public ObservableCollection<GateModel> Connections { get; private set; }

        bool _IsMirrored;

        public bool IsMirrored
        {
            get
            { return _IsMirrored; }
            set
            {
                if (_IsMirrored == value)
                    return;
                _IsMirrored = value;
                RaisePropertyChanged("IsMirrored");
            }
        }
        #region implementation of IEqualable
        public static bool operator ==(RailModel A, RailModel B)
        {
            if (ReferenceEquals(A, B))
                return true;
            else if ((object)A == null || (object)B == null)
                return false;
            else
                return (A.BaseData.ID == B.BaseData.ID);
        }
        public static bool operator !=(RailModel A, RailModel B) { return !(A == B); }

        public bool Equals(RailModel other)
        {
            return (this == other);
        }

        public override int GetHashCode()
        {
            return this._baseData.ID.GetHashCode() ^ this._baseData.ID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this == (RailModel)obj;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        #endregion


    }

}
