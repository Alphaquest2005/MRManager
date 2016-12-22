using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DataInterfaces;
using EF7.DataContext;
using EF7Entities;
using Model.Interfaces.MREntitiesQS;


namespace EFRepository
{
    public class EF7DataContext<T> where T:IEntity
    {
         static EF7DataContext()
         {
            var ctxasm = typeof(MREntitiesQSContext).Assembly;
            ContextTypes = ctxasm.GetTypes().ToList();

            Assembly asm = typeof(EF7Entity).Assembly;
             EntityTypes = asm.GetTypes().ToList();
         }

        private static readonly List<Type> ContextTypes;
        private static readonly List<Type> EntityTypes;
        public static List<T> GetData(List<Expression<Func<T, bool>>> filter,
            List<Expression<Func<T, dynamic>>> includes)
        {
            try
            {
               
                var t = typeof (T);
                var context = t.FullName.Split('.').FirstOrDefault(x => x.Contains("Entities"));
                Type type = EntityTypes.FirstOrDefault(x => x.FullName.Contains("."+ t.Name.Substring(1)) && x.FullName.Contains(context));
                if(type == null) return new List<T>();
               
                
                Type ctxType = ContextTypes.FirstOrDefault(x => x.FullName.Contains(context));
                if(ctxType == null) return new List<T>();
               var rep = new Repository<T>();



                var task = rep.GetType()
                    .GetMethod("GetData")
                    .MakeGenericMethod(ctxType, type)
                    .Invoke(rep, new object[] { filter, includes});

               
                return (List<T>) task;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<IMedium> GetMedia(List<int> entityId)
        {
            return Repository<IMedium>.GetImage(entityId);
        }
    }
}
