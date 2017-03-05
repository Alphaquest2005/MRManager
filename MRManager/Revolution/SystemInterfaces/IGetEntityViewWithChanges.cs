using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IGetEntityViewWithChanges<out TEntityView> : IProcessSystemMessage, IEntityViewRequest<TEntityView> where TEntityView : IEntityView
    {
        Dictionary<string, object> Changes { get; }
        
    }

    
    public interface IUpdateEntityViewWithChanges<out TEntityView> : IProcessSystemMessage, IEntityViewRequest<TEntityView> where TEntityView : IEntityView
    {
        Dictionary<string, object> Changes { get; }
        int EntityId { get; }
    }

    public interface IAddEntityViewWithChanges<out TEntityView> : IProcessSystemMessage, IEntityViewRequest<TEntityView> where TEntityView : IEntityView
    {
        Dictionary<string, object> Changes { get; }
    }



    public interface ILoadEntityViewSetWithChanges<out TEntityView,out TMatchType> :IProcessSystemMessage, IEntityViewRequest<TEntityView> where TEntityView : IEntityView where TMatchType:IMatchType
    {
        Dictionary<string, object> Changes { get; }
    }

    public interface ILoadPulledEntityViewSetWithChanges<out TEntityView, out TMatchType> : IProcessSystemMessage, IEntityViewRequest<TEntityView> where TEntityView : IEntityView where TMatchType : IMatchType
    {
        Dictionary<string, dynamic> Changes { get; }
        string EntityName { get; }

    }

    public interface ILoadPulledEntityViewSetWithChanges<out TMatchType> : IProcessSystemMessage, IEntityViewRequest where TMatchType : IMatchType
    {
        Dictionary<string, dynamic> Changes { get; }
        string EntityName { get; }
        Type ViewType { get; }

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