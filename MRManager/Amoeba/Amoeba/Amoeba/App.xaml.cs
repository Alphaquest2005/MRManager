
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
			if (File.Exists("Amoeba-Logs.xml")) File.Delete("Amoeba-Logs.xml");
			Logger.Initialize();
		}
	}
}
