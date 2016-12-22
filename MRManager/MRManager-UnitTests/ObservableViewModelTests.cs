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
//        [TestMethod]
//        public void CanAccessProperty()
//        {
//            var MsgSource = new MessageSource(this.ToString());

//#region subscribtions
//            var ServiceStartedViewEventSubscription = new ViewEventSubscription<ObservableViewModel<IAddressCities>, ServiceStarted<EntitySetWithFilterLoaded<IAddressCities>>>(
//                processId: 1,
//                eventPredicate: (e) => true,
//                actionPredicate: new List<Func<ObservableViewModel<IAddressCities>, ServiceStarted<EntitySetWithFilterLoaded<IAddressCities>>, bool>>
//                {
//                    (s, e) => true
//                },
//                action: (s, e) => EventMessageBus.Current.GetEvent<EntitySetWithFilterLoaded<IAddressCities>>(MsgSource).Subscribe(x => s.EntitySet = new ObservableList<IAddressCities>(x.Entities.Select(z => (IAddressCities)z).ToList())));

//            var citiesViewEventSubscription = new ViewEventSubscription<ObservableViewModel<IAddressCities>, CurrentEntityChanged<ICities>>(
//                processId: 1,
//                eventPredicate: (e) => e.EntityId != EntityStates.NullEntity,
//                actionPredicate: new List<Func<ObservableViewModel<IAddressCities>, CurrentEntityChanged<ICities>, bool>>
//                {
//                    (s, e) => s.CurrentEntity.Value.Id != e.EntityId
//                },
//                action: (s, e) =>
//                    s.FilterExpression =
//                        new ObservableList<Expression<Func<IAddressCities, bool>>>() {x => x.CityId == e.EntityId});
//            var addressesViewEventSubscription = new ViewEventSubscription<ObservableViewModel<IAddressCities>, CurrentEntityChanged<IAddresses>>(
//                processId: 1,
//                eventPredicate: (e) => e.EntityId != EntityStates.NullEntity,
//                actionPredicate: new List<Func<ObservableViewModel<IAddressCities>, CurrentEntityChanged<IAddresses>, bool>>
//                {
//                    (s, e) => s.CurrentEntity.Id != e.EntityId
//                },
//                action: (s, e) =>
//                    s.FilterExpression =
//                        new List<Expression<Func<IAddressCities, bool>>>() {x => x.Id == e.EntityId});
//#endregion


//            var eventsub = new List<IEventSubscription<IViewModel, IEvent>>()
//            {
//                ServiceStartedViewEventSubscription,
//                citiesViewEventSubscription,
//                addressesViewEventSubscription
//            };
//            var viewModel = new ObservableViewModel<IAddressCities>(
//              eventSubscriptions: eventsub

//               process: new SystemProcess(new Process(1,0, "Test Proces", "This is a Test", "T"),new MachineInfo("test", "test location", 2), new User(DesignDataContext.SampleData<IPersons>(), "test","joe") )
//               );

//            dynamic dynamicViewModel =new DynamicViewModel<ObservableViewModel<IAddressCities>>(viewModel);
//            dynamicViewModel.CurrentEntity = new AddressCities() {Id = 5,CityId = 10};
//           // var id = dynamicViewModel.GetValue("Id");
//            Assert.AreEqual(5, dynamicViewModel.Id);
//        }
    }
}
