using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SystemInterfaces;
using SystemMessages;
using CommonMessages;
using Core.Common.UI;
using DesignTimeData;
using EF.Entities;
using EventAggregator;
using EventMessages;
using Interfaces;
using JB.Collections.Reactive;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using Utilities;
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
              eventSubscriptions: new List<IEventSubscription<IViewModel, IEvent>>(), 
               process: new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T"), new MachineInfo("test", "test location", 2), new User(DesignDataContext.SampleData<IPersons>(), "test", "joe")),
               eventPublications:new List<IEventPublication<IViewModel, IEvent>>()
               {
                   
               },
               commands:new List<IViewCommand<IViewModel, IEvent>>()
               );

            dynamic dynamicViewModel = new DynamicViewModel<ObservableViewModel<IPersons>>(viewModel);
            dynamicViewModel.CurrentEntity.Value = new Persons() { Id = 5 };
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(5, dynamicViewModel.Id);
        }

        [TestMethod]
        public void InitalizeObserveableWithPublications()
        {
            EventMessageBus.Current.GetEvent<EntityChanges<IPersons>>(new MessageSource(this.ToString()))
                .Subscribe(x => handleEntityChanges(x));
            var viewModel = new LoginViewModel(
              eventSubscriptions: new List<IEventSubscription<IViewModel, IEvent>>(),
               process: new SystemProcess(new Process(1, 0, "Test Proces", "This is a Test", "T"), new MachineInfo("test", "test location", 2), new User(DesignDataContext.SampleData<IPersons>(), "test", "joe")),
               eventPublications: new List<IEventPublication<IViewModel, IEvent>>()
                                   {
                                        new ViewEventPublication<LoginViewModel, EntityChanges<IPersons>>(
                                                        subject:(s) => s.ChangeTracking.DictionaryChanges,
                                                        subjectPredicate: new List<Func<LoginViewModel, IObservable<dynamic>, bool>>(),
                                                        messageData:new List<Func<LoginViewModel, dynamic>>()
                                                        {
                                                            (s) => s.CurrentEntity.Value.Id,
                                                            (s) => s.ChangeTracking.ToDictionary(x => x.Key, x => x.Value)
                                                        }),
                                   },
               commands: new List<IViewCommand<IViewModel, IEvent>>()
               );

            dynamic dynamicViewModel = new DynamicViewModel<ObservableViewModel<IPersons>>(viewModel);
            dynamicViewModel.CurrentEntity.Value = new Persons() { Id = 5 };


            dynamicViewModel.Id = 6;
            // var id = dynamicViewModel.GetValue("Id");
            Assert.AreEqual(6, dynamicViewModel.Id);
        }

        private void handleEntityChanges(EntityChanges<IPersons> entityChanges)
        {
            Assert.AreEqual(5, entityChanges.EntityId);
        }
    }
}
