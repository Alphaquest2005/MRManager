using System;
using System.ComponentModel.Composition;
using System.Reflection;
using SystemInterfaces;
using Akka.Actor;
using DataInterfaces;
using DataServices.Actors;



namespace ActorBackBone
{
    [Export]
    public class ActorBackBone: IActorBackBone
    {
        //ToDo:Get rid of private setter
        public static ActorBackBone Instance { get; private set; }
       
        public static ActorSystem System { get; private set; }


        public void Intialize(IDataContext ctx, Assembly dbContextAssembly, Assembly entityAssembly)
        {
             try
            {
                System = ActorSystem.Create("System");
                System.ActorOf(Props.Create<ServiceManager>(ctx, dbContextAssembly, entityAssembly),"ServiceManager");
                Instance = this;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
