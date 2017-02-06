using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Hosting.Extension;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MEFTestCases
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var catalog = new AggregateCatalog(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            var genericCatalog = new GenericCatalog(catalog);
            var container = new CompositionContainer(genericCatalog);

            var test = container.GetExportedValueOrDefault<IEntityCacheViewModel<IVisitType>>();
            var c = container.GetExports(typeof(IEntityCacheViewModel<>), null, null);

        }
 public interface IVisitType: IEntity
    {
    }

    public interface IEntityCacheViewModel<T> where T:IEntity
    {
    }

    [Export(typeof(IEntityCacheViewModel<>))]
    public class ViewModel<TEntity> : IEntityCacheViewModel<TEntity> where TEntity : IEntity
    {
        
    }

    public interface IEntity
    {
    }

    }

   
}
