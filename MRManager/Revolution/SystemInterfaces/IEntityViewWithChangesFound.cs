using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEntityViewWithChangesFound<TView> : IProcessSystemMessage
    {
        TView Entity { get; set; }
        Dictionary<string, object> Changes { get; }
    }
}
