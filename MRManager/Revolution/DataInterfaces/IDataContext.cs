using System.ComponentModel.Composition;
using NHibernate;

namespace DataInterfaces
{
    [InheritedExport]
    public interface IDataContext
    {
        ISessionFactory Instance { get; }
    }
}