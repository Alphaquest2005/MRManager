using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using SystemInterfaces;
using CommonMessages;
using EventAggregator;
using MefContrib.Hosting.Generics;
using StartUp.Messages;


namespace BootStrapper
{
    public class BootStrapper
    {
        static BootStrapper()
        {
            Instance = new BootStrapper();
            var catalog =
                new DirectoryCatalog(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().Location).Path)),"*.dll");
            GenericCatalog genericCatalog = new GenericCatalog(catalog);
            Container = new CompositionContainer(genericCatalog);
   
           
        }

        public static BootStrapper Instance { get; }

        public void StartUp()
        {
            var x = Container.GetExport<IActorBackBone>().Value;
           // var t = Container.GetExport<ISummaryViewModel<IPatientInfo>>().Value;
           // EventMessageBus.Current.Publish(Container.GetExport<ICreateSummaryViewModel<IPatientInfo>>().Value);
            EventMessageBus.Current.Publish(Container.GetExport<IStartDataService>().Value, new MessageSource(this.ToString()));
           
        }

        public static CompositionContainer Container { get;  }
    }

  
}
