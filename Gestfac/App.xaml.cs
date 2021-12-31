using Gestfac.Exceptions;
using Gestfac.Models;
using Gestfac.Services;
using Gestfac.Stores;
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
        private readonly NavigationStore _navigationStore;

        public App()
        {
            catalog = new Catalog();
            _navigationStore = new NavigationStore();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateProductListingViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private AddProductViewModel CreateAddProductViewModel()
        {
            return new AddProductViewModel(catalog, new NavigationService(_navigationStore, CreateProductListingViewModel));
        }

        private ProductListingViewModel CreateProductListingViewModel()
        {
            return new ProductListingViewModel(catalog, new NavigationService(_navigationStore, CreateAddProductViewModel));
        }
    }
}
