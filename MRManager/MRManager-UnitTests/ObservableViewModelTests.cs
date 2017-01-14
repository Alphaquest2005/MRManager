using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using SystemInterfaces;
using Common;
using CommonMessages;
using Core.Common.UI;
using DataEntites;
using EF.Entities;
using EventAggregator;
using EventMessages;
using Interfaces;
using JB.Reactive.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactiveUI;
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
        public ISystemSource Source => new Source(Guid.NewGuid(), $"EntityRepository:<{typeof(ObservableViewModelTests).GetFriendlyName()}>",new SourceType(typeof(ObservableViewModelTests)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        [TestMethod]
        public void InitalizeObserveableWithNoSubscriptions()
        {
            var MsgSource = new MessageSource(this.ToString());
            dynamic viewModel = new LoginViewModel(process: new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T", new Agent("TestManager")),Source.MachineInfo),
               eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
               eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>()
               {
                   
               }, commandInfo: new List<IViewModelEventCommand<IViewModel,IEvent>>(), orientation: typeof(IBodyViewModel));
            viewModel.CurrentEntity.Value = new UserSignIn() { Id = 5, Username = "Joe", Password = "Test"};
            // var id = dynamicViewModel.GetValue("Id");
           Assert.AreEqual(5, viewModel.Id);
        }

        [TestMethod]
        public void InitalizeObserveableWithPublications()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<ISignInInfo>>(Source)
                .Subscribe(x => handleEntityChanges(x));
            var viewModel = new LoginViewModel(process: new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T", new Agent("TestManager")), Source.MachineInfo),
               eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
               eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>()
               {
                   new ViewEventPublication<LoginViewModel, EntityChanges<IUserSignIn>>(
                       subject: (s) => s.ChangeTracking.DictionaryChanges,
                       subjectPredicate: new List<Func<LoginViewModel, bool>>(),
                       messageData: (s) => new ViewEventPublicationParameter(new object[] {s.State.Value.Entity.Id, s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},new StateEventInfo(s.Process.Id, RevolutionData.Context.View.Events.EntityChanged), s.Process, s.Source ), key: "Entity Changes")
               }, commandInfo: new List<IViewModelEventCommand<IViewModel,IEvent>>(), orientation: typeof(IBodyViewModel));

            dynamic dynamicViewModel = viewModel;
            dynamicViewModel.CurrentEntity.Value = new UserSignIn() { Id = 5, Username = "Joe", Password = "Test"};


            dynamicViewModel.Id = 6;
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(6, dynamicViewModel.Id);
        }

        [TestMethod]
        public void InitalizeObserveableWithPublicationsSubjectPredicate()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<ISignInInfo>>(Source)
                .Subscribe(x => handleEntityChanges(x));
            var viewModel = new LoginViewModel(process: new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T", new Agent("TestManager")), Source.MachineInfo),
               eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
               eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>()
               {
                   new ViewEventPublication<LoginViewModel, EntityChanges<ISignInInfo>>(
                       subject: (s) => s.ChangeTracking.DictionaryChanges,
                       subjectPredicate: new List<Func<LoginViewModel, bool>>()
                       {
                           v => v.ChangeTracking.Keys.Contains(nameof(v.State.Value.Entity.Usersignin))
                       },
                       messageData: (s) => new ViewEventPublicationParameter(new object[] {s.State.Value.Entity.Id, s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},new StateEventInfo(s.Process.Id, RevolutionData.Context.View.Events.EntityChanged), s.Process, s.Source ), key: "entity Changes")
               }, commandInfo: new List<IViewModelEventCommand<IViewModel, IEvent>>(), orientation: typeof(IBodyViewModel));

            dynamic dynamicViewModel = viewModel;
            dynamicViewModel.CurrentEntity.Value = new UserSignIn() { Id = 5, Username = "Joe", Password = "Test" };


            dynamicViewModel.Id = 6;
            dynamicViewModel.ChangeTracking.Add(nameof(IUserSignIn.Username), "joe");
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(6, dynamicViewModel.Id);
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
            var viewModel = CreateLoginViewModel();

            dynamic dynamicViewModel = viewModel;
            dynamicViewModel.CurrentEntity.Value = new UserSignIn() { Id = 5 };
            dynamicViewModel.ChangeTracking.Add(nameof(ISignInInfo.Usersignin), "joe");
            dynamicViewModel.ChangeTracking.Add(nameof(ISignInInfo.Usersignin), "test");
            dynamicViewModel.Commands["ValidateUserInfo"].Execute();
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(5, dynamicViewModel.Id);
        }

        [TestMethod]
        public void TestCommandPredicate()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<ISignInInfo>>(Source)
                .Subscribe(x => handleEntityChanges(x));
            var viewModel = CreateLoginViewModel();

            dynamic dynamicViewModel = viewModel;
            dynamicViewModel.CurrentEntity.Value = new UserSignIn() { Id = 5, Username = "Joe", Password = "Test" };
            dynamicViewModel.ChangeTracking.Add(nameof(ISignInInfo.Usersignin), "joe");
            dynamicViewModel.ChangeTracking.Add(nameof(ISignInInfo.Password), "test");
            dynamicViewModel.Commands["ValidateUserInfo"].Execute();
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(5, dynamicViewModel.Id);
            
        }

        private static LoginViewModel CreateLoginViewModel()
        {
            
            var viewModel = new LoginViewModel(process: new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T", new Agent("TestManager")),
                new MachineInfo("test", 2)),
                eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
                eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>(), commandInfo: new List<IViewModelEventCommand<IViewModel,IEvent>>()
                {
                    new ViewEventCommand<LoginViewModel, EntityChanges<IUserSignIn>>("ValidateUserInfo",
                        
                        subject: (s) => ((ReactiveCommand<IViewModel, Unit>) s.Commands["ValidateUserInfo"]).AsObservable(),
                        commandPredicate: new List<Func<LoginViewModel, bool>>()
                        {
                            (v) => v.ChangeTracking.Keys.Count > 2,
                            v => v.ChangeTracking.Values.Contains(nameof(IUserSignIn.Password)) && v.ChangeTracking.Values.Contains(nameof(IUserSignIn.Username)), 
                        },
                        messageData: s => new ViewEventCommandParameter(new object[] {s.State.Value.Entity.Id,s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},new StateCommandInfo(s.Process.Id, RevolutionData.Context.View.Commands.ChangeEntity),s.Process,s.Source))
                }, orientation: typeof(IBodyViewModel));
            return viewModel;
        }
    }
}
