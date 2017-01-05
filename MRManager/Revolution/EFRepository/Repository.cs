using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using SystemInterfaces;
using Common;
using CommonMessages;
using EventAggregator;
using EventMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using RevolutionEntities.Process;
using Utilities;


namespace EFRepository
{
    public class Repository<TEntity, TDBContext> where TEntity: class, IEntity where TDBContext : DbContext, new()
    {
        public static ISourceMessage SourceMessage => new SourceMessage(new MessageSource(typeof(Repository<TEntity, TDBContext>).ToString()), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        public static void Create(TEntity entity, ISystemProcess process)
        {
            Contract.Requires(entity.Id == 0);
            try
            {
                using (var ctx = new TDBContext())
                    {
                        if (entity.Id == 0)
                        {
                            ctx.Set<TEntity>().Add(entity);
                            ctx.SaveChanges(true);
                        }
                        
                        
                        EventMessageBus.Current.Publish(new EntityCreated<TEntity>(entity, process, SourceMessage), SourceMessage);
                    }
            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(ICreateEntity<TEntity>),
                    failedEventMessage: null,
                    expectedEventType: typeof(IEntityCreated<TEntity>),
                    exception: ex,
                    SourceMsg: SourceMessage), SourceMessage);
            }
        }

        public static void Update(int entityId, Dictionary<string, object> changes, ISystemProcess process)
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    var entity = ctx.Set<TEntity>().First(x => x.Id == entityId);
                    entity.ApplyChanges(changes);
                    ctx.Set<TEntity>().Update(entity);

