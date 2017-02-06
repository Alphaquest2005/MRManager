using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    
    public interface IEntityViewSetWithChangesLoaded<TView> : IProcessSystemMessage where TView : IEntityView
    {
        List<TView> EntitySet { get; }
        Dictionary<string, object> Changes { get; }
    }

}
