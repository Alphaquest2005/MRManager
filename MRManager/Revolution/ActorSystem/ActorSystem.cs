using System;
using System.ComponentModel.Composition;
using System.Reflection;
using Actor.Interfaces;
using Akka.Actor;
using DataServices.Actors;



namespace ActorBackBone
{
    [Export(typeof(IActorBackBone))]
    public class ActorBackBone: IActorBackBone
    {
        //ToDo:Get rid of private setter
        public static ActorBackBone Instance { get; private set; }
       
        public static ActorSystem System { get; private set; }


        public void Intialize(Assembly dbContextAssembly, Assembly entityAssembly, bool autoRun)
        {
             try
            {
                System = ActorSystem.Create("System");
                System.ActorOf(Props.Create<ServiceManager>(dbContextAssembly, entityAssembly, autoRun),"ServiceManager");
                Instance = this;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
