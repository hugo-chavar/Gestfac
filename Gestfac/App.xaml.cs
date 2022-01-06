using Gestfac.DbContexts;
using Gestfac.Exceptions;
using Gestfac.Models;
using Gestfac.Services;
using Gestfac.Services.Creators;
using Gestfac.Services.Creators.ProductCreators;
using Gestfac.Services.Providers;
using Gestfac.Services.Providers.ProductProviders;
using Gestfac.Stores;
using Gestfac.ViewModels;
using Microsoft.EntityFrameworkCore;
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
        private const string ConnectionString = "Data source=gestfac.db";
        private readonly Catalog catalog;
        private readonly CatalogStore catalogStore;
        private readonly NavigationStore _navigationStore;
        private readonly GestfacDbContextFactory _dbContextFactory;

        public App()
        {
            _dbContextFactory = new GestfacDbContextFactory(ConnectionString);
            IProvider<Product> productProvider = new DatabaseProductProvider(_dbContextFactory);
            ICreator<Product> productCreator = new DatabaseProductCreator(_dbContextFactory);
            catalog = new Catalog(productProvider, productCreator);
            catalogStore = new CatalogStore(catalog);
            _navigationStore = new NavigationStore();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            using (GestfacDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

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
            return new AddProductViewModel(catalogStore, new NavigationService(_navigationStore, CreateProductListingViewModel));
        }

        private ProductListingViewModel CreateProductListingViewModel()
        {
            return ProductListingViewModel.LoadViewModel(catalogStore, new NavigationService(_navigationStore, CreateAddProductViewModel));
        }
    }
}
