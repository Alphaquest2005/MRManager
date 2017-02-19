using System.ComponentModel.Composition;

namespace ViewMessages
{
    
    public class ViewMessageConst
   {
        private static readonly ViewMessageConst instance;
        static ViewMessageConst()
        {
            instance = new ViewMessageConst();
        }

        
        public static ViewMessageConst Instance
        {
            get { return instance; }
        }
        public string ViewHome => "ViewHome";
        public string ViewPatientSummary => "ViewPatientSummary";
        public string ViewPatientVisit => "ViewPatientVisit";
        public string ViewPatientSyntom => "ViewPatientSyntom";
        public string ViewInterview => "ViewInterview";
        public string ViewPatientResponses => "ViewPatientResponses";
       
   }
}
