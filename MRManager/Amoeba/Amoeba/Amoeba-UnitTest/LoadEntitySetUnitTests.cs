
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using SystemMessages;
using CommonMessages;
using DataEntites;
using EF.Entities;
using EventAggregator;
using EventMessages;
using Interfaces;
using JB.Collections.Reactive;
using ReactiveUI;
using System.Linq;
using EF.DBContexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NH.DBContext;

namespace Amoeba_UnitTest
{
    [TestClass]
    public class LoadEntitySetUnitTests
    {
        private static MessageSource msgSource => new MessageSource(typeof(LoadEntitySetUnitTests).ToString());
        [TestMethod]
        public void LoadIEntity()
        {
            var t = new AmoebaDBContext().GetType().Assembly;
            var x1 = new EFEntity().GetType().Assembly;
            var d = new NHDBContext();
            BootStrapper.BootStrapper.Instance.StartUp(d, t, x1);
            EventMessageBus.Current.GetEvent<EntitySetLoaded<IEntities>>(msgSource).Subscribe(x => handleEntitiesLoaded(x.Entities));
            EventMessageBus.Current.Publish(new LoadEntitySet<IEntities>(msgSource), msgSource);
        }

        private static void handleEntitiesLoaded(IEnumerable<IEntities> entities)
        {
            Assert.IsTrue(entities.Any());
        }
    }
}
