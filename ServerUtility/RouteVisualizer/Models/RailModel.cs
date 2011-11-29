using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using RouteVisualizer;
using RouteVisualizer.EF;
using System.Reactive.Linq;

using Livet;

namespace RouteVisualizer.Models
{
    public class RailModel
        : Model
    {
        public RailModel(RailData data)
        {
            this._baseData = data;

            this.Connections = new ObservableCollection<GateModel>(this._baseData.Gates.Select((g) => new GateModel(g)).ToList());

            var pathes = this._baseData.Pathes.Select((p) => new PathModel()
            {
                BaseData = p,
                OwnerRail = this,
                PreviousGate = this.Connections.First((conn) => conn.BaseData.ID == p.GateStart.ID),
                NextGate = this.Connections.First(conn => conn.BaseData.ID == p.GateEnd.ID),
            }).ToList();

            this.Pathes = new ObservableCollection<PathModel>(pathes);
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
      
        public bool IsPathValidated
        {
            get
            {
                try
                {
                    LocateGate();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public IDictionary<IGate, Point> LocateGate()
        {
            if (this.Pathes == null || this.Pathes.Count == 0)
                return new Dictionary<IGate, Point>();

            var dict = new Dictionary<IGate, Point>();
            foreach (var conn in this.Connections)
                dict.Add(conn, conn.BasePosition);

            //check isolated gate
            if (this.Connections.Any((conn) => conn.ConnectedPathes.Count == 0))
                throw new InvalidOperationException("isolated gate found");

            foreach (var path in this.Pathes)
            {
                var sentvec = path.Bound.BottomLeft - path.Bound.TopRight;

                var basepoint = dict [path.PreviousGate];

                // basepoint += sentvec;

                //check overwrite
                //var zero = new Point();
                //if (dict [path.NextGate] != zero && dict [path.NextGate] != sentvec)
                //{
                //    throw new InvalidOperationException(string.Format("gate position mismatching : {0}", path.NextGate.ToString()));
                //}

                dict [path.NextGate] = basepoint + sentvec;
            }

            return dict;
        }

    }

}
