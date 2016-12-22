//using System;
//using System.Diagnostics;
//using System.Linq;
//using Akka.Actor;
//using DataContext;
//using DataInterfaces;
//using EventAggregator;
//using EventMessages;
//using Model.Interfaces;
//using NHibernate.Linq;
//using System.Linq.Expressions;
//using DataEntites.ViewEntities;
//using NHibernate;
//using Utilities;

//namespace DataServices.Actors
//{
//   public class LoadResultsActor: ReceiveActor 
//   {
//       private readonly IDataContext dbContext = null;
//       public LoadResultsActor(IDataContext context)
//        {
//           Receive<LoadResults<,>>(m => HandleLoadResults(m.Filter, m.Result));
//            dbContext = context;
//        }

        

//       private void HandleLoadResults<T,TResults>(Expression<Func<T, bool>> filter, Expression<Func<T, TResults>> result) where T:IEntity where TResults:class
//       {
//            using (var ctx = dbContext.Instance.OpenSession())
//            using (var transaction = ctx.BeginTransaction())
//            {
              
//              // Debug.WriteLine(filter.Translate());
//               EventMessageBus.Current.Publish(new ResultsLoaded<TResults>(ctx.Query<T>().Where(filter).Select(result).ToList()));
//            }
//        }

        
//    }
//}
