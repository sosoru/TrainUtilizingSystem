using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using RouteVisualizer;
using RouteVisualizer.EF;

using Livet;

namespace RouteVisualizer.Models
{
    public class GateModel
        : Model, IGate 
    {
        private GateData BaseData;
        public GateModel(GateData data)
        {
            this.BaseData = data;

            this._connectedPathes = new List<IPath>();
            this.Position = new Point(data.Position.First(), data.Position.Last());
        }

        private IList<IPath> _connectedPathes;
        public IList<IPath> ConnectedPathes
        {
            get { return this._connectedPathes; }
        }

        public Point Position
        {
            get;
            private set;
        }


        string _Name;

        public string Name
        {
            get
            { return _Name; }
            set
            {
                if (_Name == value)
                    return;
                _Name = value;
                RaisePropertyChanged("Name");
            }
        }
      
        
        public override string ToString()
        {
            return string.Format("Name : {0}", this.BaseData.GateName);
        }
    }
}
