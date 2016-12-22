using System.IO;
using System.Windows;
using log4netWrapper;

namespace Amoeba
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			if (File.Exists("MRManager-Logs.xml")) File.Delete("MRManager-Logs.xml");
			Logger.Initialize();
		}
	}
}
