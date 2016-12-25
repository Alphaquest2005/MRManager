using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;
using SystemInterfaces;
using SystemMessages;
using CommonMessages;
using Core.Common.UI;
using DesignTime;
using EF.Entities;
using EventAggregator;
using EventMessages;
using Interfaces;
using JB.Collections.Reactive;
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
    public class ObservableViewModelTests
    {
        [TestMethod]
        public void InitalizeObserveableWithNoSubscriptions()
        {
            var MsgSource = new MessageSource(this.ToString());
            var viewModel = new LoginViewModel(
              eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(), 
               process: new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T"), new MachineInfo("test", "test location", 2), new User(DesignDataContext.SampleData<IPersons>(), "test", "joe")),
               eventPublications:new List<IViewModelEventPublication<IViewModel, IEvent>>()
               {
                   
               },
               commandInfo: new List<IViewModelEventCommand<IViewModel,IEvent>>()
               );

            dynamic dynamicViewModel = viewModel;
            dynamicViewModel.CurrentEntity.Value = new Persons() { Id = 5 };
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(5, dynamicViewModel.Id);
        }

        [TestMethod]
        public void InitalizeObserveableWithPublications()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<IUserSignIn>>(new MessageSource(this.ToString()))
                .Subscribe(x => handleEntityChanges(x));
            var viewModel = new LoginViewModel(
              eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
               process: new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T"), new MachineInfo("test", "test location", 2), new User(DesignDataContext.SampleData<IPersons>(), "test", "joe")),
               eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>()
                                   {
                                        new ViewEventPublication<LoginViewModel, EntityChanges<IUserSignIn>>(
                                                        subject:(s) => s.ChangeTracking.DictionaryChanges,
                                                        subjectPredicate: new List<Func<LoginViewModel, bool>>(),
                                                        messageData:new List<Func<LoginViewModel, dynamic>>()
                                                        {
                                                            (s) => s.CurrentEntity.Value.Id,
                                                            (s) => s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                                                        }
                                                        )
                                    },
               commandInfo:new List<IViewModelEventCommand<IViewModel,IEvent>>()
               );

            dynamic dynamicViewModel = viewModel;
            dynamicViewModel.CurrentEntity.Value = new Persons() { Id = 5 };


            dynamicViewModel.Id = 6;
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(6, dynamicViewModel.Id);
        }

        [TestMethod]
        public void InitalizeObserveableWithPublicationsSubjectPredicate()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<IUserSignIn>>(new MessageSource(this.ToString()))
                .Subscribe(x => handleEntityChanges(x));
            var viewModel = new LoginViewModel(
              eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
               process: new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T"), new MachineInfo("test", "test location", 2), new User(DesignDataContext.SampleData<IPersons>(), "test", "joe")),
               eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>()
                                   {
                                        new ViewEventPublication<LoginViewModel, EntityChanges<IUserSignIn>>(
                                                        subject:(s) => s.ChangeTracking.DictionaryChanges,
                                                        subjectPredicate: new List<Func<LoginViewModel, bool>>()
                                                        {
                                                            v => v.ChangeTracking.Keys.Contains("UserName")
                                                        },
                                                        messageData:new List<Func<LoginViewModel, dynamic>>()
                                                        {
                                                            (s) => s.CurrentEntity.Value.Id,
                                                            (s) => s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                                                        }
                                                        )
                                    },
               commandInfo: new List<IViewModelEventCommand<IViewModel, IEvent>>()
               );

            dynamic dynamicViewModel = viewModel;
            dynamicViewModel.CurrentEntity.Value = new Persons() { Id = 5 };


            dynamicViewModel.Id = 6;
            dynamicViewModel.ChangeTracking.Add("UserName", "joe");
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
            EventMessageBus.Current.GetEvent<EntityChanges<IUserSignIn>>(new MessageSource(this.ToString()))
                .Subscribe(x => handleEntityChanges(x));
            var viewModel = CreateLoginViewModel();

            dynamic dynamicViewModel = viewModel;
            dynamicViewModel.CurrentEntity.Value = new Persons() { Id = 5 };
            dynamicViewModel.ChangeTracking.Add("UserName", "joe");
            dynamicViewModel.ChangeTracking.Add("Password", "test");
            dynamicViewModel.Commands["ValidateUserInfo"].Execute();
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(5, dynamicViewModel.Id);
        }

        [TestMethod]
        public void TestCommandPredicate()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<IUserSignIn>>(new MessageSource(this.ToString()))
                .Subscribe(x => handleEntityChanges(x));
            var viewModel = CreateLoginViewModel();

            dynamic dynamicViewModel = viewModel;
            dynamicViewModel.CurrentEntity.Value = new Persons() { Id = 5 };
            dynamicViewModel.ChangeTracking.Add("UserName", "joe");
            dynamicViewModel.ChangeTracking.Add("Password", "test");
            dynamicViewModel.Commands["ValidateUserInfo"].Execute();
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(5, dynamicViewModel.Id);
        }

        private static LoginViewModel CreateLoginViewModel()
        {
            var viewModel = new LoginViewModel(
                eventSubscriptions: new List<IViewModelEventSubscription<IViewModel, IEvent>>(),
                process:
                    new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T"),
                        new MachineInfo("test", "test location", 2),
                        new User(DesignDataContext.SampleData<IPersons>(), "test", "joe")),
                eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>(),
                commandInfo: new List<IViewModelEventCommand<IViewModel,IEvent>>()
                {
                    new ViewEventCommand<LoginViewModel, EntityChanges<IUserSignIn>>("ValidateUserInfo",
                        v =>
                            v.ChangeTracking.WhenAny(x => x.Keys,
                                x => x.Value.Contains("Password") && x.Value.Contains("UserName")), 
                        subject: (s) => ((ReactiveCommand<IViewModel, Unit>) s.Commands["ValidateUserInfo"]).AsObservable(),
                        subjectPredicate: new List<Func<LoginViewModel, bool>>()
                        {
                            (v) => v.ChangeTracking.Keys.Count > 2
                        },
                        messageData: new List<Func<LoginViewModel, dynamic>>()
                        {
                            (s) => s.CurrentEntity?.Value.Id,
                            (s) => s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                        })
                }
                );
            return viewModel;
        }
    }
}
