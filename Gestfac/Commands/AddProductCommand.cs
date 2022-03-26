using Gestfac.Exceptions;
using Gestfac.Models;
using Gestfac.Services;
using Gestfac.Stores;
using Gestfac.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Gestfac.Commands
{
    public class AddProductCommand : AsyncCommandBase
    {
        private readonly CatalogStore catalogStore;
        private readonly AddProductViewModel addProductViewModel;
        private readonly NavigationService<ProductListingViewModel> addProductViewNavigationService;

        public AddProductCommand(AddProductViewModel addProductViewModel, CatalogStore catalogStore, NavigationService<ProductListingViewModel> navigationService)
        {
            this.catalogStore = catalogStore;
            this.addProductViewModel = addProductViewModel;
            this.addProductViewNavigationService = navigationService;
            this.addProductViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return
                !string.IsNullOrWhiteSpace(addProductViewModel.ExternalId) &&
                !string.IsNullOrWhiteSpace(addProductViewModel.Description) &&
                addProductViewModel.CurrentPrice > 0 &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Product product = new Product()
            {
                ExternalId = addProductViewModel.ExternalId,
                Description = addProductViewModel.Description,
                CurrentPriceUpdate = new PriceUpdate() { Date = DateTime.Today, Price = addProductViewModel.CurrentPrice },
                Tags = addProductViewModel.Tags?.Split(' ').ToList()
            };

            try
            {
                await catalogStore.AddProduct(product);
                MessageBox.Show("Producto agregado correctamente", "Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);

                addProductViewNavigationService.Navigate();
            }
            catch (ExistingProductException)
            {

                MessageBox.Show("Producto ya existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo grabar el nuevo producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AddProductViewModel.ExternalId) || e.PropertyName == nameof(AddProductViewModel.Description) || e.PropertyName == nameof(AddProductViewModel.CurrentPrice))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
