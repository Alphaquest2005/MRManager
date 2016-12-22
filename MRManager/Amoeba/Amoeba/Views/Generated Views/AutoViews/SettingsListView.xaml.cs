﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class SettingsAutoViewList_AutoGen
	{
		public SettingsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("SettingsAutoViewModelDataSource") as SettingsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		SettingsAutoViewModel_AutoGen im;

	}
}
