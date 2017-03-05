using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SystemInterfaces;
using CommonMessages;

using Entity.Expressions;
using EventAggregator;
using EventMessages;
using Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic;
using BootStrapper;
using Common;

using EventMessages.Events;
using MoreLinq;
using RevolutionData.Context;
using RevolutionEntities.Process;
using Utilities;
using Source = Common.Source;

namespace EFRepository
{
    public class EntityViewRepository<TView,TDbView, TEntity, TDbEntity, TDbContext>:BaseRepository<EntityViewRepository<TView, TDbView, TEntity, TDbEntity, TDbContext>>, IEntityViewRepository where TDbView:class, IEntityId, new() where TDbEntity : class,IEntity, new() where TDbContext : DbContext, new() where TEntity : class, IEntity where TView : IEntityView<TEntity>
    {


        private static Dictionary<Type, Func<string, KeyValuePair<string, object>, string>> IMatchTypeFunctions = new Dictionary<Type, Func<string, KeyValuePair<string, object>, string>>()
      {
          {typeof(IExactMatch), (str, itm) => str + $"{itm.Key} == \"{itm.Value}\" &&" },
          {typeof(IPartialMatch), (str, itm) => str + $"{itm.Key}.Contains(\"{itm.Value}\") &&" }
      };

        public static void GetEntityViewById(IGetEntityViewById<TView>  msg)
        {
            try
            {
                var exp = FindExpressionClass.FindExpression<TDbEntity, TDbView>();
                using (var ctx = new TDbContext())
                {
                    // ReSharper disable once ReplaceWithSingleCallToFirstOrDefault cuz EF7 bugging LEAVE JUST SO
                    var res = ctx.Set<TDbEntity>().AsNoTracking().Select(exp).FirstOrDefault(x => x.Id == msg.EntityId);//
                    
                    EventMessageBus.Current.Publish(new EntityFound<TView>((TView)(object)res,new StateEventInfo(msg.Process.Id, EntityView.Events.EntityViewFound), msg.Process, Source), Source);
                }
            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntityFound<TView>));
                
            }

        }

    


        public static void GetEntityViewWithChanges(IGetEntityViewWithChanges<TView> msg)
        {
            try
            {
                var exp = FindExpressionClass.FindExpression<TDbEntity, TDbView>();
                using (var ctx = new TDbContext())
                {
                    // ReSharper disable once ReplaceWithSingleCallToFirstOrDefault cuz EF7 bugging LEAVE JUST SO
                    var whereStr = msg.Changes.Aggregate("", (str, itm) => str + ($"{itm.Key} == \"{itm.Value}\" &&"));
                    whereStr = whereStr.TrimEnd('&');
                    if (string.IsNullOrEmpty(whereStr)) return;
                    var res = ctx.Set<TDbEntity>().AsNoTracking().Select(exp).Where(whereStr).FirstOrDefault();//
                    if (res != null)
                    {
                        EventMessageBus.Current.Publish(new EntityViewWithChangesFound<TView>((TView)(object)res, msg.Changes, new StateEventInfo(msg.Process.Id, EntityView.Events.EntityViewFound), msg.Process, Source), Source);
                    }
                    else
                    {
                        // not found
                    }
                    
                }
            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntityViewWithChangesFound<TView>));
            }

        }

        public static void UpdateEntityViewWithChanges(IUpdateEntityViewWithChanges<TView> msg)
        {
            try
            {
                var exp = FindExpressionClass.FindExpression<TDbEntity, TDbView>();
                using (var ctx = new TDbContext())
                {
                    // ReSharper disable once ReplaceWithSingleCallToFirstOrDefault cuz EF7 bugging LEAVE JUST SO
                    TDbEntity res;//
                    if (msg.EntityId == 0)
                    {
                        res = new TDbEntity();
                        ctx.Set<TDbEntity>().Add(res);
                    }
                    else
                    {
                       res = ctx.Set<TDbEntity>().FirstOrDefault(x => x.Id == msg.EntityId); 
                    }
                    
                    res.ApplyChanges(msg.Changes);
                    ctx.SaveChanges(true);
                    //TODO: retrieve whole item
                    var ures = ctx.Set<TDbEntity>().Select(exp).FirstOrDefault(x => x.Id == res.Id);//
                    EventMessageBus.Current.Publish(new EntityViewWithChangesUpdated<TView>((TView)(object)ures, msg.Changes, new StateEventInfo(msg.Process.Id, EntityView.Events.EntityViewFound), msg.Process, Source), Source);
                }
            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntityViewWithChangesUpdated<TView>));
            }

        }

        public static void LoadEntityViewSetWithChanges(ILoadEntityViewSetWithChanges<TView, IMatchType> msg)
        {
            try
            {
                var exp = FindExpressionClass.FindExpression<TDbEntity, TDbView>();
                using (var ctx = new TDbContext())
                {
                    // ReSharper disable once ReplaceWithSingleCallToFirstOrDefault cuz EF7 bugging LEAVE JUST SO
                    var matchtype = msg.GetType().GenericTypeArguments[1];
                    var whereStr = msg.Changes.Aggregate("", IMatchTypeFunctions[matchtype]);
                    whereStr = whereStr.TrimEnd('&');
                    IQueryable<TDbView> res;
                    res = string.IsNullOrEmpty(whereStr) 
                        ? ctx.Set<TDbEntity>().OrderByDescending(x => x.Id).AsNoTracking().Select(exp)
                        : ctx.Set<TDbEntity>().OrderByDescending(x => x.Id).AsNoTracking().Select(exp).Where(whereStr);
                    

                    EventMessageBus.Current.Publish(new EntityViewSetWithChangesLoaded<TView>(res.Select(x => (TView)(object)x).ToList(), msg.Changes, new StateEventInfo(msg.Process.Id, EntityView.Events.EntityViewFound), msg.Process, Source), Source);
                }
            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntityViewLoaded<TView>));
            }

        }




        //public static List<TView> GetEntityView(Expression<Func<TEntity, bool>> filter,Expression<Func<TDBEntity, TDBEntity>> query) 
        //{
        //    try
        //    {
        //        var t = new List<TView>();



        //        using (var ctx = new TDBContext())
        //        {
        //            var loggerFactory = ctx.GetService<ILoggerFactory>();
        //            loggerFactory.AddProvider(new MyLoggerProvider());

        //            IQueryable<TDbEntity> rres = ctx.Set<TDbEntity>(); //?.Where(x => x != null)
        //            if (rres == null) return t;
        //            // var rquery = ExpressionConverter<TView>.ConvertExpressionType<TDbEntity, TView>(query);
        //            if (filter != null)
        //            {
        //                var rfilter = ExpressionConverter<TEntity>.ConvertExpressionType<TDbEntity, bool>(filter);
        //                var res = rres.Where(rfilter).Select(query);
        //                t.AddRange(res.Select(x => (TView) (object) x));
        //            }
        //            else
        //            {
        //                t.AddRange(rres.Select(query).Select(x => (TView)(object)x));
        //            }
        //            //var res = rres.ToList(); //;

        //        }



        //        return t;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

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