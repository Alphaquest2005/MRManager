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
        public string ViewVitals => "ViewVitals";
        public string ViewPatientInfo => "ViewPatientInfo";
        public string ViewPatientResponses => "ViewPatientResponses";
       public string ViewHome => "ViewHome";
   }
}
