

using System.Linq;
using System.Windows;

using CoreEntities.Client.Repositories;


using System.ComponentModel;


namespace WaterNut.QuerySpace.CoreEntities.ViewModels
{
    public partial class BaseViewModel 
    {
        
        private BaseViewModel()
        {
        
            if (CurrentApplicationSettings == null && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                using (var ctx = new ApplicationSettingsRepository())
                {
                    CurrentApplicationSettings = ctx.ApplicationSettings().Result.FirstOrDefault();
                }

                if (CurrentApplicationSettings == null)
                {
                    MessageBox.Show("No Default Application Settings Defined");
                }
            }
        }
    }
}
