using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using StartUp.Messages;

namespace ViewModel.Interfaces
{
    
    public interface IViewModelSupervisor : IService<IViewModelSupervisor>
    {
    }
}
