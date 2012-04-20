using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
namespace LightSwitchApplication
{
    public partial class EagleBomPartsListDetail
    {
        partial void EagleBomPartsListDetail_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Write your code here.
            saveChangesTo.Add(this.DataWorkspace.ApplicationData);
            saveChangesTo.Add(this.DataWorkspace.WCF_RIA_ServiceData);
        }

        partial void EagleBomPartsListDetail_Saving(ref bool handled)
        {
            // Write your code here.
            this.DataWorkspace.ApplicationData.SaveChanges();
            this.DataWorkspace.WCF_RIA_ServiceData.SaveChanges();

            handled = true;
        }

    }
}
