using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IRepository
    {
        
    }

    [InheritedExport]
    public interface IEntityRepository:IRepository
    {
        
    }

    [InheritedExport]
    public interface IEntityViewRepository:IRepository
    {
        
    }
}
