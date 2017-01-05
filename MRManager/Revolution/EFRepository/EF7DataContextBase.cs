using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SystemInterfaces;
using EF.DBContexts;
using EF.Entities;

namespace EFRepository
{
    public abstract class EF7DataContextBase
    {
        public static List<Type> ContextTypes;
        public static List<Type> EntityTypes;

        static EF7DataContextBase()
        {
            var t = new MRManagerDBContext().GetType().Assembly;
            var x = new EFEntity<IEntity>().GetType().Assembly;
            //var ctxasm = typeof(ctx).Assembly;
            ContextTypes = t.GetTypes().ToList();

            // Assembly asm = typeof(EF7Entity).Assembly;
            EntityTypes = x.GetTypes().ToList();
        }
    }
}