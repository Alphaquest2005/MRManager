﻿// <autogenerated>
//   This file was generated by T4 code generator AllBusinessModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using TrackableEntities.Client;

		namespace PreviousDocumentDS.Business.Entities
{
    public partial class EntryDataDetails
    {
       
         partial void TrackableStartUp()
         {
           // _changeTracker = new ChangeTrackingCollection<EntryDataDetails>(this);
         }

        ChangeTrackingCollection<EntryDataDetails> _changeTracker;

        [NotMapped]
        [IgnoreDataMember]
        public new ChangeTrackingCollection<EntryDataDetails> ChangeTracker
        {
            get
            {
                return _changeTracker;
            }
        }

         public void StartTracking()
        {
            _changeTracker = new ChangeTrackingCollection<EntryDataDetails>(this);
        }
   
    }
}
		