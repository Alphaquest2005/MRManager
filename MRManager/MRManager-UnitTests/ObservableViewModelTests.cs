using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SystemInterfaces;
using Common;
using EF.Entities;
using EventAggregator;
using EventMessages;
using EventMessages.Events;
using Interfaces;
using JB.Reactive.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using Utilities;
using ViewModel.Interfaces;
using ViewModels;
using Process = RevolutionEntities.Process.Process;

namespace MRManager_UnitTests
{
    [TestClass]
    public class ObservableViewModelTests:IProcessSource
    {
        private static readonly ProcessState<ISignInInfo> ProcessState;
        private static readonly SystemProcess TestProcess= new SystemProcess(new RevolutionEntities.Process.Process(1, 0, "Test Proces", "This is a Test", "T", new Agent("TestManager")),new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        static ObservableViewModelTests()
        {
            ProcessState = new ProcessState<ISignInInfo>(TestProcess, new SignInInfo() { Id = 5, Usersignin = "Joe", Password = "Test" }, new StateInfo(1, "Test Name", "Test status", ""));
        }
       
    
        public ISystemSource Source => new Source(Guid.NewGuid(), $"EntityRepository:<{typeof(ObservableViewModelTests).GetFriendlyName()}>",new SourceType(typeof(ObservableViewModelTests)), new SystemProcess(new RevolutionEntities.Process.Process(1, 0, "Starting System", "Prepare system for Intial Use", "", new Agent("System")), new MachineInfo(Environment.MachineName, Environment.ProcessorCount)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        [TestMethod]
        public void InitalizeObserveableWithNoSubscriptions()
        {
           
            dynamic viewModel = new SigninViewModel(process: TestProcess,
                viewInfo: new ViewInfo("","",""), 
               eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
               eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>()
               {
                   
               }, commandInfo: new List<IViewModelEventCommand<IViewModel,IEvent>>(), orientation: typeof(IBodyViewModel), priority: 0);
            
            viewModel.State.Value = ProcessState;
            // var id = dynamicViewModel.GetValue("Id");
           Assert.AreEqual(5, viewModel.Id);
        }

        [TestMethod]
        public void InitalizeObserveableWithPublications()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<ISignInInfo>>(Source)
                .Subscribe(x => handleEntityChanges(x));
            dynamic viewModel = new SigninViewModel(process: TestProcess,
                viewInfo: new ViewInfo("", "", ""),
               eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
               eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>()
               {
                   new ViewEventPublication<SigninViewModel, EntityChanges<ISignInInfo>>(
                       subject: (s) => s.ChangeTracking.DictionaryChanges,
                       subjectPredicate: new List<Func<SigninViewModel, bool>>(),
                       messageData: (s) => new ViewEventPublicationParameter(new object[] {s.State.Value.Entity.Id, s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},new StateEventInfo(s.Process.Id, RevolutionData.Context.View.Events.EntityChanged), s.Process, s.Source ), key: "Entity Changes")
               }, commandInfo: new List<IViewModelEventCommand<IViewModel,IEvent>>(), orientation: typeof(IBodyViewModel), priority: 0);

            viewModel.State.Value = ProcessState;


            viewModel.Id = 6;
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(6, viewModel.Id);
        }

        [TestMethod]
        public void InitalizeObserveableWithPublicationsSubjectPredicate()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<ISignInInfo>>(Source)
                .Subscribe(x => handleEntityChanges(x));
            dynamic viewModel = new SigninViewModel(process: TestProcess,
                viewInfo: new ViewInfo("", "", ""),
               eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
               eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>()
               {
                   new ViewEventPublication<SigninViewModel, EntityChanges<ISignInInfo>>(
                       subject: (s) => s.ChangeTracking.DictionaryChanges,
                       subjectPredicate: new List<Func<SigninViewModel, bool>>()
                       {
                           v => v.ChangeTracking.Keys.Contains(nameof(v.State.Value.Entity.Usersignin))
                       },
                       messageData: (s) => new ViewEventPublicationParameter(new object[] {s.State.Value.Entity.Id, s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},new StateEventInfo(s.Process.Id, RevolutionData.Context.View.Events.EntityChanged), s.Process, s.Source ), key: "entity Changes")
               }, commandInfo: new List<IViewModelEventCommand<IViewModel, IEvent>>(), orientation: typeof(IBodyViewModel), priority: 0);

            viewModel.State.Value = ProcessState;


            viewModel.Id = 6;
            viewModel.ChangeTracking.Add(nameof(ISignInInfo.Usersignin), "joe");
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(6, viewModel.Id);
        }

        private void handleEntityChanges(EntityChanges<ISignInInfo> entityChanges)
        {
            Assert.AreEqual(5, entityChanges.EntityId);
        }
        [TestMethod]
        public void InitalizeObserveableWithCommand()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<ISignInInfo>>(Source)
                .Subscribe(x => handleEntityChanges(x));
            dynamic viewModel = CreateLoginViewModel();

            viewModel.State.Value = ProcessState;
            viewModel.ChangeTracking.Add(nameof(ISignInInfo.Usersignin), "joe");
            viewModel.ChangeTracking.Add(nameof(ISignInInfo.Password), "test");
            viewModel.Commands["ValidateUserInfo"].Execute();
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(5, viewModel.Id);
        }

        [TestMethod]
        public void TestCommandPredicate()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<ISignInInfo>>(Source)
                .Subscribe(x => handleEntityChanges(x));
            dynamic viewModel = CreateLoginViewModel();

            viewModel.State.Value = ProcessState;
            viewModel.ChangeTracking.Add(nameof(ISignInInfo.Usersignin), "joe");
            viewModel.ChangeTracking.Add(nameof(ISignInInfo.Password), "test");
            viewModel.Commands["ValidateUserInfo"].Execute();
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(5, viewModel.Id);
            
        }

        private static SigninViewModel CreateLoginViewModel()
        {
            
            var viewModel = new SigninViewModel(
                process: TestProcess,
                viewInfo: new ViewInfo("", "", ""),
                eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
                eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>(),
                commandInfo: new List<IViewModelEventCommand<IViewModel,IEvent>>()
                {
                    new ViewEventCommand<SigninViewModel, EntityChanges<ISignInInfo>>("ValidateUserInfo",
                        
                        subject: (s) => s.Commands.GetValueOrNull("ValidateUserInfo").AsObservable(),
                        commandPredicate: new List<Func<SigninViewModel, bool>>()
                        {
                            (v) => v.ChangeTracking.Keys.Count > 2,
                            v => v.ChangeTracking.Values.Contains(nameof(ISignInInfo.Password)) && v.ChangeTracking.Values.Contains(nameof(ISignInInfo.Usersignin)), 
                        },
                        messageData: s => new ViewEventCommandParameter(new object[] {s.State.Value.Entity.Id,s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},new StateCommandInfo(s.Process.Id, RevolutionData.Context.View.Commands.ChangeEntity),s.Process,s.Source))
                }, orientation: typeof(IBodyViewModel), priority: 0);
            return viewModel;
        }
    }
}
