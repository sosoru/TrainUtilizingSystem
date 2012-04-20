using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class PurchaseInfo
    {
        partial void PricePerUnit_Compute(ref decimal result)
        {
            // Set result to the desired field value
            result = this.Price / this.Qty;
        }
    }
}
