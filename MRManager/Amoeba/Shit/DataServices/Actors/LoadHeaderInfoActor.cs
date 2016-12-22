

using System;
using System.Linq.Expressions;
using Akka.Actor;
using EventMessages;
using DataInterfaces;

namespace DataServices.Actors
{
    public class LoadHeaderInfoActor<T> : ReceiveActor where T : class, IEntity
    {
        private readonly IDataContext dbContext = null;
        public LoadHeaderInfoActor(IDataContext context)
        {
            Receive<LoadHeaderInfo<T>>(m => HandleLoadResults(m.SelectExpression));
            dbContext = context;
        }



        private void HandleLoadResults(Expression<Func<T, IHeaderInfo<T>>> selectExpression)
        {

            using (var ctx = dbContext.Instance.OpenSession())
            using (var transaction = ctx.BeginTransaction())
            {

                // Debug.WriteLine(filter.Translate());
                try
                {
                    
                    // var lst =
                    //     ctx.Query<IPatient>()
                    //         .Fetch(t => t.Person).ThenFetch(x => x.Names)
                    //         .ToList();
                    //var res = lst.Select(p => new HeaderInfo<IPatient>(p.Id,p.Person.Names.Select(z => z.PersonName1).First()))
                    //         .ToList(); //.Aggregate((c, n) => c + " " + n)





                   // EventMessageBus.Current.Publish(new HeaderInfoLoaded<IPatientInfo>(lst.Select(x => (IHeaderInfo<IPatient>)x).ToList()), this.ToString());
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }


    }
}
