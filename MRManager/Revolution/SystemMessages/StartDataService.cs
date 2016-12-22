﻿using System.ComponentModel.Composition;
using StartUp.Messages;

namespace SystemMessages
{
    [Export]
    public class StartService<TService> : IStartService<TService> where TService:IService<TService>
    {
       
    }
}
