using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SystemInterfaces;
using EFRepository;


namespace EFRepository
{
    public class EF7DataContext<TEntity> : EF7DataContextBase where TEntity : class, IEntity
    {
        private static Type EntityType { get; set; }
        private static Type ctxType { get; set; }

        static EF7DataContext() 
        {
            var t = typeof(TEntity);
            EntityType = EntityTypes.FirstOrDefault(x => x.Name == (t.Name.Substring(1)));
            ctxType = ContextTypes.FirstOrDefault(x => x.BaseType != null && x.BaseType.Name.Contains("DbContext"));
            if (EntityType == null) throw new InvalidOperationException("DataType Is not Found");
            if (ctxType == null) throw new InvalidOperationException("DBContext Is not Found");
        }

        public static void Create(ICreateEntity<TEntity> msg )
        {
            typeof(EntityRepository<,,>).MakeGenericType(typeof(TEntity),EntityType, ctxType)
                .GetMethod("Create")
                .Invoke(null, new object[] { msg});

        }

        public static void Delete(IDeleteEntity<TEntity> msg )
        {
            typeof(EntityRepository<,,>).MakeGenericType(typeof(TEntity),EntityType, ctxType)
                .GetMethod("Delete")
               .Invoke(null, new object[] { msg });
            
        }

        public static void Update(IUpdateEntityWithChanges<TEntity> msg )
        {
            typeof(EntityRepository<,,>).MakeGenericType(typeof(TEntity),EntityType, ctxType)
                 .GetMethod("Update")
                 .Invoke(null, new object[] { msg});
        }

        public static void Add(IAddOrGetEntityWithChanges<TEntity> msg)
        {
            typeof(EntityRepository<,,>).MakeGenericType(typeof(TEntity), EntityType, ctxType)
                 .GetMethod("Add")
                 .Invoke(null, new object[] { msg });
        }

        public static void GetEntityById(IGetEntityById<TEntity> msg )
        {
            typeof(EntityRepository<,,>).MakeGenericType(typeof(TEntity),EntityType, ctxType)
                  .GetMethod("GetEntityById")
                  .Invoke(null, new object[] { msg });
        }
        public static void GetEntityWithChanges(IGetEntityWithChanges<TEntity> msg )
        {
            typeof(EntityRepository<,,>).MakeGenericType(typeof(TEntity),EntityType, ctxType)
                .GetMethod("GetEntityWithChanges")
                .Invoke(null, new object[] { msg});
        }


        public static void LoadEntitySet(ILoadEntitySet<TEntity> msg )
        {
            typeof(EntityRepository<,,>).MakeGenericType(typeof(TEntity),EntityType, ctxType)
                .GetMethod("LoadEntitySet")
                .Invoke(null, new object[] { msg });
        }

        public static void LoadEntitySetWithFilter(ILoadEntitySetWithFilter<TEntity> msg )
        {
            typeof(EntityRepository<,,>).MakeGenericType(typeof(TEntity),EntityType, ctxType)
                 .GetMethod("LoadEntitySetWithFilter")
                 .Invoke(null, new object[] { msg});
        }

        public static void LoadEntitySetWithFilterWithIncludes(ILoadEntitySetWithFilterWithIncludes<TEntity> msg )
        {
            typeof(EntityRepository<,,>).MakeGenericType(typeof(TEntity),EntityType, ctxType)
                .GetMethod("LoadEntitySetWithFilterWithIncludes")
                .Invoke(null, new object[] { msg });
        }


        
    }

}