                    ctx.SaveChanges(true);
                    EventMessageBus.Current.Publish(new EntityUpdated<TEntity>(entity, process, SourceMessage), SourceMessage);
                }
            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(IUpdateEntity<TEntity>),
                    failedEventMessage: null,
                    expectedEventType: typeof(IEntityCreated<TEntity>),
                    exception: ex,
                    SourceMsg: SourceMessage), SourceMessage);
            }
           
        }

        public static void Delete(int entityId, ISystemProcess process)
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    if (entityId != 0)
                    {
                        var entity = ctx.Set<TEntity>().Find(entityId);
                        ctx.Set<TEntity>().Remove(entity);
                        ctx.SaveChanges(true);
                        EventMessageBus.Current.Publish(new EntityDeleted<TEntity>(entityId, process, SourceMessage), SourceMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(IDeleteEntity<TEntity>),
                    failedEventMessage: null,
                    expectedEventType: typeof(IEntityCreated<TEntity>),
                    exception: ex,
                    SourceMsg: SourceMessage), SourceMessage);
            }


        }


        public static void GetEntityById(int entityId, ISystemProcess process)
        {
            try
            {
                using (var ctx = new TDBContext())
                {
                    var p = ctx.Set<TEntity>().First(x => x.Id == entityId);
                    EventMessageBus.Current.Publish(new EntityFound<TEntity>(p, process, SourceMessage), SourceMessage);
                }
            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(
                    new ProcessEventFailure(failedEventType: typeof (IGetEntityById<TEntity>),
                        failedEventMessage: null,
                        expectedEventType: typeof (IEntityFound<TEntity>),
                        exception: ex,
                        SourceMsg: SourceMessage), SourceMessage);
            }


        }

        public static void GetEntityWithChanges(int entityId, Dictionary<string, object> changes, ISystemProcess process)
        {

            using (var ctx = new TDBContext())
            {

                try
                {
                    var whereStr = changes.Aggregate("", (str, itm) => str + ($"{itm.Key} == \"{itm.Value}\" &&"));
                    whereStr = whereStr.TrimEnd('&');
                    if (string.IsNullOrEmpty(whereStr)) return;
                    var p = ctx.Set<TEntity>().Where(whereStr).First();
                    EventMessageBus.Current.Publish(new EntityWithChangesFound<TEntity>(p, changes, process, SourceMessage), SourceMessage);
                    
                }
                catch (Exception ex)
                {

                    EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(IGetEntityWithChanges<TEntity>),
                                                                         failedEventMessage: null,
                                                                         expectedEventType: typeof(IEntityWithChangesFound<TEntity>),
                                                                         exception: ex,
                                                                         SourceMsg: SourceMessage), SourceMessage);
                }


            }

        }


        public static void LoadEntitySet(ISystemProcess process) 
        {
            try
            {
               using (var ctx = new TDBContext())
                {
                    var loggerFactory = ctx.GetService<ILoggerFactory>();
                    loggerFactory.AddProvider(new MyLoggerProvider());
                    
                    IQueryable<TEntity> rres = ctx.Set<TEntity>(); 
                    var res = rres.ToList(); //;
                    
                    EventMessageBus.Current.Publish(new EntitySetLoaded<TEntity>(res, process, SourceMessage), SourceMessage);
                }
                
            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(ILoadEntitySet<TEntity>),
                                                                         failedEventMessage: null,
                                                                         expectedEventType: typeof(IEntitySetLoaded<TEntity>),
                                                                         exception: ex,
                                                                         SourceMsg: SourceMessage), SourceMessage);
            }
        }

        public static void LoadEntitySetWithFilter(List<Expression<Func<TEntity, bool>>> filter, ISystemProcess process)
        {
            try
            {
                
                using (var ctx = new TDBContext())
                {
                    var loggerFactory = ctx.GetService<ILoggerFactory>();
                    loggerFactory.AddProvider(new MyLoggerProvider());
                    

                    IQueryable<TEntity> rres = ctx.Set<TEntity>(); 
                    if (rres == null) return;
                   
                    if (filter != null)
                        rres =
                            filter?.Aggregate(rres, (current, s) => current.Where(s));
                    

                    var res = rres.ToList(); 
                    EventMessageBus.Current.Publish(new EntitySetWithFilterLoaded<TEntity>(res, process, SourceMessage), SourceMessage);
                }
                
                
            }
            catch (Exception ex)
            {

                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(ILoadEntitySetWithFilter<TEntity>),
                                                                         failedEventMessage: null,
                                                                         expectedEventType: typeof(IEntitySetWithFilterLoaded<TEntity>),
                                                                         exception: ex,
                                                                         SourceMsg: SourceMessage), SourceMessage);
            }
        }

        public static void EntitySetWithFilterWithIncludesLoaded(List<Expression<Func<TEntity, bool>>> filter,
           List<Expression<Func<TEntity, dynamic>>> includes, ISystemProcess process)
        {
            try
            {
               using (var ctx = new TDBContext())
                {
                    var loggerFactory = ctx.GetService<ILoggerFactory>();
                    loggerFactory.AddProvider(new MyLoggerProvider());
                    

                    IQueryable<TEntity> rres = ctx.Set<TEntity>();
                    if (rres == null) return ;
                    if (includes != null)
                        rres =
                            includes.Aggregate(rres, (current, inc) => current.Include(inc));

                    if (filter != null)
                        rres =
                            filter?.Aggregate(rres, (current, s) => current.Where(s));
                    
                    var res = rres.ToList();
                    EventMessageBus.Current.Publish(new EntitySetWithFilterWithIncludesLoaded<TEntity>(res, includes, process, SourceMessage), SourceMessage);
                }
                
                
                
            }
            catch (Exception ex)
            {
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(ILoadEntitySetWithFilterWithIncludes<TEntity>),
                                                                         failedEventMessage: null,
                                                                         expectedEventType: typeof(IEntitySetWithFilterWithIncludesLoaded<TEntity>),
                                                                         exception: ex,
                                                                         SourceMsg: SourceMessage), SourceMessage);
            }
        }
    }

    public class Repository<T,TView, TViewDBEntity>
        {
            public static List<TView> GetEntityView<TDBContext, TDbEntity>(Expression<Func<T, bool>> filter,Expression<Func<TDbEntity, TViewDBEntity>> query) where TDbEntity : class where TDBContext : DbContext, new()
        {
            try
            {
                var t = new List<TView>();



                using (var ctx = new TDBContext())
                {
                    var loggerFactory = ctx.GetService<ILoggerFactory>();
                    loggerFactory.AddProvider(new MyLoggerProvider());
                    
                    IQueryable<TDbEntity> rres = ctx.Set<TDbEntity>(); //?.Where(x => x != null)
                    if (rres == null) return t;
                   // var rquery = ExpressionConverter<TView>.ConvertExpressionType<TDbEntity, TView>(query);
                    if (filter != null)
                    {
                        var rfilter = ExpressionConverter<T>.ConvertExpressionType<TDbEntity, bool>(filter);
                        var res = rres.Where(rfilter).Select(query);
                        t.AddRange(res.Select(x => (TView) (object) x));
                    }
                    else
                    {
                        t.AddRange(rres.Select(query).Select(x => (TView)(object)x));
                    }
                    //var res = rres.ToList(); //;
                    
                }



                return t;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //public static List<IMedia> GetImage<TDBContext>(List<int> mediaId) where TDBContext : DbContext, new()
        //{
        //    try
        //    {
        //        var res = new List<IMedia>();
        //        var connstr = new TDBContext().Database.GetDbConnection().ConnectionString;

        //        foreach (var m in mediaId)
        //        {



        //            var photoImage = default(byte[]);

        //            using (var conn = new SqlConnection(connstr))
        //            {
        //                using (var cmd = new SqlCommand("SelectMediaInfo", conn))
        //                {
        //                    using (var ts = new TransactionScope())
        //                    {
        //                        cmd.CommandType = CommandType.StoredProcedure;
        //                        cmd.Parameters.AddWithValue("@MediaId", m);

        //                        int mediaTypeId;
        //                        var serverPathName = default(string);
        //                        var serverTxnContext = default(byte[]);

        //                        conn.Open();
        //                        using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
        //                        {
        //                            rdr.Read();
        //                            mediaTypeId = rdr.GetInt32(0);
        //                            serverPathName = rdr.GetSqlString(1).Value;
        //                            serverTxnContext = rdr.GetSqlBinary(2).Value;
        //                            rdr.Close();
        //                        }
        //                        conn.Close();

        //                        using (var source = new SqlFileStream(serverPathName, serverTxnContext, FileAccess.Read)
        //                            )
        //                        {
        //                            using (var dest = new MemoryStream())
        //                            {
        //                                source.CopyTo(dest, 4096);
        //                                dest.Close();
        //                                photoImage = dest.ToArray();
        //                            }
        //                            source.Close();
        //                        }

        //                        res.Add(new Media() {Value = photoImage, Id = m, MediaTypeId = mediaTypeId});

        //                        ts.Complete();
        //                    }
        //                }
        //            }

        //        }

        //        return res;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
