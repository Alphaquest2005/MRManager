using System.Data.Entity;

namespace T4Entities
{
    public partial class AmoebaDBEntities : DbContext
    {
        public AmoebaDBEntities(string connString)
            : base(connString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}
