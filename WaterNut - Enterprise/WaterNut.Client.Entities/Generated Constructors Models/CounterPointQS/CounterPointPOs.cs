﻿// <autogenerated>
//   This file was generated by T4 code generator AllClientModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using CounterPointQS.Client.DTO;
using TrackableEntities.Client;

namespace CounterPointQS.Client.Entities
{

    public partial class CounterPointPOs 
    {
       public CounterPointPOs()
        {
            this.DTO = new DTO.CounterPointPOs();//{TrackingState = TrackableEntities.TrackingState.Added}
            _changeTracker = new ChangeTrackingCollection<DTO.CounterPointPOs>(this.DTO);

            CustomClassStartUp();
            MyNavPropStartUp();
            IIdentifiableEntityStartUp();
            AutoGenStartUp();
        }
    partial void CustomClassStartUp();
    partial void AutoGenStartUp();
    partial void MyNavPropStartUp();
    partial void IIdentifiableEntityStartUp();
   
    }
}
		