using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInterfaces;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NH.Mappings;
using NHibernate;
using NHibernate.Mapping;


namespace NH.DBContext
{
    public partial class NHDBContext : IDataContext
    {
        private static readonly ISessionFactory _instance;

        static NHDBContext()
        {
            _instance = Fluently.Configure()
                                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(Properties.Settings.Default.DataBaseConnectionString))
                                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ApplicationSettingMap>()).BuildSessionFactory();

            
        }

        public ISessionFactory Instance => _instance;

    }
}
