//using System.Collections.Generic;
//using System.Linq;
//using Akka.Actor;
//using CommonMessages;
//
//using EFRepository;
//using EventAggregator;
//using EventMessages;
//using Interfaces;

//namespace DataServices.Actors
//{
//   public class GetMediaActor : ReceiveActor
//    {
      
//       public GetMediaActor()
//        {
             
//            Receive<GetMedia>(m => HandleGetEntityById(m.MediaIdList, m.Source));
          
//        }

//        Source Source => new Source(new MessageSource(this.ToString()), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

//        private void HandleGetEntityById(List<int> entityId, MessageSource source)
//       {

//            var res = EF7DataContext<IMedia>.GetMedia(entityId);

//            if (res != null && res.Any())
//                {
//                    EventMessageBus.Current.Publish(new GotMedia(res, source), MsgSource);
//                }
//                else
//                {
//                    EventMessageBus.Current.Publish(new MediaNotFound(entityId, source), MsgSource);
//                }
            
//        }
//    }
//}
