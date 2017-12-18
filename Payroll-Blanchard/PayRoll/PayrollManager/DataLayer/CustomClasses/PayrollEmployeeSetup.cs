using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManager.DataLayer
{
    public partial class PayrollEmployeeSetup
    {
        private double _calcAmount = double.NegativeInfinity;
        public double CalcAmount
        {
            get
            {
                try
                {
                    if (_calcAmount > double.NegativeInfinity) return _calcAmount;
                        if (this.PayrollSetupItem == null)
                        {
                            PayrollSetupItemReference.Load();
                            if (this.PayrollSetupItem == null)
                            {
                                _calcAmount = 0;
                                return _calcAmount;
                        }
                                
                        }
                        DataLayer.PayrollItem p = new PayrollItem() {PayrollSetupItem = this.PayrollSetupItem};
                        if (BaseViewModel.Instance.ConfigPayrollItem(p, this) == BaseViewModel.TriBoolState.Success)
                        {
                            double amt =
                                Convert.ToDouble(BaseViewModel.GetPayrollAmount(Convert.ToDouble(this.BaseAmount), p));
                            //p = null;
                            p = null;
                            _calcAmount = amt;
                            return _calcAmount;
                        }
                        else
                        {
                        //p = null;
                        _calcAmount = 0;
                        return _calcAmount;
                    }
                   
                }
                catch (Exception ex)
                {
                    //throw;
                }
                 
                    return 0;
                
            }
        }
    }
}
