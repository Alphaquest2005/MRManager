using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    
    public interface ILoadViewModel : IProcessSystemMessage
    {
        IViewModelInfo ViewModelInfo { get; }
    }
}
