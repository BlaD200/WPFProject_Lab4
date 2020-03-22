using System;
using System.Windows;
using WPFProject_Lab4.Models;
using WPFProject_Lab4.Tools.DataStorage;

namespace WPFProject_Lab4.Tools.Managers
{
    internal static class StationManager
    {
        private static IDataStorage _dataStorage;

        internal static User CurrentUser { get; set; }

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
        
        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(1);
        }
    }
}
