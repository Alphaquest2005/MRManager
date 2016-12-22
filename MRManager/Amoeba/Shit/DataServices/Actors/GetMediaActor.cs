using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using CommonMessages;
using EFRepository;
using EventAggregator;
using EventMessages;
using Model.Interfaces.MREntitiesQS;

namespace DataServices.Actors
{
   public class GetMediaActor : ReceiveActor
    {
      
       public GetMediaActor()
        {
             
            Receive<GetMedia>(m => HandleGetEntityById(m.MediaIdList, m.Source));
          
        }

        MessageSource msgSource => new MessageSource(this.ToString());

        private void HandleGetEntityById(List<int> entityId, MessageSource source)
       {

            var res = EF7DataContext<IMedium>.GetMedia(entityId);

            if (res != null && res.Any())
                {
                    EventMessageBus.Current.Publish(new GotMedia(res, source), msgSource);
                }
                else
                {
                    EventMessageBus.Current.Publish(new MediaNotFound(entityId, source), msgSource);
                }
            
        }
    }
}
