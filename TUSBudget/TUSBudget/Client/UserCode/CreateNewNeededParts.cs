using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;

using System.Windows.Controls;

namespace LightSwitchApplication
{
    public partial class CreateNewNeededParts
    {
        private TextBox _tbox;

        partial void CreateNewNeededParts_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {
            // Write your code here.
            this.NeededPartsProperty = new NeededParts();
        }

        partial void CreateNewNeededParts_Saved()
        {
            // Write your code here.
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.NeededPartsProperty);
        }

        partial void Bulk_Insert_CanExecute(ref bool result)
        {
            // Write your code here.
            if (this._tbox == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }

        }

        partial void Bulk_Insert_Execute()
        {
            // Write your code here.
            var cnt = this._tbox.Text;
            foreach (var line in cnt.Split('\n'))
            { 
            }

        }

        void tbox_ControlAvailable(object sender, ControlAvailableEventArgs e)
        {
            this._tbox = (TextBox)e.Control;
        }

        partial void CreateNewNeededParts_Activated()
        {
            // Write your code here.
            var tbox = this.FindControl("TextBox_Bulk");

            tbox.ControlAvailable += new EventHandler<ControlAvailableEventArgs>(tbox_ControlAvailable);
        }

    }
}