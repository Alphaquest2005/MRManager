﻿// <autogenerated>
//   This file was generated by T4 code generator AllBusinessModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

		namespace SalesDataQS.Business.Entities
{
    public partial class SalesDataAllocations
    {
       public SalesDataAllocations()
        {
            CustomClassStartUp();
            MyNavPropStartUp();
            SetupProperties();
            IIdentifiableEntityStartUp();
            AutoGenStartUp();  
            TrackableStartUp();
        }

      public SalesDataAllocations(bool start) : this()
       {
          if(start) StartTracking();
       }
    partial void SetupProperties();
    partial void AutoGenStartUp();
    partial void CustomClassStartUp();
    partial void MyNavPropStartUp();
    partial void IIdentifiableEntityStartUp();
    partial void TrackableStartUp();
   
    }
}
		