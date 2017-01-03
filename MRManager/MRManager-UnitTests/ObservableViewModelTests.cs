using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using SystemInterfaces;
using CommonMessages;
using Core.Common.UI;
using EF.Entities;
using EventAggregator;
using EventMessages;
using Interfaces;
using JB.Reactive.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewModel.Interfaces;
using ViewModels;
using Process = RevolutionEntities.Process.Process;

namespace MRManager_UnitTests
{
    [TestClass]
    public class ObservableViewModelTests
    {
        protected SourceMessage SourceMessage => new SourceMessage(new MessageSource(this.ToString()), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        [TestMethod]
        public void InitalizeObserveableWithNoSubscriptions()
        {
            var MsgSource = new MessageSource(this.ToString());
            dynamic viewModel = new LoginViewModel(process: new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T", new Agent("TestManager")),SourceMessage.MachineInfo),
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
            EventMessageBus.Current.GetEvent<EntityChanges<IUserSignIn>>(SourceMessage)
                .Subscribe(x => handleEntityChanges(x));
            var viewModel = new LoginViewModel(process: new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T", new Agent("TestManager")), SourceMessage.MachineInfo),
               eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
               eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>()
               {
                   new ViewEventPublication<LoginViewModel, EntityChanges<IUserSignIn>>(
                       subject:(s) => s.ChangeTracking.DictionaryChanges,
                       subjectPredicate: new List<Func<LoginViewModel, bool>>(),
                       messageData:(s) => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value.Id, s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)}, s.Process, s.SourceMessage ))
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
            EventMessageBus.Current.GetEvent<EntityChanges<IUserSignIn>>(SourceMessage)
                .Subscribe(x => handleEntityChanges(x));
            var viewModel = new LoginViewModel(process: new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T", new Agent("TestManager")), SourceMessage.MachineInfo),
               eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
               eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>()
               {
                   new ViewEventPublication<LoginViewModel, EntityChanges<IUserSignIn>>(
                       subject:(s) => s.ChangeTracking.DictionaryChanges,
                       subjectPredicate: new List<Func<LoginViewModel, bool>>()
                       {
                           v => v.ChangeTracking.Keys.Contains(nameof(v.CurrentEntity.Value.Username))
                       },
                       messageData:(s) => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value.Id, s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)}, s.Process, s.SourceMessage ))
               }, commandInfo: new List<IViewModelEventCommand<IViewModel, IEvent>>(), orientation: typeof(IBodyViewModel));

            dynamic dynamicViewModel = viewModel;
            dynamicViewModel.CurrentEntity.Value = new UserSignIn() { Id = 5, Username = "Joe", Password = "Test" };


            dynamicViewModel.Id = 6;
            dynamicViewModel.ChangeTracking.Add(nameof(IUserSignIn.Username), "joe");
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(6, dynamicViewModel.Id);
        }

        private void handleEntityChanges(EntityChanges<IUserSignIn> entityChanges)
        {
            Assert.AreEqual(5, entityChanges.EntityId);
        }
        [TestMethod]
        public void InitalizeObserveableWithCommand()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<IUserSignIn>>(SourceMessage)
                .Subscribe(x => handleEntityChanges(x));
            var viewModel = CreateLoginViewModel();

            dynamic dynamicViewModel = viewModel;
            dynamicViewModel.CurrentEntity.Value = new UserSignIn() { Id = 5 };
            dynamicViewModel.ChangeTracking.Add(nameof(IUserSignIn.Username), "joe");
            dynamicViewModel.ChangeTracking.Add(nameof(IUserSignIn.Username), "test");
            dynamicViewModel.Commands["ValidateUserInfo"].Execute();
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(5, dynamicViewModel.Id);
        }

        [TestMethod]
        public void TestCommandPredicate()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<IUserSignIn>>(SourceMessage)
                .Subscribe(x => handleEntityChanges(x));
            var viewModel = CreateLoginViewModel();

            dynamic dynamicViewModel = viewModel;
            dynamicViewModel.CurrentEntity.Value = new UserSignIn() { Id = 5, Username = "Joe", Password = "Test" };
            dynamicViewModel.ChangeTracking.Add(nameof(IUserSignIn.Username), "joe");
            dynamicViewModel.ChangeTracking.Add(nameof(IUserSignIn.Password), "test");
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
                        v =>
                            v.ChangeTracking.WhenAny(x => x.Keys,
                                x => x.Value.Contains(nameof(IUserSignIn.Password)) && x.Value.Contains(nameof(IUserSignIn.Username))), 
                        subject: (s) => ((ReactiveCommand<IViewModel, Unit>) s.Commands["ValidateUserInfo"]).AsObservable(),
                        subjectPredicate: new List<Func<LoginViewModel, bool>>()
                        {
                            (v) => v.ChangeTracking.Keys.Count > 2
                        },
                        messageData: s => new ViewEventPublicationParameter(new object[] {s.CurrentEntity.Value.Id,s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)},s.Process,s.SourceMessage))
                }, orientation: typeof(IBodyViewModel));
            return viewModel;
        }
    }
}
