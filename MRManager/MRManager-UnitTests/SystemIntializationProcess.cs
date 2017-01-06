using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
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
using ViewMessages;
using ViewModel.Interfaces;
using ViewModels;


namespace MRManager_UnitTests
{
    [TestClass]
    public class SystemIntializationProcess
    {
        
        protected ISourceMessage SourceMessage => new SourceMessage(new MessageSource(this.ToString()), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        private static void StartSystem()
        {

            var t = new MRManagerDBContext().GetType().Assembly;
            var x = new EFEntity<IEntity>().GetType().Assembly;
            BootStrapper.BootStrapper.Instance.StartUp(t, x);
            var mainWindow = MainWindowViewModel.Instance;

        }


        [TestMethod]
        public void SystemTest()
        {
            RegisterProcess1Events();
            RegisterProcess2Events();

            StartSystem();
            
            
            Thread.Sleep(TimeSpan.FromSeconds(60));

            Process1Asserts();
            Process2Asserts();
        }

        private IProcessSystemMessage process2ServiceActorStarted;


        private void RegisterProcess2Events()
        {

            Func<IProcessSystemMessage, bool> processPredicate = x => x.Process.Id == 2;
            
            EventMessageBus.Current.GetEvent<IServiceStarted<IProcessService>>(SourceMessage).Where(processPredicate).Subscribe(x => process2ServiceActorStarted = x);
            EventMessageBus.Current.GetEvent<ISystemProcessStarted>(SourceMessage).Where(processPredicate).Subscribe(x => process2Started = x);
            EventMessageBus.Current.GetEvent<IViewModelCreated<ILoginViewModel>>(SourceMessage).Where(x => x.Process.Id == 2).Subscribe(x => LoginViewModelCreated = x);
            EventMessageBus.Current.GetEvent<IViewModelLoaded<IScreenModel, IViewModel>>(SourceMessage).Subscribe(x =>
            {
                LoginViewModelLoadedInMScreenViewModel = x;
            });
            EventMessageBus.Current.GetEvent<IRequestProcessState>(SourceMessage).Where(x => x.Process.Id == 2).Subscribe(x => process2StateRequest = x);

            EventMessageBus.Current.GetEvent<IViewStateLoaded<ILoginViewModel, IProcessState<ISignInInfo>>>(SourceMessage)
                .Where(x => x.Process.Id == 2)
                .Subscribe(x => viewLoadedState = x);

            EventMessageBus.Current.GetEvent<IViewStateLoaded<ILoginViewModel, IProcessState<ISignInInfo>>>(SourceMessage)
               .Where(z => z.Process.Id == 2 && process2StateRequest != null && z.State.Entity == NullEntity<ISignInInfo>.Instance)
               .Subscribe(z =>((dynamic)LoginViewModelCreated.ViewModel).Usersignin = "joe");

            EventMessageBus.Current.GetEvent<IServiceStarted<IEntityViewDataServiceActor<IGetEntityViewWithChanges<ISignInInfo>>>>(SourceMessage).Subscribe(x => getEntityChangesActor = x);

            EventMessageBus.Current.GetEvent<IProcessStateMessage<ISignInInfo>>(SourceMessage).Where(x => x.Process.Id == 2).Subscribe(x => process2StateMessageList.Add(x));
            
            EventMessageBus.Current.GetEvent<IGetEntityViewWithChanges<ISignInInfo>>(SourceMessage).Subscribe(x => UserNameEntityChanges = x);

            EventMessageBus.Current.GetEvent<IEntityViewWithChangesFound<ISignInInfo>>(SourceMessage).Where(x => x.Process.Id == 2 && x.Entity.Usersignin == "joe" && x.Changes.Count == 1).Subscribe(
                x =>
                {
                   ((dynamic)LoginViewModelCreated.ViewModel).Password = "test";
                    ((dynamic)LoginViewModelCreated.ViewModel).Commands["ValidateUserInfo"].Execute();
                });
            EventMessageBus.Current.GetEvent<IEntityViewWithChangesFound<ISignInInfo>>(SourceMessage)
                .Where(x => x.Process.Id == 2 && x.Changes.Count == 2 && x.Entity.Usersignin == "joe")
                .Subscribe(
                    x =>
                    {
                        userFound = x;
                    });
            EventMessageBus.Current.GetEvent<IUserValidated>(SourceMessage).Subscribe(x => userValidated = x);

        }

        private void Process2Asserts()
        {
            Assert.IsTrue(EventFailures.Count == 0);
            Assert.IsNotNull(process2ServiceActorStarted);
            Assert.IsNotNull(process2Started);
            Assert.IsNotNull(LoginViewModelCreated);
            Assert.IsTrue(process2StateMessageList.Count >= 1);
            Assert.IsNotNull(LoginViewModelLoadedInMScreenViewModel);
            Assert.IsInstanceOfType(LoginViewModelLoadedInMScreenViewModel.ViewModel, typeof(ILoginViewModel));
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
        private IProcessSystemMessage mainWindowViewModelCreated;
        private IProcessSystemMessage process2Started;
        private IViewModelCreated<ILoginViewModel> LoginViewModelCreated;
        private IViewModelLoaded<IScreenModel, IViewModel> LoginViewModelLoadedInMScreenViewModel;
        private IRequestProcessState process2StateRequest;
        private List<IProcessStateMessage<ISignInInfo>> process2StateMessageList = new List<IProcessStateMessage<ISignInInfo>>();
        private IGetEntityViewWithChanges<ISignInInfo> UserNameEntityChanges;
        private IEntityViewWithChangesFound<ISignInInfo> userFound;
        private IUserValidated userValidated;
        private IServiceStarted<IEntityViewDataServiceActor<IGetEntityViewWithChanges<ISignInInfo>>> getEntityChangesActor;
        private List<IProcessEventFailure> EventFailures = new List<IProcessEventFailure>();
        private IViewStateLoaded<ILoginViewModel, IProcessState<ISignInInfo>> viewLoadedState;


        private void RegisterProcess1Events()
        {
            EventMessageBus.Current.GetEvent<IServiceStarted<IServiceManager>>(SourceMessage).Subscribe(x => serviceManagerStarted = x);
            EventMessageBus.Current.GetEvent<ProcessEventFailure>(SourceMessage).Subscribe(x => EventFailures.Add(x));
            Func<IProcessSystemMessage, bool> procesPredicate = x => x.Process.Id == 1;
            //IServiceStarted<IServiceManager> serviceStarted = null;
            EventMessageBus.Current.GetEvent<IServiceStarted<IProcessService>>(SourceMessage).Where(procesPredicate).Subscribe(x => processServiceActorStarted = x);
            EventMessageBus.Current.GetEvent<IServiceStarted<IViewModelSupervisor>>(SourceMessage).Where(procesPredicate).Subscribe(x => viewModelSupervisorStarted = x);
            EventMessageBus.Current.GetEvent<ISystemProcessStarted>(SourceMessage).Where(procesPredicate).Subscribe(x => processStarted = x);
            EventMessageBus.Current.GetEvent<IViewModelCreated<IScreenModel>>(SourceMessage).Where(procesPredicate).Subscribe(x => screenViewModelCreated = x);
            EventMessageBus.Current.GetEvent<IViewModelLoaded<IMainWindowViewModel, IScreenModel>>(SourceMessage).Subscribe(x => screenViewModelLoadedInMainWindowViewModel = x);
            EventMessageBus.Current.GetEvent<ISystemProcessCompleted>(SourceMessage).Where(procesPredicate).Subscribe(x => processCompleted = x);
        }

        private void Process1Asserts()
        {
            Assert.IsTrue(EventFailures.Count == 0);
            Assert.IsNotNull(processStarted);
            Assert.IsNotNull(viewModelSupervisorStarted);
            Assert.IsNotNull(processServiceActorStarted);
            Assert.IsNotNull(screenViewModelCreated);
            Assert.IsNotNull(screenViewModelLoadedInMainWindowViewModel);
            Assert.IsNotNull(processCompleted);
        }




    }


}
