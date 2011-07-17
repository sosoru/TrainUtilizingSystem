using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using SensorLivetView.Models;
using SensorLibrary;

namespace SensorLivetView.ViewModels.Controls
{
    public class VoltageGraphViewModel : ViewModel
    {
        public IGraphPainter Painter { get; set; }
                
        
    }
}
