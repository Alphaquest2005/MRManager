using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EFRepository
{
    public abstract class EF7DataContextBase
    {
        protected static List<Type> ContextTypes;
        protected static List<Type> EntityTypes;

        public static void Initialize(Assembly dbContextAssembly, Assembly entityAssembly)
        {
            //var ctxasm = typeof(ctx).Assembly;
            ContextTypes = dbContextAssembly.GetTypes().ToList();

            // Assembly asm = typeof(EF7Entity).Assembly;
            EntityTypes = entityAssembly.GetTypes().ToList();
        }
    }
}