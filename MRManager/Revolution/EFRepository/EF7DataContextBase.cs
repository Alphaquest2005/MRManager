using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SystemInterfaces;


namespace EFRepository
{
    public abstract class EF7DataContextBase
    {
        public static List<Type> ContextTypes;
        public static List<Type> EntityTypes;

        static EF7DataContextBase()

        {
            ContextTypes = BootStrapper.BootStrapper.DbContextAssembly.GetTypes().ToList();
            EntityTypes = BootStrapper.BootStrapper.EntitiesAssembly.GetTypes().ToList();
        }
    }
}