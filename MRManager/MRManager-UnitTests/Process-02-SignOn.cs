using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using SystemInterfaces;
using Actor.Interfaces;
using DataServices.Actors;
using Domain.Interfaces;
using EventAggregator;
using EventMessages;
using EventMessages.Commands;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Process.WorkFlow;
using RevolutionData;
using RevolutionEntities;
using RevolutionEntities.Process;
using ViewMessages;
using ViewModel.Interfaces;


namespace MRManager_UnitTests
{
    [TestClass]
    public class UserSignOnProcess
    {

        private ISystemSource Source = SystemIntializationProcess.Source;


        [TestMethod]
        public void Process02()
        {
            RegisterProcess2Events();

            SystemIntializationProcess.StartSystem();

            EventMessageBus.Current.GetEvent<ISystemProcessCompleted>(Source).Where(x => x.Process.Id == 1).Subscribe(
                x =>
                {
                    EventMessageBus.Current.Publish(new StartSystemProcess(2, new StateCommandInfo(2, RevolutionData.Context.Process.Commands.StartProcess), x.Process, Source), Source);
                });
            
            
            
            Thread.Sleep(TimeSpan.FromSeconds(15));

            Process2Asserts();
        }

        private IProcessSystemMessage process2ServiceActorStarted;


        private void RegisterProcess2Events()
        {
            EventMessageBus.Current.GetEvent<IProcessLogCreated>(Source).Subscribe(x => ProcessLogs.Enqueue(x));
            EventMessageBus.Current.GetEvent<IComplexEventLogCreated>(Source).Subscribe(x => ComplextEventLogs.Enqueue(x));
            EventMessageBus.Current.GetEvent<IProcessEventFailure>(Source).Subscribe(x => EventFailures.Enqueue(x));

            Func<IProcessSystemMessage, bool> processPredicate = x => x.Process.Id == 2;
            
            EventMessageBus.Current.GetEvent<IServiceStarted<IProcessService>>(Source).Where(processPredicate).Subscribe(x => process2ServiceActorStarted = x);
            EventMessageBus.Current.GetEvent<ISystemProcessStarted>(Source).Where(processPredicate).Subscribe(x => process2Started = x);
            EventMessageBus.Current.GetEvent<IViewModelCreated<ISigninViewModel>>(Source).Where(x => x.Process.Id == 2).Subscribe(x => LoginViewModelCreated = x);
            EventMessageBus.Current.GetEvent<IViewModelLoaded<IScreenModel, IViewModel>>(Source).Subscribe(x =>
            {
                LoginViewModelLoadedInMScreenViewModel = x;
            });
            EventMessageBus.Current.GetEvent<IRequestProcessState>(Source).Where(x => x.Process.Id == 2).Subscribe(x => process2StateRequest = x);

            EventMessageBus.Current.GetEvent<IViewStateLoaded<ISigninViewModel, IProcessState<ISignInInfo>>>(Source)
                .Where(x => x.Process.Id == 2)
                .Subscribe(x => viewLoadedState = x);

            EventMessageBus.Current.GetEvent<IViewStateLoaded<ISigninViewModel, IProcessState<ISignInInfo>>>(Source)
               .Where(z => z.Process.Id == 2 && process2StateRequest != null && z?.State?.Entity == NullEntity<ISignInInfo>.Instance)
               .Subscribe(z =>((dynamic)z.ViewModel).Usersignin = "joe");

            EventMessageBus.Current.GetEvent<IServiceStarted<IEntityViewDataServiceActor<IGetEntityViewWithChanges<ISignInInfo>>>>(Source).Subscribe(x => getEntityChangesActor = x);

            EventMessageBus.Current.GetEvent<IProcessStateMessage<ISignInInfo>>(Source).Where(x => x.Process.Id == 2).Subscribe(x => process2StateMessageList.Add(x));
            
            EventMessageBus.Current.GetEvent<IGetEntityViewWithChanges<ISignInInfo>>(Source).Subscribe(x => UserNameEntityChanges = x);

            EventMessageBus.Current.GetEvent<IGetEntityViewWithChanges<ISignInInfo>>(Source).Subscribe(x => GetEntityViewWithChanges.Enqueue(x));

            EventMessageBus.Current.GetEvent<IEntityViewWithChangesFound<ISignInInfo>>(Source).Where(x => x.Process.Id == 2 && x.Entity.Usersignin == "joe" && x.Changes.Count == 1).Subscribe(
                x =>
                {
                   ((dynamic)LoginViewModelCreated.ViewModel).Password = "test";
                    ((dynamic)LoginViewModelCreated.ViewModel).Commands["ValidateUserInfo"].Execute();
                });
            EventMessageBus.Current.GetEvent<IEntityViewWithChangesFound<ISignInInfo>>(Source)
                .Where(x => x.Process.Id == 2 && x.Changes.Count == 2 && x.Entity.Usersignin == "joe")
                .Subscribe(
                    x =>
                    {
                        userFound = x;
                    });
            EventMessageBus.Current.GetEvent<IUserValidated>(Source).Subscribe(x => userValidated = x);
            EventMessageBus.Current.GetEvent<ISystemProcessCompleted>(Source).Where(x => x.Process.Id == 2).Subscribe(x => process2Completed = x);
            EventMessageBus.Current.GetEvent<IStartSystemProcess>(Source).Where(x => x.ProcessToBeStartedId == ProcessActions.NullProcess).Subscribe(x => StartProcess3 = x);

            ///////// Process Clean up ////////////////////

            EventMessageBus.Current.GetEvent<ICleanUpSystemProcess>(Source).Where(x => x.ProcessToBeCleanedUpId == 2).Subscribe(x => CommandtoCleanupProcess2 = x);
            
            
        }

