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
using EventMessages.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using RevolutionEntities.Process;
using Utilities;

namespace EFRepository
{
    public class EntityRepository<TEntity,TDBEntity, TDBContext>:BaseRepository<EntityRepository<TEntity,TDBEntity, TDBContext>> where TEntity:IEntity where TDBContext : DbContext, new() where TDBEntity:class, IEntity, new()
    {

        public static void Create(ICreateEntity<TEntity> msg )
        {
            Contract.Requires(msg.Entity.Id == 0);
            try
            {
                using (var ctx = new TDBContext())
                {
                    ctx.Set<TDBEntity>().Add((TDBEntity)(object) msg.Entity);
                    ctx.SaveChanges(true);
                    EventMessageBus.Current.Publish(new EntityCreated<TEntity>(msg.Entity,new StateEventInfo(msg.Process.Id, RevolutionData.Context.Entity.Events.EntityCreated), msg.Process, Source), Source);
                }
            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntityCreated<TEntity>));
            }
        }

        public static void Update(IUpdateEntityWithChanges<TEntity> msg)
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    TDBEntity entity;//
                    if (msg.EntityId == 0)
                    {
                        entity = new TDBEntity();
                        ctx.Set<TDBEntity>().Add(entity);
                    }
                    else
                    {
                        entity = ctx.Set<TDBEntity>().FirstOrDefault(x => x.Id == msg.EntityId);
                    }

                    
                    entity.ApplyChanges(msg.Changes);
                   
                    ctx.SaveChanges(true);
                    EventMessageBus.Current.Publish(new EntityUpdated<TEntity>((TEntity)(object)entity,new StateEventInfo(msg.Process.Id, RevolutionData.Context.Entity.Events.EntityUpdated), msg.Process, Source), Source);
                }
            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntityUpdated<TEntity>));
            }

        }

        public static void Add(IAddOrGetEntityWithChanges<TEntity> msg)
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    TDBEntity entity;//
                    var whereStr = msg.Changes.Aggregate("", (str, itm) => str + ($"{itm.Key} == \"{itm.Value}\" &&"));
                    whereStr = whereStr.TrimEnd('&');
                    if (string.IsNullOrEmpty(whereStr)) return;
                    entity = ctx.Set<TDBEntity>().Where(whereStr).FirstOrDefault();

                    if (entity != null)
                    {
                        EventMessageBus.Current.Publish(new EntityUpdated<TEntity>((TEntity)(object)entity, new StateEventInfo(msg.Process.Id, RevolutionData.Context.Entity.Events.EntityUpdated), msg.Process, Source), Source);
                        return;
                    }

                    
                        entity = new TDBEntity();
                        ctx.Set<TDBEntity>().Add(entity);
                   
                    entity.ApplyChanges(msg.Changes);

                    ctx.SaveChanges(true);
                    EventMessageBus.Current.Publish(new EntityUpdated<TEntity>((TEntity)(object)entity, new StateEventInfo(msg.Process.Id, RevolutionData.Context.Entity.Events.EntityUpdated), msg.Process, Source), Source);
                }
            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntityUpdated<TEntity>));
            }

        }

        public static void Delete(IDeleteEntity<TEntity> msg )
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
                        EventMessageBus.Current.Publish(new EntityDeleted<TEntity>(msg.EntityId,new StateEventInfo(msg.Process.Id, RevolutionData.Context.Entity.Events.EntityDeleted), msg.Process, Source), Source);
                    }
                }
            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntityDeleted<TEntity>));
            }


        }


        public static void GetEntityById(IGetEntityById<TEntity> msg )
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    var p = ctx.Set<TDBEntity>().AsNoTracking().First(x => x.Id == msg.EntityId);
                    EventMessageBus.Current.Publish(new EntityFound<TEntity>((TEntity)(object)p, new StateEventInfo(msg.Process.Id, RevolutionData.Context.Entity.Events.EntityFound), msg.Process, Source), Source);
                }
            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntityFound<TEntity>));
            }


        }

        public static void GetEntityWithChanges(IGetEntityWithChanges<TEntity> msg )
        {

            using (var ctx = new TDBContext())
            {

                try
                {
                    var whereStr = msg.Changes.Aggregate("", (str, itm) => str + ($"{itm.Key} == \"{itm.Value}\" &&"));
                    whereStr = whereStr.TrimEnd('&');
                    if (string.IsNullOrEmpty(whereStr)) return;
                    var p = ctx.Set<TDBEntity>().AsNoTracking().Where(whereStr).FirstOrDefault();
                    EventMessageBus.Current.Publish(new EntityWithChangesFound<TEntity>((TEntity)(object)p, msg.Changes, new StateEventInfo(msg.Process.Id, RevolutionData.Context.Entity.Events.EntityFound), msg.Process, Source), Source);

                }
                catch (Exception ex)
                {
                    PublishProcesError(msg, ex, typeof(IEntityWithChangesFound<TEntity>));
                }


            }

        }


        public static void LoadEntitySet(ILoadEntitySet<TEntity> msg)
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    var loggerFactory = ctx.GetService<ILoggerFactory>();
                    loggerFactory.AddProvider(new MyLoggerProvider());

                    IQueryable<TDBEntity> rres = ctx.Set<TDBEntity>().AsNoTracking();
                    var res = rres.OrderByDescending(x => x.Id).Select(x => (TEntity)(object)x).ToList(); //;

                    EventMessageBus.Current.Publish(new EntitySetLoaded<TEntity>(res, new StateEventInfo(msg.Process.Id, RevolutionData.Context.Entity.Events.EntitySetLoaded), msg.Process, Source), Source);
                }

            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntitySetLoaded<TEntity>));
            }
        }

        public static void LoadEntitySetWithFilter(ILoadEntitySetWithFilter<TEntity> msg )
        {
            try
            {

                using (var ctx = new TDBContext())
                {
                    var loggerFactory = ctx.GetService<ILoggerFactory>();
                    loggerFactory.AddProvider(new MyLoggerProvider());


                    IQueryable<TDBEntity> rres = ctx.Set<TDBEntity>().AsNoTracking();
                    if (rres == null) return;


                    var rfilter = msg.Filter.Select(ExpressionConverter<TEntity>.ConvertExpressionType<TDBEntity, bool>).ToList();
                    rres = rfilter?.Aggregate(rres, (current, s) => current.Where(s));


                    var res = rres.OrderByDescending(x => x.Id).Select(x => (TEntity)(object)x).ToList();
                    EventMessageBus.Current.Publish(new EntitySetWithFilterLoaded<TEntity>(res, new StateEventInfo(msg.Process.Id, RevolutionData.Context.Entity.Events.EntitySetLoaded), msg.Process, Source), Source);
                }


            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntitySetWithFilterLoaded<TEntity>));
            }
        }

        public static void EntitySetWithFilterWithIncludesLoaded(ILoadEntitySetWithFilterWithIncludes<TEntity> msg )
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    var loggerFactory = ctx.GetService<ILoggerFactory>();
                    loggerFactory.AddProvider(new MyLoggerProvider());

                    var rIncludes = msg.Includes.Select(ExpressionConverter<TEntity>.ConvertExpressionType<TDBEntity, object>).ToList();
                    var rfilter = msg.Filter.Select(ExpressionConverter<TEntity>.ConvertExpressionType<TDBEntity, bool>).ToList();
                    IQueryable<TDBEntity> rres = ctx.Set<TDBEntity>().AsNoTracking();
                    if (rres == null) return;


                    rres = rIncludes.Aggregate(rres, (current, inc) => current.Include(inc));

                    rres = rfilter?.Aggregate(rres, (current, s) => current.Where(s));

                    var res = rres.OrderByDescending(x => x.Id).Select(x => (TEntity)(object)x).ToList();
                    EventMessageBus.Current.Publish(new EntitySetWithFilterWithIncludesLoaded<TEntity>(res, msg.Includes, new StateEventInfo(msg.Process.Id, RevolutionData.Context.Entity.Events.EntitySetLoaded), msg.Process, Source), Source);
                }



            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntitySetWithFilterWithIncludesLoaded<TEntity>));
            }
        }


    }
}

