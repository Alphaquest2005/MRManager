using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEntityDeleted:IProcessSystemMessage
    {
        int EntityId { get; }
    }

}
