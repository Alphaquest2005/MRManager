using System;
using SystemInterfaces;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;


namespace DataServices.Actors
{
    public static class LoadEntityViewExtensions
    {
        public static void LoadEntityView<T>(this LoadEntityView<T> msg, ISourceMessage source) where T : class, IEntity
        {

            Type viewDBType = null;
            TypeMaps.TypeMaps.InterfacesEntityDictionary.TryGetValue(msg.ViewType, out viewDBType);
            if(viewDBType == null) throw new Exception("Type " + msg.ViewType.FullName + "Not Matched");
            var task = typeof(EF7DataContext<T>)
                .GetMethod("GetEntityView")
                .MakeGenericMethod(msg.ViewType, viewDBType)
                .Invoke(null, new object[] { null, msg.Expression });

            var entitySetType = typeof(EntitySetLoaded<>).MakeGenericType(msg.ViewType);
            var entitySetInst = Activator.CreateInstance(entitySetType, task,msg.Process, msg.Source);
            //ToDo: publish view message
         //   EventMessageBus.Current.Publish(entitySetInst, source);
        }

        public static void LoadEntityView<T>(this LoadEntityViewWithFilter<T> msg, ISourceMessage source) where T : class, IEntity
        {

            var task = typeof(EF7DataContext<T>)
                   .GetMethod("GetEntityView")
                   .MakeGenericMethod(msg.ViewType, msg.ViewDbType)
                   .Invoke(null, new object[] { msg.Filter, msg.Expression });



            var entitySetType = typeof(EntitySetWithFilterLoaded<>).MakeGenericType(msg.ViewType);
            var entitySetInst = Activator.CreateInstance(entitySetType, task, msg.Process, source);
            //Todo: publish view message
           // EventMessageBus.Current.Publish(entitySetInst, source);
        }
    }
}