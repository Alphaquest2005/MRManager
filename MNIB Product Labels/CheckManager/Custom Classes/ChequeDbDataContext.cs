using System.Data.Linq;

namespace CheckManager
{
    public partial class ChequeDBDataContext
    {
        partial void OnCreated()
        {
            DataLoadOptions options = new DataLoadOptions();
            options.LoadWith<Voucher>(p => p.Prepared);
            options.LoadWith<Voucher>(p => p.Authorizeds);
            options.LoadWith<Voucher>(p => p.Disbursed);
            options.LoadWith<Prepared>(p => p.User);
            options.LoadWith<Authorized>(p => p.User);
            options.LoadWith<Disbursed>(p => p.Payee);
            options.LoadWith<Disbursed>(p => p.User);
            options.LoadWith<User>(p => p.UserPermissions);
            options.LoadWith<UserPermission>(p => p.Permission);
            LoadOptions = options;
        }
    }
}