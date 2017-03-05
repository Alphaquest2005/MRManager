using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using SystemInterfaces;
using Actor.Interfaces;
using Akka.Actor;
using DataServices.Actors;
using ViewModel.Interfaces;


namespace ActorBackBone
{
    [Export(typeof(IActorBackBone))]
    public class ActorBackBone: IActorBackBone
    {
        //ToDo:Get rid of private setter
        public static ActorBackBone Instance { get; private set; }
       
        public static ActorSystem System { get; private set; }


        public void Intialize(bool autoRun, List<IMachineInfo> machineInfo, List<IProcessInfo> processInfos, List<IComplexEventAction> complexEventActions, List<IViewModelInfo> viewInfos)
        {
             try
            {
                System = ActorSystem.Create("System");
                System.ActorOf(Props.Create<ServiceManager>(autoRun,machineInfo, processInfos, complexEventActions,viewInfos),"ServiceManager");
                Instance = this;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
