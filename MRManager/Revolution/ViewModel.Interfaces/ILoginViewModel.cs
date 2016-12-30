﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using ViewModelInterfaces;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface ILoginViewModel: IEntityViewModel<IUserSignIn>
    {
    }
}