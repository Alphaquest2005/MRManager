using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IScreenModel : IScreenLayoutViewModel
    {
        dynamic Slider { get; set; }
        
    }
}
