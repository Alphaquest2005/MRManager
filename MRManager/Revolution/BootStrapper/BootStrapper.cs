using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using SystemInterfaces;
using DataInterfaces;
using MefContrib.Hosting.Generics;


namespace BootStrapper
{
    public class BootStrapper
    {
        static BootStrapper()
        {
            try
            {
                Instance = new BootStrapper();

                var catalog =
                    new DirectoryCatalog(
                        Path.GetDirectoryName(
                            Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().Location).Path)),
                        "*.dll");
                GenericCatalog genericCatalog = new GenericCatalog(catalog);
                Container = new CompositionContainer(genericCatalog);
            }
            catch (Exception)
            {

                throw;
            }



        }

        public static BootStrapper Instance { get; }

        public void StartUp(IDataContext ctx, Assembly dbContextAssembly, Assembly entityAssembly)
        {
            try
            {
                var x = Container.GetExport<IActorBackBone>().Value;
                x.Intialize(ctx, dbContextAssembly, entityAssembly);
            }
            catch (Exception)
            {

                throw;
            }



            // var t = Container.GetExport<ISummaryViewModel<IPatientInfo>>().Value;
            // EventMessageBus.Current.Publish(Container.GetExport<ICreateSummaryViewModel<IPatientInfo>>().Value);
            //EventMessageBus.Current.Publish(Container.GetExport<IStartDataService>().Value, new MessageSource(this.ToString()));

        }

        public static CompositionContainer Container { get;  }
    }
}
