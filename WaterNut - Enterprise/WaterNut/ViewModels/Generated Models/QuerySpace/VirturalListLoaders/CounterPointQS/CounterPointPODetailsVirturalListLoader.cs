﻿// <autogenerated>
//   This file was generated by T4 code generator AllQuerySpaceViewModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.UI;
using Core.Common.UI.DataVirtualization;
using SimpleMvvmToolkit;
using CounterPointQS.Client.Repositories;
using CounterPointQS.Client.Entities;



namespace WaterNut.QuerySpace.CounterPointQS.ViewModels
{
    public partial class CounterPointPODetailsVirturalListLoader : IVirtualListLoader<CounterPointPODetails>
	{
        public IEnumerable<string> IncludesLst = new List<string>(){""};
		public bool CanSort
        {
            get
            {
                return false;
            }
        }
        
        public string FilterExpression
        {
            get { return _filterExpression; }
            set
            {
                _filterExpression = value;
                MessageBus.Default.BeginNotify(MessageToken.CounterPointPODetailsFilterExpressionChanged, null,
                                            new NotificationEventArgs<string>(MessageToken.CounterPointPODetailsFilterExpressionChanged, _filterExpression));
            }
        }
        private string _filterExpression = "None";

		public void SetNavigationExpression (string navigationProperty, string expression )
        {
            try
            {
                navExp = new Dictionary<string, string> {{navigationProperty, expression}};
                //if (string.IsNullOrEmpty(FilterExpression)) FilterExpression = "All";
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public void ClearNavigationExpression()
        {
            navExp = new Dictionary<string, string>();           
        }

        private Dictionary<string, string> navExp = new Dictionary<string, string>(); 
		public Dictionary<string, string> NavigationExpression
        {
            get { return navExp; }
        }

        public IList<CounterPointPODetails> LoadRange(int startIndex, int count, SortDescriptionCollection sortDescriptions, out int overallCount)
        {
            try
            {
                if (FilterExpression == null) FilterExpression = "None";
			    using (var ctx = new CounterPointPODetailsRepository())
				{
					var r = ctx.LoadRange(startIndex, count, FilterExpression, navExp, IncludesLst);
				    overallCount = r.Result.Item2;

					return r.Result.Item1.ToList();
				}

            }
            catch (Exception ex)
            {
                StatusModel.Message(ex.Message);
                overallCount = 0;
                return new List<CounterPointPODetails>() ;
            }
			
        }
	}
}
		
		
