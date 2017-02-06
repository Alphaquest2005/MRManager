using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    
    public interface INavigateToView:IProcessSystemMessage
    {
        string View { get; }
    }
}
