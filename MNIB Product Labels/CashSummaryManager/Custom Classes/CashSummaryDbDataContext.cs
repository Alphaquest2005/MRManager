using System.Data.Linq;

namespace CashSummaryManager
{
    public partial class CashSummaryDBDataContext
    {
        partial void OnCreated()
        {
            DataLoadOptions options = new DataLoadOptions();
            
            options.LoadWith<User>(p => p.UserPermissions);
            options.LoadWith<UserPermission>(p => p.Permission);

            options.LoadWith<DrawerCashDetail>(p => p.CashTypeComponent);
            options.LoadWith<CashTypeComponent>(p => p.CashComponent);
            options.LoadWith<CashTypeComponent>(p => p.CashType);
           // options.LoadWith<CashType>(p => p.CashTypeComponents);
            

            LoadOptions = options;
        }
    }
}