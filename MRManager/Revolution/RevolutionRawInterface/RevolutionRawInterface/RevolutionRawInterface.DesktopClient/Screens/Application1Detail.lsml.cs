using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System;

namespace LightSwitchApplication
{
    public partial class Application1Detail
    {
        partial void Application1_Loaded(bool succeeded)
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.Application1);
        }

        partial void Application1_Changed()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.Application1);
        }

        partial void Application1Detail_Saved()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.Application1);
        }
    }
}