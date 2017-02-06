using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    
    public interface IEntityViewLoaded<out TView> : IProcessSystemMessage where TView : IEntityId
    {
        IEnumerable<TView> Entities { get; }
    }
}
