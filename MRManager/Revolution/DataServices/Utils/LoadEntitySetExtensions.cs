using SystemInterfaces;
using CommonMessages;
using DataInterfaces;
using EFRepository;
using EventAggregator;
using EventMessages;

namespace DataServices.Actors
{
    public static class LoadEntitySetExtensions
    {
        

        public static void LoadEntitySet<T>(this LoadEntitySet<T> msg, IDataContext dbContext,  ISourceMessage source) where T : IEntity
        {

            var res = EF7DataContext<T>.GetData(null, null);
            EventMessageBus.Current.Publish(new EntitySetLoaded<T>(res,msg.Process, source), source);
        }

        public static void LoadEntitySet<T>(this LoadEntitySetWithFilter<T> msg, IDataContext dbContext, ISourceMessage source) where T : IEntity
                {

                    var res = EF7DataContext<T>.GetData(msg.Filter, null);
                    EventMessageBus.Current.Publish(new EntitySetWithFilterLoaded<T>(res,msg.Process, source), source);
                }
        public static void LoadEntitySet<T>(this LoadEntitySetWithFilterWithIncludes<T> msg, IDataContext dbContext, ISourceMessage source) where T : IEntity
        {

            var res = EF7DataContext<T>.GetData(msg.Filter, msg.Includes);
            EventMessageBus.Current.Publish(new EntitySetWithFilterWithIncludesLoaded<T>(res,msg.Includes,msg.Process, source), source);
        }
    }
}