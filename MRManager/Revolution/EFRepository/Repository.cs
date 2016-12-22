using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Utilities;


namespace EFRepository
{
    public class Repository<T>
    {
        public static List<T> GetData<TDBContext, TDbEntity>(List<Expression<Func<T, bool>>> filter,
            List<Expression<Func<T, dynamic>>> includes) where TDbEntity : class where TDBContext : DbContext, new()
        {
            try
            {
                var t = new List<T>();



                using (var ctx = new TDBContext())
                {
                    var loggerFactory = ctx.GetService<ILoggerFactory>();
                    loggerFactory.AddProvider(new MyLoggerProvider());
                    //var rres = inc != null ? ctx.Set<TDbEntity>().Include(inc).Where(exp) : ctx.Set<TDbEntity>().Where(exp);

                    IQueryable<TDbEntity> rres = ctx.Set<TDbEntity>(); //?.Where(x => x != null)
                    if (rres == null) return t;
                    if (includes != null)
                        rres =
                            includes.Select(itm => ExpressionConverter<T>.ConvertExpressionType<TDbEntity, dynamic>(itm))
                                .Where(inc => inc != null)
                                .Aggregate(rres, (current, inc) => (IQueryable<TDbEntity>) current.Include(inc));

                    if (filter != null)
                        rres =
                            filter?.Select(ExpressionConverter<T>.ConvertExpressionType<TDbEntity, bool>)
                                .Where(exp => exp != null)
                                .Aggregate(rres, (current, s) => current.Where(s));
                    // rres.Aggregate(rres, (current, exp) => (IQueryable<TDbEntity>)current.Where(exp));

                    var res = rres.ToList(); //;
                    t.AddRange(res.Select(itm => (T) (object) itm));
                }



                return t;
            }
            catch (Exception)
            {

                throw;
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
