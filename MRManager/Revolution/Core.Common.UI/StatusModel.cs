namespace Core.Common.UI
{
	public class StatusModel 
	{
         private static readonly StatusModel instance;
         static StatusModel()
        {


            instance = new StatusModel();
           
        }

         public static StatusModel Instance
        {
            get { return instance; }
        }

        public const string MaxValChanged = "MaxValChanged";
        public const string ErrorTxtChanged = "ErrorTxtChanged";
        public const string StatusTxtChanged = "StatusTxtChanged";
        public const string StatusValueChanged = "StatusValueChanged";
        public const string StatusVisibilityChanged = "StatusVisibilityChanged";


		public StatusModel()
		{
			
            //EventMessageBus.Current.GetEvent<MaxValChanged>, OnMaxValChanged);
            //RegisterToReceiveMessages(StatusTxtChanged, OnStatusTxtChanged);
            //RegisterToReceiveMessages(ErrorTxtChanged, OnErrorTxtChanged);
            //RegisterToReceiveMessages(StatusValueChanged, OnStatusValueChanged);
            //RegisterToReceiveMessages(StatusVisibilityChanged, OnStatusVisibilityChanged);
		}

  //      private void OnErrorTxtChanged()
  //      {
  //          NotifyPropertyChanged(x => ErrorTxt);
  //      }

  //      private void OnStatusVisibilityChanged()
  //      {
  //          NotifyPropertyChanged(x => StatusVisibility);
  //      }

  //      private void OnStatusValueChanged()
  //      {
  //          NotifyPropertyChanged(x => StatusValue);
  //      }

  //      private void OnStatusTxtChanged()
  //      {
  //          NotifyPropertyChanged(x => StatusTxt);
  //      }

  //      private void OnMaxValChanged()
  //      {
  //          NotifyPropertyChanged(x => MaxVal);
  //      }
	   
		//private static double _maxVal = 0;
		//public double MaxVal 
		//{ 
		//	get { return _maxVal; }
		//	set 
		//	{
		//		_maxVal = value;
  //              NotifyPropertyChanged(x => MaxVal);
		//	}
		//}

		//private static double _statusValue;
		//public double StatusValue
		//{
		//	get { return _statusValue; }
		//	set
		//	{
		//		_statusValue = value;
  //              NotifyPropertyChanged(x => StatusValue);
		//	}
		//}


	 //   private static string _operation;
  //      public string Operation
  //      {
  //          get { return _operation; }
  //          set
  //          {
  //              _operation = value;
  //              NotifyPropertyChanged(x => Operation);
  //          }
  //      }

		//private static string _statusTxt = "test";
		//public string StatusTxt 
		//{ 
		//	get { return _statusTxt; }
		//	set
		//	{
		//		_statusTxt = value;
  //              NotifyPropertyChanged(x => StatusTxt);
		//	}
		//}

		//private static Visibility _statusVisibility = Visibility.Hidden;
		//public Visibility StatusVisibility
		//{
		//	get { return _statusVisibility; }
		//	set
		//	{
		//		_statusVisibility = value;
  //              NotifyPropertyChanged(x => StatusVisibility);
		//	}
		//}

  //      private static string _errorTxt = "test";
  //      public string ErrorTxt
  //      {
  //          get { return _errorTxt; }
  //          set
  //          {
  //              _errorTxt = value;
  //              NotifyPropertyChanged(x => ErrorTxt);
  //          }
  //      }


	 //   public static void StartStatusUpdate(string operation, double max, double value = 1)
		//{
		//	_statusValue = 0;
		//	_maxVal = max;
		//	_operation = operation;
		//	_statusTxt = string.Format("{0} | {1} of {2}", operation, value, max);
		//	_statusVisibility = Visibility.Visible;
		//  // timer.Enabled = true;
  //          MessageBus.Default.BeginNotify(MaxValChanged, null, new NotificationEventArgs(MaxValChanged));
  //          MessageBus.Default.BeginNotify(StatusTxtChanged, null, new NotificationEventArgs(StatusTxtChanged));
  //          MessageBus.Default.BeginNotify(StatusValueChanged, null, new NotificationEventArgs(StatusValueChanged));
  //          MessageBus.Default.BeginNotify(StatusVisibilityChanged, null, new NotificationEventArgs(StatusVisibilityChanged));
			
  //          Refresh();
		//}

  //      public static void Refresh()
  //      {
  //          if (Application.Current != null)
  //              Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send, new ThreadStart(delegate { }));
  //      }

  //      public static void RefreshNow()
  //      {
  //          if (Application.Current != null)
  //              Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send, new ThreadStart(delegate { }));
  //      }
	   

		//public static void StatusUpdate( string operation = "" ,double value = 0)
		//{
		//   _statusValue = value == 0 ? _statusValue += 1 : value;
		//	_operation = operation == ""? _operation: operation;
  //          if (_statusVisibility == Visibility.Hidden) _statusVisibility = Visibility.Visible;
		//	_statusTxt = string.Format("{0} | {1} of {2}", _operation, _statusValue, _maxVal);
		//	if(_maxVal == _statusValue) StopStatusUpdate();

  //          MessageBus.Default.BeginNotify(MaxValChanged, null, new NotificationEventArgs(MaxValChanged));
  //          MessageBus.Default.BeginNotify(StatusTxtChanged, null, new NotificationEventArgs(StatusTxtChanged));
  //          MessageBus.Default.BeginNotify(StatusValueChanged, null, new NotificationEventArgs(StatusValueChanged));
  //          MessageBus.Default.BeginNotify(StatusVisibilityChanged, null, new NotificationEventArgs(StatusVisibilityChanged));
  //          Refresh();
		//}

  //      public static void Message(string message)
  //      {
  //          if (_statusVisibility == Visibility.Hidden) _statusVisibility = Visibility.Visible;
  //          _statusTxt = string.Format("{0}", message);
  //          MessageBus.Default.BeginNotify(StatusTxtChanged, null, new NotificationEventArgs(StatusTxtChanged));
  //          MessageBus.Default.BeginNotify(StatusVisibilityChanged, null, new NotificationEventArgs(StatusVisibilityChanged));
  //          Refresh();
  //      }

  //      public static void Error(string message)
  //      {
  //          if (_statusVisibility == Visibility.Hidden) _statusVisibility = Visibility.Visible;
  //          _errorTxt = string.Format("{0}", message);
  //          MessageBus.Default.BeginNotify(ErrorTxtChanged, null, new NotificationEventArgs(ErrorTxtChanged));
  //          MessageBus.Default.BeginNotify(StatusVisibilityChanged, null, new NotificationEventArgs(StatusVisibilityChanged));
  //          Refresh();
  //      }

		//public static void StopStatusUpdate()
		//{
		//	_statusValue = 0;
		//	_maxVal = 0;
		//	_operation = "";
		//	_statusTxt = "";
		//	_statusVisibility = Visibility.Hidden;
		//	if (timer.Enabled == true) timer.Enabled = false;
  //          MessageBus.Default.BeginNotify(MaxValChanged, null, new NotificationEventArgs(MaxValChanged));
  //          MessageBus.Default.BeginNotify(StatusTxtChanged, null, new NotificationEventArgs(StatusTxtChanged));
  //          MessageBus.Default.BeginNotify(StatusValueChanged, null, new NotificationEventArgs(StatusValueChanged));
  //          MessageBus.Default.BeginNotify(StatusVisibilityChanged, null, new NotificationEventArgs(StatusVisibilityChanged));
  //          Refresh();
		//}

		

		//private static DateTime _startTime ;
		//private static readonly Timer timer = new Timer(1000);
		//public static void Timer(string operation)
		//{
		//	_statusValue = 0;
		//	_maxVal = 10;
		//	_operation = operation;
		//    _statusTxt = operation;//"";
		//	_statusVisibility = Visibility.Visible;
		//	_startTime = DateTime.Now;
		//	if (timer.Enabled == true) timer.Enabled = false;
		//	timer.Enabled = true;
  //          MessageBus.Default.BeginNotify(MaxValChanged, null, new NotificationEventArgs(MaxValChanged));
  //          MessageBus.Default.BeginNotify(StatusTxtChanged, null, new NotificationEventArgs(StatusTxtChanged));
  //          MessageBus.Default.BeginNotify(StatusValueChanged, null, new NotificationEventArgs(StatusValueChanged));
  //          MessageBus.Default.BeginNotify(StatusVisibilityChanged, null, new NotificationEventArgs(StatusVisibilityChanged));
  //          Refresh();
		//}

		//static void timer_Elapsed(object sender, ElapsedEventArgs e)
		//{
		//   _statusValue = _statusValue == _maxVal ? 0 : _statusValue += 1;
		//	_statusTxt = string.Format("{0} | {1}", _operation, (DateTime.Now - _startTime).ToString(@"hh\:mm\:ss"));
  //          MessageBus.Default.BeginNotify(StatusTxtChanged, null, new NotificationEventArgs(StatusTxtChanged));
  //          MessageBus.Default.BeginNotify(StatusValueChanged, null, new NotificationEventArgs(StatusValueChanged));
  //          Refresh();
		//}


	    public static void Timer(string gettingData)
	    {
	        
	    }

	    public static void StopStatusUpdate()
	    {
	        
	    }

	    public static void Error(string s)
	    {
	       
	    }

	    public static void Message(string message)
	    {
	        
	    }
	}
}