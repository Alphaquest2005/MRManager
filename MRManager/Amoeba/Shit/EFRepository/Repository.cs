using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using EF7.DataContext;
using EF7.MREntitiesQS.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Model.Interfaces.MREntitiesQS;
using Microsoft.Extensions.Logging;


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
                            includes.Select(itm => ConvertExpressionType<TDbEntity, dynamic>(itm))
                                .Where(inc => inc != null)
                                .Aggregate(rres, (current, inc) => (IQueryable<TDbEntity>) current.Include(inc));

                    if (filter != null)
                        rres =
                            filter?.Select(ConvertExpressionType<TDbEntity, bool>)
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

        private static class ExpressionTransformer<TFrom, TTo>
        {
            private class Visitor : ExpressionVisitor
            {
                private ParameterExpression _parameter;

                public Visitor(ParameterExpression parameter)
                {
                    _parameter = parameter;
                }

                protected override Expression VisitParameter(ParameterExpression node)
                {
                    return _parameter;
                }
            }

            internal static Expression<Func<TTo, U>> Tranform<U>(Expression<Func<TFrom, U>> expression)
            {
                ParameterExpression parameter = Expression.Parameter(typeof (TTo));
                Expression body = new Visitor(parameter).Visit(expression.Body);
                return Expression.Lambda<Func<TTo, U>>(body, parameter);
            }
        }


        private static Expression<Func<T1, U>> ConvertExpressionType<T1, U>(Expression<Func<T, U>> filter)
        {
            if (filter == null) return null;
            var newExpression = ExpressionTransformer<T, T1>.Tranform(filter);
            return newExpression;
        }


        public static List<IMedium> GetImage(List<int> mediaId)
        {
            try
            {
                var res = new List<IMedium>();
                var connstr = new MREntitiesQSContext().Database.GetDbConnection().ConnectionString;

                foreach (var m in mediaId)
                {



                    var photoImage = default(byte[]);

                    using (var conn = new SqlConnection(connstr))
                    {
                        using (var cmd = new SqlCommand("SelectMediaInfo", conn))
                        {
                            using (var ts = new TransactionScope())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@MediaId", m);

                                int mediaTypeId;
                                var serverPathName = default(string);
                                var serverTxnContext = default(byte[]);

                                conn.Open();
                                using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                                {
                                    rdr.Read();
                                    mediaTypeId = rdr.GetInt32(0);
                                    serverPathName = rdr.GetSqlString(1).Value;
                                    serverTxnContext = rdr.GetSqlBinary(2).Value;
                                    rdr.Close();
                                }
                                conn.Close();

                                using (var source = new SqlFileStream(serverPathName, serverTxnContext, FileAccess.Read)
                                    )
                                {
                                    using (var dest = new MemoryStream())
                                    {
                                        source.CopyTo(dest, 4096);
                                        dest.Close();
                                        photoImage = dest.ToArray();
                                    }
                                    source.Close();
                                }

                                res.Add(new Medium() {Id = m, MediaTypeId = mediaTypeId, Media = photoImage});

                                ts.Complete();
                            }
                        }
                    }

                }

                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