        private void Process2Asserts()
        {
            Assert.IsTrue(EventFailures.Count == 0 && ProcessLogs.IsEmpty && ComplextEventLogs.Count == 0);
            Assert.IsNotNull(process2ServiceActorStarted);
            Assert.IsNotNull(process2Started);
            Assert.IsNotNull(LoginViewModelCreated);
            Assert.IsTrue(process2StateMessageList.Count >= 1);
            Assert.IsNotNull(LoginViewModelLoadedInMScreenViewModel);
            Assert.IsInstanceOfType(LoginViewModelLoadedInMScreenViewModel.ViewModel, typeof(ISigninViewModel));
            Assert.IsNotNull(getEntityChangesActor);
            Assert.IsNotNull(process2StateRequest);
            Assert.IsTrue(process2StateMessageList.Count >= 2);
            Assert.IsNotNull(viewLoadedState);
            Assert.IsNotNull(UserNameEntityChanges);
            Assert.IsNotNull(userFound);
            Assert.IsTrue(GetEntityViewWithChanges.Count == 2);
            Assert.IsNotNull(userValidated);
            Assert.IsNotNull(process2Completed);

            
            Assert.IsNotNull(StartProcess3);

            Assert.IsNotNull(CommandtoCleanupProcess2);
            Assert.IsFalse(LoginViewModelLoadedInMScreenViewModel.LoadingViewModel.BodyViewModels.Contains(LoginViewModelLoadedInMScreenViewModel.ViewModel));
            

        }



        private IProcessSystemMessage process2Completed;
       // private IProcessSystemMessage mainWindowViewModelCreated;
        private IProcessSystemMessage process2Started;
        private IProcessSystemMessage StartProcess3;
        private IViewModelCreated<ISigninViewModel> LoginViewModelCreated;
        private IViewModelLoaded<IScreenModel, IViewModel> LoginViewModelLoadedInMScreenViewModel;
        private IRequestProcessState process2StateRequest;
        private List<IProcessStateMessage<ISignInInfo>> process2StateMessageList = new List<IProcessStateMessage<ISignInInfo>>();
        private IGetEntityViewWithChanges<ISignInInfo> UserNameEntityChanges;
        private IEntityViewWithChangesFound<ISignInInfo> userFound;
        private IUserValidated userValidated;
        private IServiceStarted<IEntityViewDataServiceActor<IGetEntityViewWithChanges<ISignInInfo>>> getEntityChangesActor;
        private ConcurrentQueue<IProcessEventFailure> EventFailures = new ConcurrentQueue<IProcessEventFailure>();
        private ConcurrentQueue<IGetEntityViewWithChanges<ISignInInfo>> GetEntityViewWithChanges = new ConcurrentQueue<IGetEntityViewWithChanges<ISignInInfo>>();
        private IViewStateLoaded<ISigninViewModel, IProcessState<ISignInInfo>> viewLoadedState;


    

        private ConcurrentQueue<IComplexEventLogCreated> ComplextEventLogs = new ConcurrentQueue<IComplexEventLogCreated>();

        private ConcurrentQueue<IProcessLogCreated> ProcessLogs =new ConcurrentQueue<IProcessLogCreated>();
        private ICleanUpSystemProcess CommandtoCleanupProcess2;
    }


}
