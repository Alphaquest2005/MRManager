﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CashSummaryManager.ViewModels;
using MNIB_Distribution_Manager;

namespace CashSummaryManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            InitializeComponent();
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            do
                exception = exception.InnerException;
            while (exception.InnerException != null);

            MessageBox.Show(exception.Message + "|" + Properties.Settings.Default.CashSummaryConnectionString);
        }

        
        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();

        }
        private void SwitchWindowState(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                return;
            }
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;

            }
        }
        private void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
           
        }



        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReportViewer.Visibility = Visibility.Hidden;
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = CashBreakDown.Instance;
        }

        private void ToDrawerSelector(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = DrawerSelector.Instance;
        }
    }
}
