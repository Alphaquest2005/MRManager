using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SystemInterfaces;
using DataInterfaces;


namespace EFRepository
{
    public class EF7DataContext<T> : EF7DataContextBase where T : IEntity
    {
        public static List<T> GetData(List<Expression<Func<T, bool>>> filter,
            List<Expression<Func<T, dynamic>>> includes)
        {
            try
            {

                var t = typeof (T);
                var type = EntityTypes.FirstOrDefault(x => x.Name == (t.Name.Substring(1)));
                if (type == null) return new List<T>();


                Type ctxType =
                    ContextTypes.FirstOrDefault(x => x.BaseType != null && x.BaseType.Name.Contains("DbContext"));
                if (ctxType == null) return new List<T>();
                var rep = new Repository<T>();



                var task = rep.GetType()
                    .GetMethod("GetData")
                    .MakeGenericMethod(ctxType, type)
                    .Invoke(rep, new object[] {filter, includes});


                return (List<T>) task;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<TView> GetEntityView<TView,TViewDBEntity>(Expression<Func<T, bool>> filter, Expression query)
        {
            try
            {
                var t = typeof(T);
                var type = EntityTypes.FirstOrDefault(x => x.Name == (t.Name.Substring(1)));
                if (type == null) return new List<TView>();


                Type ctxType =
                    ContextTypes.FirstOrDefault(x => x.BaseType != null && x.BaseType.Name.Contains("DbContext"));
                if (ctxType == null) return new List<TView>();
                var rep = new Repository<T, TView, TViewDBEntity>();




                var task = rep.GetType()
                    .GetMethod("GetEntityView")
                    .MakeGenericMethod(ctxType, type)
                    .Invoke(rep, new object[] { filter, query });


                return (List<TView>)task;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}
