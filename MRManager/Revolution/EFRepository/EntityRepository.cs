using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using SystemInterfaces;
using Common;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using RevolutionEntities.Process;
using Utilities;

namespace EFReposi
{
    public class EntityRepository<TEntity,TDBEntity, TDBContext>:IProcessSource where TEntity:IEntity where TDBContext : DbContext, new() where TDBEntity:class, IEntity
    {
        public  ISystemSource Source => new Source(Guid.NewGuid(), $"EntityRepository:<{typeof(TEntity).GetFriendlyName()},{typeof(TDBEntity).GetFriendlyName()},{typeof(TDBContext).GetFriendlyName()}>" , new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        public  void Create(ICreateEntity<TEntity> msg )
        {
            Contract.Requires(msg.Entity.Id == 0);
            try
            {
                using (var ctx = new TDBContext())
                {
                    ctx.Set<TDBEntity>().Add((TDBEntity)(object) msg.Entity);
                    ctx.SaveChanges(true);
                    EventMessageBus.Current.Publish(new EntityCreated<TEntity>(msg.Entity, msg.Process, Source), Source);
                }
            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: msg.GetType(),
                    failedEventMessage: msg,
                    expectedEventType: typeof(IEntityCreated<TEntity>),
                    exception: ex,
                    source: Source), Source);
            }
        }

        public void Update(IUpdateEntity<TEntity> msg)
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    var entity = ctx.Set<TDBEntity>().First(x => x.Id == msg.EntityId);
                    entity.ApplyChanges(msg.Changes);
                    ctx.Set<TDBEntity>().Update(entity);

                    ctx.SaveChanges(true);
                    EventMessageBus.Current.Publish(new EntityUpdated<TEntity>((TEntity)(object)entity, msg.Process, Source), Source);
                }
            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType:msg.GetType(),
                    failedEventMessage: msg, 
                    expectedEventType: typeof(IEntityUpdated<TEntity>),
                    exception: ex,
                    source: Source), Source);
            }

        }

        public void Delete(IDeleteEntity<TEntity> msg )
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    if (msg.EntityId != 0)
                    {
                        var entity = ctx.Set<TDBEntity>().Find(msg.EntityId);
                        ctx.Set<TDBEntity>().Remove(entity);
                        ctx.SaveChanges(true);
                        EventMessageBus.Current.Publish(new EntityDeleted<TEntity>(msg.EntityId, msg.Process, Source), Source);
                    }
                }
            }
            catch (Exception ex)
            {
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: msg.GetType(),
                    failedEventMessage: msg,
                    expectedEventType: typeof(IEntityDeleted<TEntity>),
                    exception: ex,
                    source: Source), Source);
            }


        }


        public void GetEntityById(IGetEntityById<TEntity> msg )
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    var p = ctx.Set<TDBEntity>().First(x => x.Id == msg.EntityId);
                    EventMessageBus.Current.Publish(new EntityFound<TEntity>((TEntity)(object)p, msg.Process, Source), Source);
                }
            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: msg.GetType(),
                    failedEventMessage: msg,
                    expectedEventType: typeof(IEntityFound<TEntity>),
                    exception: ex,
                    source: Source), Source);
            }


        }

        public void GetEntityWithChanges(IGetEntityWithChanges<TEntity> msg )
        {

            using (var ctx = new TDBContext())
            {

                try
                {
                    var whereStr = msg.Changes.Aggregate("", (str, itm) => str + ($"{itm.Key} == \"{itm.Value}\" &&"));
                    whereStr = whereStr.TrimEnd('&');
                    if (string.IsNullOrEmpty(whereStr)) return;
                    var p = ctx.Set<TDBEntity>().Where(whereStr).First();
                    EventMessageBus.Current.Publish(new EntityWithChangesFound<TEntity>((TEntity)(object)p, msg.Changes, msg.Process, Source), Source);

                }
                catch (Exception ex)
                {

                    EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: msg.GetType(),
                    failedEventMessage: msg,
                    expectedEventType: typeof(IEntityWithChangesFound<TEntity>),
                    exception: ex,
                    source: Source), Source);
                }


            }

        }


        public void LoadEntitySet(ILoadEntitySet<TEntity> msg)
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    var loggerFactory = ctx.GetService<ILoggerFactory>();
                    loggerFactory.AddProvider(new MyLoggerProvider());

                    IQueryable<TDBEntity> rres = ctx.Set<TDBEntity>();
                    var res = rres.Select(x => (TEntity)(object)x).ToList(); //;

                    EventMessageBus.Current.Publish(new EntitySetLoaded<TEntity>(res, msg.Process, Source), Source);
                }

            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: msg.GetType(),
                    failedEventMessage: msg,
                    expectedEventType: typeof(IEntitySetLoaded<TEntity>),
                    exception: ex,
                    source: Source), Source);
            }
        }

        public void LoadEntitySetWithFilter(ILoadEntitySetWithFilter<TEntity> msg )
        {
            try
            {

                using (var ctx = new TDBContext())
                {
                    var loggerFactory = ctx.GetService<ILoggerFactory>();
                    loggerFactory.AddProvider(new MyLoggerProvider());


                    IQueryable<TDBEntity> rres = ctx.Set<TDBEntity>();
                    if (rres == null) return;


                    var rfilter = msg.Filter.Select(ExpressionConverter<TEntity>.ConvertExpressionType<TDBEntity, bool>).ToList();
                    rres = rfilter?.Aggregate(rres, (current, s) => current.Where(s));


                    var res = rres.Select(x => (TEntity)(object)x).ToList();
                    EventMessageBus.Current.Publish(new EntitySetWithFilterLoaded<TEntity>(res, msg.Process, Source), Source);
                }


            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: msg.GetType(),
                    failedEventMessage: msg,
                    expectedEventType: typeof(IEntitySetWithFilterLoaded<TEntity>),
                    exception: ex,
                    source: Source), Source);
            }
        }

        public void EntitySetWithFilterWithIncludesLoaded(ILoadEntitySetWithFilterWithIncludes<TEntity> msg )
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    var loggerFactory = ctx.GetService<ILoggerFactory>();
                    loggerFactory.AddProvider(new MyLoggerProvider());

                    var rIncludes = msg.Includes.Select(ExpressionConverter<TEntity>.ConvertExpressionType<TDBEntity, object>).ToList();
                    var rfilter = msg.Filter.Select(ExpressionConverter<TEntity>.ConvertExpressionType<TDBEntity, bool>).ToList();
                    IQueryable<TDBEntity> rres = ctx.Set<TDBEntity>();
                    if (rres == null) return;


                    rres = rIncludes.Aggregate(rres, (current, inc) => current.Include(inc));

                    rres = rfilter?.Aggregate(rres, (current, s) => current.Where(s));

                    var res = rres.Select(x => (TEntity)(object)x).ToList();
                    EventMessageBus.Current.Publish(new EntitySetWithFilterWithIncludesLoaded<TEntity>(res, msg.Includes, msg.Process, Source), Source);
                }



            }
            catch (Exception ex)
            {
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: msg.GetType(),
                    failedEventMessage: msg,
                    expectedEventType: typeof(IEntitySetWithFilterWithIncludesLoaded<TEntity>),
                    exception: ex,
                    source: Source), Source);
            }
        }
    }
}