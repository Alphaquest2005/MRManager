using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SystemInterfaces;


namespace EFRepository
{
    public class EF7DataContext<TEntity> : EF7DataContextBase where TEntity : class, IEntity
    {
        private static Type EntityType { get; }
        private static Type ctxType { get; }
        static EF7DataContext()
        {
            var t = typeof(TEntity);
            EntityType = EntityTypes.FirstOrDefault(x => x.Name == (t.Name.Substring(1)));
            ctxType = ContextTypes.FirstOrDefault(x => x.BaseType != null && x.BaseType.Name.Contains("DbContext"));
            if (EntityType == null) throw new InvalidOperationException("DataType Is not Found");
            if (ctxType == null) throw new InvalidOperationException("DBContext Is not Found");
        }


        public static void GetEntityView<TView,TViewDBEntity>(Expression<Func<TEntity, bool>> filter, Expression query)
        {
            try
            {
                var rep = new Repository<TEntity, TView, TViewDBEntity>();

                rep.GetType()
                    .GetMethod("GetEntityView")
                    .MakeGenericMethod(ctxType, EntityType)
                    .Invoke(rep, new object[] { filter, query });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Create(TEntity entity, ISystemProcess process)
        {
            typeof(Repository<,>).MakeGenericType(ctxType, EntityType)
                .GetMethod("Create")
                .Invoke(null, new object[] { entity, process });

        }

        public static void Delete(int entityId, ISystemProcess process)
        {
            typeof(Repository<,>).MakeGenericType(ctxType, EntityType)
                .GetMethod("Delete")
               .Invoke(null, new object[] { entityId, process });
            
        }

        public static void Update(int entityId, Dictionary<string, object> changes, ISystemProcess process)
        {
            typeof(Repository<,>).MakeGenericType(ctxType, EntityType)
                 .GetMethod("Update")
                 .Invoke(null, new object[] { entityId, changes, process });
        }

        public static void GetEntityById(int entityId, ISystemProcess process)
        {
            typeof(Repository<,>).MakeGenericType(ctxType, EntityType)
                  .GetMethod("GetEntityById")
                  .Invoke(null, new object[] { entityId, process });
        }
        public static void GetEntityWithChanges(int entityId, Dictionary<string, object> changes, ISystemProcess process)
        {
            typeof(Repository<,>).MakeGenericType(ctxType, EntityType)
                .GetMethod("GetEntityWithChanges")
                .Invoke(null, new object[] { entityId, changes, process });
        }

        public static void LoadEntitySet(ISystemProcess process)
        {
            typeof(Repository<,>).MakeGenericType(ctxType, EntityType)
                .GetMethod("LoadEntitySet")
                .Invoke(null, new object[] { process });
        }

        public static void LoadEntitySetWithFilter(List<Expression<Func<TEntity, bool>>> filter, ISystemProcess process)
        {
            typeof(Repository<,>).MakeGenericType(ctxType, EntityType)
                 .GetMethod("LoadEntitySetWithFilter")
                 .Invoke(null, new object[] { filter, process });
        }

        public static void LoadEntitySetWithFilterWithIncludes(List<Expression<Func<TEntity, bool>>> filter, List<Expression<Func<TEntity, object>>> includes, ISystemProcess process)
        {
            typeof(Repository<,>).MakeGenericType(ctxType, EntityType)
                .GetMethod("LoadEntitySetWithFilterWithIncludes")
                .Invoke(null, new object[] { filter, includes, process });
        }
    }


}
