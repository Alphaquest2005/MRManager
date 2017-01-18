using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using Common;
using CommonMessages;
using DataEntites;
using DataServices.Actors;
using Domain.Interfaces;
using EF.DBContexts;
using EF.Entities;
using EventAggregator;
using EventMessages;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevolutionEntities.Process;
using StartUp.Messages;
using Utilities;
using ViewMessages;
using ViewModel.Interfaces;
using ViewModels;
using RevolutionLogger;




namespace MRManager_UnitTests
{
    [TestClass]
    public class UserSignOnProcess
    {

        private ISystemSource Source = SystemIntializationProcess.Source;


        [TestMethod]
        public void SystemTest()
        {
            RegisterProcess2Events();

            SystemIntializationProcess.StartSystem();

            EventMessageBus.Current.GetEvent<ISystemProcessCompleted>(Source).Where(x => x.Process.Id == 1).Subscribe(
                x =>
                {
                    EventMessageBus.Current.Publish(new StartSystemProcess(2, new StateCommandInfo(2, RevolutionData.Context.Process.Commands.StartProcess), x.Process, Source), Source);
                });
            
            
            
            Thread.Sleep(TimeSpan.FromSeconds(30));

            Process2Asserts();
        }

        private IProcessSystemMessage process2ServiceActorStarted;


        private void RegisterProcess2Events()
        {

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

        }

        private void Process2Asserts()
        {
            Assert.IsTrue(EventFailures.Count == 0);
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
            Assert.IsNotNull(userValidated);

        }


        private IProcessSystemMessage serviceManagerStarted;
        private IProcessSystemMessage processServiceActorStarted;
        private IProcessSystemMessage viewModelSupervisorStarted;
        private IProcessSystemMessage processStarted;
        private IProcessSystemMessage screenViewModelCreated;
        private IProcessSystemMessage screenViewModelLoadedInMainWindowViewModel;
        private IProcessSystemMessage processCompleted;
       // private IProcessSystemMessage mainWindowViewModelCreated;
        private IProcessSystemMessage process2Started;
        private IViewModelCreated<ISigninViewModel> LoginViewModelCreated;
        private IViewModelLoaded<IScreenModel, IViewModel> LoginViewModelLoadedInMScreenViewModel;
        private IRequestProcessState process2StateRequest;
        private List<IProcessStateMessage<ISignInInfo>> process2StateMessageList = new List<IProcessStateMessage<ISignInInfo>>();
        private IGetEntityViewWithChanges<ISignInInfo> UserNameEntityChanges;
        private IEntityViewWithChangesFound<ISignInInfo> userFound;
        private IUserValidated userValidated;
        private IServiceStarted<IEntityViewDataServiceActor<IGetEntityViewWithChanges<ISignInInfo>>> getEntityChangesActor;
        private ConcurrentQueue<IProcessEventFailure> EventFailures = new ConcurrentQueue<IProcessEventFailure>();
        private IViewStateLoaded<ISigninViewModel, IProcessState<ISignInInfo>> viewLoadedState;


    

        private ConcurrentQueue<IComplexEventLogCreated> ComplextEventLogs = new ConcurrentQueue<IComplexEventLogCreated>();

        private ConcurrentQueue<IProcessLogCreated> ProcessLogs =new ConcurrentQueue<IProcessLogCreated>();

    




    }


}
