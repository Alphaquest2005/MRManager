﻿// <autogenerated>
//   This file was generated by T4 code generator Business.Models.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using TrackableEntities.Client;

		namespace AllocationDS.Business.Entities
{
    public partial class xcuda_Item
    {
       
         partial void TrackableStartUp()
         {
           // _changeTracker = new ChangeTrackingCollection<xcuda_Item>(this);
         }

        ChangeTrackingCollection<xcuda_Item> _changeTracker;

        [NotMapped]
        [IgnoreDataMember]
        public new ChangeTrackingCollection<xcuda_Item> ChangeTracker
        {
            get
            {
                return _changeTracker;
            }
        }

         public void StartTracking()
        {
            _changeTracker = new ChangeTrackingCollection<xcuda_Item>(this);
        }
   
    }
}
		