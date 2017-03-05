using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using SystemInterfaces;
using Actor.Interfaces;
using EventAggregator;
using EventMessages;
using EventMessages.Commands;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Process.WorkFlow;
using RevolutionData;
using RevolutionEntities.Process;
using ViewModel.Interfaces;


namespace MRManager_UnitTests
{
    [TestClass]
    public class PatientInfoProcess
    {

        private ISystemSource Source = SystemIntializationProcess.Source;


        [TestMethod]
        public void UserProcess03()
        {
            RegisterEvents();

            SystemIntializationProcess.StartSystem();
            //Don't change process number system stops at 1
            EventMessageBus.Current.GetEvent<ISystemProcessCompleted>(Source).Where(x => x.Process.Id == 1).Subscribe(
                x =>
                {
                    EventMessageBus.Current.Publish(new StartSystemProcess(3,
                                            new StateCommandInfo(3, RevolutionData.Context.Process.Commands.StartProcess),
                                            new SystemProcess(Processes.ProcessInfos.FirstOrDefault(z => z.Id == 1),
                                                              new Agent("joe"),
                                                              SystemIntializationProcess.Source.MachineInfo ),
                                            Source), Source);
                });
            
            
            
            Thread.Sleep(TimeSpan.FromSeconds(15));

            Asserts();
        }

       


        private void RegisterEvents()
        {
            EventMessageBus.Current.GetEvent<IProcessLogCreated>(Source).Subscribe(x => ProcessLogs.Enqueue(x));
            EventMessageBus.Current.GetEvent<IComplexEventLogCreated>(Source).Subscribe(x => ComplextEventLogs.Enqueue(x));
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source).Subscribe(x => EventFailures.Enqueue(x));

            Func<IProcessSystemMessage, bool> processPredicate = x => x.Process.Id == 3;
            
            EventMessageBus.Current.GetEvent<IServiceStarted<IProcessService>>(Source).Where(processPredicate).Subscribe(x => ServiceActorStarted = x);
            EventMessageBus.Current.GetEvent<ISystemProcessStarted>(Source).Where(processPredicate).Subscribe(x => ProcessStarted = x);

            EventMessageBus.Current.GetEvent<IViewModelCreated<IPatientSummaryListViewModel>>(Source).Where(x => x.Process.Id == 3).Subscribe(x => PatientSummaryListViewModelCreated = x);
            EventMessageBus.Current.GetEvent<IViewModelLoaded<IScreenModel, IViewModel>>(Source).Where(x => x.Process.Id == 3 && x.ViewModel is IPatientSummaryListViewModel).Subscribe(x =>  //
            {
                PatientSummaryViewModelLoadedInMScreenViewModel = x;
            });
            EventMessageBus.Current.GetEvent<IRequestProcessState>(Source).Where(x => x.Process.Id == 3).Subscribe(x => process3StateRequest = x);

            EventMessageBus.Current.GetEvent<IUpdateProcessStateList<IPatientInfo>>(Source).Where(x => x.Process.Id == 3).Subscribe(x => processStateMessageList.Add(x));

            EventMessageBus.Current.GetEvent<IEntityViewSetWithChangesLoaded<IPatientInfo>>(Source).Where(x => x.Process.Id == 3 && x.Changes.Count == 0).Subscribe(x => EntityViewSetLoaded_without_Changes = x);

            EventMessageBus.Current.GetEvent<IEntityViewSetWithChangesLoaded<IPatientInfo>>(Source).Where(x => x.Process.Id == 3 && x.Changes.Count == 2).Subscribe(x => EntityViewSetLoaded_WithChanges = x);

            EventMessageBus.Current.GetEvent<IViewStateLoaded<IPatientSummaryListViewModel, IProcessStateList<IPatientInfo>>>(Source).Where(x => x.Process.Id == 3).Subscribe(x => InitialViewStateLoaded = x);

            EventMessageBus.Current.GetEvent<IViewStateLoaded<IPatientSummaryListViewModel, IProcessStateList<IPatientInfo>>>(Source).Where(x => x.Process.Id == 3 
                                      //  && x.State != null && x.State.EntitySet.Any() 
                                        && processStateMessageList.Count == 1).Subscribe(
                x =>
                {
                    ((dynamic)PatientSummaryListViewModelCreated.ViewModel).Name = "lisa";
                    ((dynamic)PatientSummaryListViewModelCreated.ViewModel).Age = "27";
                    ((dynamic)PatientSummaryListViewModelCreated.ViewModel).Commands["Search"].Execute();
                });

        }

        private void Asserts()
        {
            Assert.IsTrue(EventFailures.Count == 0 && ProcessLogs.IsEmpty && ComplextEventLogs.Count == 0);
            Assert.IsNotNull(ServiceActorStarted);
            Assert.IsNotNull(ProcessStarted);
            Assert.IsNotNull(PatientSummaryListViewModelCreated);
            Assert.IsNotNull(process3StateRequest); 
            Assert.IsNotNull(PatientSummaryViewModelLoadedInMScreenViewModel);
            Assert.IsNotNull(InitialViewStateLoaded);
            Assert.IsNotNull(EntityViewSetLoaded_without_Changes);
            Assert.IsNotNull(EntityViewSetLoaded_WithChanges);
            Assert.IsTrue(processStateMessageList.Count == 2);


        }



        private IProcessSystemMessage ServiceActorStarted;
        private IProcessSystemMessage ProcessStarted;
        private ConcurrentQueue<IProcessEventFailure> EventFailures = new ConcurrentQueue<IProcessEventFailure>();
        private ConcurrentQueue<IComplexEventLogCreated> ComplextEventLogs = new ConcurrentQueue<IComplexEventLogCreated>();
        private ConcurrentQueue<IProcessLogCreated> ProcessLogs =new ConcurrentQueue<IProcessLogCreated>();
        private IViewModelCreated<IPatientSummaryListViewModel> PatientSummaryListViewModelCreated;
        private IViewModelLoaded<IScreenModel, IViewModel> PatientSummaryViewModelLoadedInMScreenViewModel;
        private IRequestProcessState process3StateRequest;
        
        private IViewStateLoaded<IPatientSummaryListViewModel, IProcessState<IPatientInfo>> InitialViewStateLoaded;
        private List<IUpdateProcessStateList<IPatientInfo>> processStateMessageList = new List<IUpdateProcessStateList<IPatientInfo>>();
        private IEntityViewSetWithChangesLoaded<IPatientInfo> EntityViewSetLoaded_without_Changes;
        private IEntityViewSetWithChangesLoaded<IPatientInfo> EntityViewSetLoaded_WithChanges;
    }


}
