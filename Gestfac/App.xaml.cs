using Gestfac.Exceptions;
using Gestfac.Models;
using Gestfac.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Gestfac
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Catalog catalog;

        public App()
        {
            catalog = new Catalog();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(catalog)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
