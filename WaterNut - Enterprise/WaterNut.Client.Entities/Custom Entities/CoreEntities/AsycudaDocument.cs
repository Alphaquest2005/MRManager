using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Validation;

namespace CoreEntities.Client.Entities
{
   public partial class AsycudaDocument
    {
        [NumberValidation]
        public Nullable<double> TotalFreight
        {
            get { return this.asycudadocument.TotalFreight; }
            set
            {
                if (value == this.asycudadocument.TotalFreight) return;
                this.asycudadocument.TotalFreight = value;
                if (this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged) this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                NotifyPropertyChanged("TotalFreight");
            }
        }
    }
}
