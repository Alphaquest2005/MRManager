﻿using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IGetEntityViewWithChanges<out TEntityView> : IEntityViewRequest<TEntityView> where TEntityView : IEntityView
    {
        Dictionary<string, object> Changes { get; }
        
    }

    
    public interface IUpdateEntityViewWithChanges<out TEntityView> : IEntityViewRequest<TEntityView> where TEntityView : IEntityView
    {
        Dictionary<string, object> Changes { get; }
        int EntityId { get; }
    }
    

    
    public interface ILoadEntityViewSetWithChanges<out TEntityView,out TMatchType> : IEntityViewRequest<TEntityView> where TEntityView : IEntityView where TMatchType:IMatchType
    {
        Dictionary<string, object> Changes { get; }
    }

    public interface IPartialMatch : IMatchType
    {

    }
    public interface IExactMatch : IMatchType
    {
        
    }

    public interface IMatchType
    {
    }
}