using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Linq;
using EventAggregator;

namespace BootStrapper
{
    public static class MEFExtensions
    {
        public static IEnumerable<Type> GetExportedTypes<T>(this CompositionContainer container, T type = default(T))
        {
            return container.Catalog.Parts
                .Select(part => ComposablePartExportType<T>(part))
                .Where(t => t != null);
        }

        private static Type ComposablePartExportType<T>(ComposablePartDefinition part)
        {

            if (part.ExportDefinitions.Any(def => def.Metadata.ContainsKey("ExportTypeIdentity") 
                    && def.Metadata["ExportTypeIdentity"].Equals(typeof(T).FullName)
                    ))
            {
                return ReflectionModelServices.GetPartType(part).Value;
            }
           
            
            return null;
        }
       
    }
}