using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PrintableListView
{
    public class HeaderFooterDataContext
    {
        public object ParentDataContext
        {
            get
            {
                return parentDataContext;
            }
            set
            {
                parentDataContext = value;
            }
        }
        private object parentDataContext;

        public IList PageItems
        {
            get
            {
                return pageItems;
            }
            set
            {
                pageItems = value;
            }
        }
        private IList pageItems;

        public bool IsFirstPage
        {
            get
            {
                return isFirstPage;
            }
            set
            {
                isFirstPage = value;
            }
        }
        private bool isFirstPage = false;

        public bool IsLastPage
        {
            get
            {
                return isLastPage;
            }
            set
            {
                isLastPage = value;
            }
        }
        private bool isLastPage = false;

        public int PageNumber
        {
            get
            {
                return pageNumber;
            }
            set
            {
                pageNumber = value;
            }
        }
        int pageNumber;

        public override string ToString()
        {
            return pageNumber.ToString();
        }

        public HeaderFooterDataContext(object parentDataContext, int pageNumber, IList items)
        {
            this.PageNumber = pageNumber;
            this.ParentDataContext = parentDataContext;
            this.PageItems = items;
        }  

        public HeaderFooterDataContext(object parentDataContext, IList items)
        {
            this.ParentDataContext = parentDataContext;
            this.PageItems = items;
        }
    }
}
