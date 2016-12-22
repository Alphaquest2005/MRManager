using System;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Akka.Actor;
using DataServices.Actors;
using MREntitiesQS.DataLayer.Context;


namespace ActorBackBone
{
    [Export]
    public class ActorBackBone: IActorBackBone
    {
        
        static ActorBackBone()
        {
            try
            {
                System = ActorSystem.Create("DataServiceSystem");
                Instance = new ActorBackBone();

                System.ActorOf(Props.Create(() => new DataContextManager(new MREntitiesQSContext())),
                    "MREntitiesDataContextManager");
                //System.ActorOf(Props.Create(() => new DataContextManager(new CoreEntitiesContext())),
                //    "CoreDataContextManager");
               



            }
            catch (Exception)
            {
                throw;
            }

        }

        public static ActorBackBone Instance { get; }

        public static ActorSystem System { get; }

    }
}
