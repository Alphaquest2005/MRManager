using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using PrismMVVMLibrary;

namespace SalesTaskPad
{
    public class SalesTaskPadVM : ViewModelBase
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IUnityContainer container;

        public SalesTaskPadVM(IUnityContainer container, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            //+ Example of run time data vs. design time data (Design data goes in in SalesTaskPadVMDesign.cs)
            ExampleData = "Run Time Data";
        }


        //+ ToDo: Replace this with your own data fields
        private string exampleData;
        public string ExampleData
        {
            get { return exampleData; }
            set
            {
                if (!object.Equals(exampleData, value))
                {
                    exampleData = value;
                    RaisePropertyChanged(() => ExampleData);
                }
            }
        }
    }
}
