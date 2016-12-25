//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using SystemInterfaces;
//using Core.Common.UI;
//using DesignTime;
//using EF.Entities;
//using EventMessages;
//using Interfaces;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using RevolutionEntities.Process;
//using RevolutionEntities.ViewModels;
//using Utilities;
//using ViewModels;
//using Process = RevolutionEntities.Process.Process;

//namespace MRManager_UnitTests
//{
//    [TestClass]
//    public class DynamicViewModelTests
//    {
//        [TestMethod]
//        public void CanAccessProperty()
//        {
//            var citiesViewEventSubscription = new ViewEventSubscription<WriteEntityViewModel<IAddressCities>, CurrentEntityChanged<ICities>>(
//                processId: 1,
//                eventPredicate: (e) => e.EntityId != EntityStates.NullEntity,
//                actionPredicate: new List<Func<WriteEntityViewModel<IAddressCities>, CurrentEntityChanged<ICities>, bool>>
//                {
//                    (s, e) => s.CurrentEntity.Id != e.EntityId
//                },
//                action: (s, e) =>
//                    s.FilterExpression =
//                        new List<Expression<Func<IAddressCities, bool>>>() {x => x.CityId == e.EntityId});
//            var addressesViewEventSubscription = new ViewEventSubscription<WriteEntityViewModel<IAddressCities>, CurrentEntityChanged<IAddresses>>(
//                processId: 1,
//                eventPredicate: (e) => e.EntityId != EntityStates.NullEntity,
//                actionPredicate: new List<Func<WriteEntityViewModel<IAddressCities>, CurrentEntityChanged<IAddresses>, bool>>
//                {
//                    (s, e) => s.CurrentEntity.Id != e.EntityId
//                },
//                action: (s, e) =>
//                    s.FilterExpression =
//                        new List<Expression<Func<IAddressCities, bool>>>() {x => x.Id == e.EntityId});

//            var eventsub = new List<IViewModelEventSubscription<IViewModel, IEvent>>()
//            {
//                citiesViewEventSubscription,
//                addressesViewEventSubscription
//            };
//            var viewModel = new WriteEntityViewModel<IAddressCities>(
//               createEntityAction: () => new AddressCities()
//                                        {
//                                            CityId = CacheViewModel<ICities>.Instance.CurrentEntity.Id,
//                                            Id = CacheViewModel<IAddresses>.Instance.CurrentEntity.Id,
//                                            RowState = DataInterfaces.RowState.Added},
//               createNullEntityAction: () => new AddressCities() { Id = EntityStates.NullEntity },
//               eventSubscriptionsActions: eventsub,
//               eventPublications: new List<IViewModelEventPublication<IViewModel, IEvent>>(), 
//               commandInfo:new List<IViewModelEventCommand<IViewModel, IEvent>>(), 
//               process: new SystemProcess(new Process(1,0, "Test Proces", "This is a Test", "T"),new MachineInfo("test", "test location", 2), new User(DesignDataContext.SampleData<IPersons>(), "test","joe") )
//               );

//            dynamic dynamicViewModel =new DynamicViewModel<WriteEntityViewModel<IAddressCities>>(viewModel);
//            dynamicViewModel.CurrentEntity = new AddressCities() {Id = 5,CityId = 10};
//           // var id = dynamicViewModel.GetValue("Id");
//            Assert.AreEqual(5, dynamicViewModel.Id);
//        }
//    }
//}
