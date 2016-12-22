using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using DataInterfaces;
using EventAggregator;
using EventMessages;
using NHibernate.Linq;
using System.Linq.Expressions;
using CommonMessages;
using EFRepository;

namespace DataServices.Actors
{
    public class LoadEntitySetActor<T>: ReceiveActor where T : IEntity
   {
       private readonly IDataContext dbContext = null;
       public LoadEntitySetActor(IDataContext context)
        {
            
            Receive<LoadEntitySet<T>>(m => HandleLoadEntitySet(m.Source));
           Receive<LoadEntitySetWithFilter<T>>(m => HandleLoadEntitySet(m.Filter, m.Source));
           Receive<LoadEntitySetWithFilterWithIncludes<T>>(m => HandleLoadEntitySetWithIncludes(m.Filter, m.Includes, m.Source));
            dbContext = context;
        }

        MessageSource msgSource => new MessageSource(this.ToString());

        private void HandleLoadEntitySet(List<Expression<Func<T, bool>>> filter, MessageSource source )
       {
           try
           {
               
              var res = EF7DataContext<T>.GetData(filter,null);
              EventMessageBus.Current.Publish(new EntitySetWithFilterLoaded<T>(res, source), msgSource);
           }
           catch (Exception)
           {

               throw;
           }

       }

        private void HandleLoadEntitySetWithIncludes(List<Expression<Func<T, bool>>> filter, List<Expression<Func<T, dynamic>>> includes, MessageSource source = null)
        {
            try
            {

                var res = EF7DataContext<T>.GetData(filter, includes);
                EventMessageBus.Current.Publish(new EntitySetWithFilterWithIncludesLoaded<T>(res,includes, source), msgSource);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void HandleLoadEntitySet(MessageSource source)
        {
           try
           {
               using (var ctx = dbContext.Instance.OpenSession())
               using (var transaction = ctx.BeginTransaction())
               {
                   // Debug.WriteLine(filter.Translate());
                   EventMessageBus.Current.Publish(new EntitySetLoaded<T>(ctx.Query<T>().ToList(), source), msgSource);
               }
           }
           catch (Exception)
           {

               throw;
           }

        }
    }
}
