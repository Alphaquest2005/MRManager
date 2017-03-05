using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Entity.Expressions;

namespace EFRepository
{

    public class EntityViewDataContext<TEntityView> : EF7DataContextBase where TEntityView:IEntityView
    {
       public static Type TEntity { get; set; }
        public static Type EntityType { get; set; }
        private static Type ViewType { get; set; }
        private static Type ctxType { get; set; }

       
         static EntityViewDataContext()
        {
            var bt = typeof(TEntityView).GetInterfaces().FirstOrDefault(x => x.Name.Contains("IEntityView"));
            TEntity = bt.GetGenericArguments().First();
            EntityType = EntityTypes.FirstOrDefault(x => x.Name == (TEntity.Name.Substring(1)));

            ViewType = EntityTypes.FirstOrDefault(x => x.Name == (typeof(TEntityView).Name.Substring(1)));
            ctxType = ContextTypes.FirstOrDefault(x => x.BaseType != null && x.BaseType.Name.Contains("DbContext"));
            
            if (ViewType == null) throw new InvalidOperationException("ViewType Is not Found");
            if (ctxType == null) throw new InvalidOperationException("DBContext Is not Found");
        }


     
        public static void GetEntityViewById(IGetEntityViewById<TEntityView> msg)
        {

            typeof(EntityViewRepository<,,,,>).MakeGenericType(typeof(TEntityView), ViewType, TEntity, EntityType, ctxType)
                  .GetMethod("GetEntityViewById")
                  .Invoke(null, new object[] { msg });
        }

        public static void GetEntityViewWithChanges(IGetEntityViewWithChanges<TEntityView> msg)
        {

            typeof(EntityViewRepository<,,,,>).MakeGenericType(typeof(TEntityView), ViewType, TEntity, EntityType, ctxType)
                  .GetMethod("GetEntityViewWithChanges")
                  .Invoke(null, new object[] { msg });
        }

        public static void UpdateEntityViewWithChanges(IUpdateEntityViewWithChanges<TEntityView> msg)
        {

            typeof(EntityViewRepository<,,,,>).MakeGenericType(typeof(TEntityView), ViewType, TEntity, EntityType, ctxType)
                  .GetMethod("UpdateEntityViewWithChanges")
                  .Invoke(null, new object[] { msg });
        }

        public static void LoadEntityViewSetWithChanges(ILoadEntityViewSetWithChanges<TEntityView, IMatchType> msg)
        {

            typeof(EntityViewRepository<,,,,>).MakeGenericType(typeof(TEntityView), ViewType, TEntity, EntityType, ctxType)
                  .GetMethod("LoadEntityViewSetWithChanges")
                  .Invoke(null, new object[] { msg });
        }




       
    }
}
