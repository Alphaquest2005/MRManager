using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Web;
using System.Xml;
using System.Net;
using System.Diagnostics;
using RMSDataAccessLayer;


namespace Test
{
    public class ViewModel : INotifyPropertyChanged
    {
        private List<string> _WaitMessage = new List<string>() { "Please Wait..." };
        public IEnumerable WaitMessage { get { return _WaitMessage; } }

     
        private string _QueryText;
        public string QueryText
        {
            get { return _QueryText; }
            set
            {
                if (_QueryText != value)
                {
                    _QueryText = value;
                    OnPropertyChanged("QueryText");
                    _QueryCollection = null;
                    OnPropertyChanged("QueryCollection");
                    Debug.Print("QueryText: " + value);
                }
            }
        }

        public IEnumerable _QueryCollection = null;
        public IEnumerable QueryCollection
        {
            get
            {
                Debug.Print("---" + _QueryCollection);
               // QueryGoogle(QueryText);
                QueryPresciptions(QueryText);

                return _QueryCollection;
            }
        }

        public Medicine Item
        {
            get;
            set;
        }

        private void QueryPresciptions(string QueryText)
        {
           
        }

        //private void QueryGoogle(string SearchTerm)
        //{
        //    Debug.Print("Query: " + SearchTerm);
        //    string sanitized = HttpUtility.HtmlEncode(SearchTerm);
        //    string url = @"http://google.com/complete/search?output=toolbar&q=" + sanitized;
        //    WebRequest httpWebRequest = HttpWebRequest.Create(url);
        //    var webResponse = httpWebRequest.GetResponse();
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.Load(webResponse.GetResponseStream());
        //    var result = xmlDoc.SelectNodes("//CompleteSuggestion");
        //    _QueryCollection = result;
        //}

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
