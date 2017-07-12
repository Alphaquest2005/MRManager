using System;
using System.ComponentModel.Composition;
using System.ServiceModel;
using AllocationQS.Business.Services;
using Core.Common.Contracts;



namespace CoreEntities.Business.Services
{
    [Export(typeof(IAsycudaSalesAllocationsExService))]
    [Export(typeof(IBusinessService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class SystemService : ISystemService, IDisposable
    {
       
        public bool ValidateInstallation()
        {
            return WaterNut.DataSpace.BaseDataModel.Instance.ValidateInstallation();
        }
       
        #region IDisposable Members

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        #endregion

    }
}

