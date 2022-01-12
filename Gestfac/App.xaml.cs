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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    string connectionString = hostContext.Configuration.GetConnectionString("Default");

                    services.AddSingleton(new GestfacDbContextFactory(connectionString));
                    services.AddSingleton<IProvider<Product>, DatabaseProductProvider>();
                    services.AddSingleton<ICreator<Product>, DatabaseProductCreator>();
                    services.AddSingleton<Catalog>();
                    services.AddSingleton<CatalogStore>();
                    services.AddSingleton<NavigationStore>();

                    services.AddTransient<AddProductViewModel>();
                    services.AddSingleton<Func<AddProductViewModel>>((s) => () => s.GetRequiredService<AddProductViewModel>());

                    services.AddTransient((s) => CreateProductListingViewModel(s));
                    services.AddSingleton<Func<ProductListingViewModel>>((s) => () => s.GetRequiredService<ProductListingViewModel>());// 

                    services.AddSingleton<NavigationService<ProductListingViewModel>>();
                    services.AddSingleton<NavigationService<AddProductViewModel>>();

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });

                })
                .Build();
            
        }

        private ProductListingViewModel CreateProductListingViewModel(IServiceProvider s)
        {
           return ProductListingViewModel.LoadViewModel(
               s.GetRequiredService<CatalogStore>(),
               s.GetRequiredService<NavigationService<AddProductViewModel>>()
               );
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            GestfacDbContextFactory dbContextFactory = _host.Services.GetRequiredService<GestfacDbContextFactory>();
            using (GestfacDbContext dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            NavigationService<ProductListingViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<ProductListingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            
            MainWindow.Show();
             
            base.OnStartup(e);
        }

        //private ProductListingViewModel CreateProductListingViewModel()
        //{
        //    return ProductListingViewModel.LoadViewModel(catalogStore, new NavigationService(_navigationStore, CreateAddProductViewModel));
        //}

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
