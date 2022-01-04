using Gestfac.Exceptions;
using Gestfac.Models;
using Gestfac.Services;
using Gestfac.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gestfac.Commands
{
    public class AddProductCommand : AsyncCommandBase
    {
        private readonly Catalog catalog;
        private readonly AddProductViewModel addProductViewModel;
        private readonly NavigationService addProductViewNavigationService;

        public AddProductCommand(AddProductViewModel addProductViewModel, Catalog catalog, NavigationService navigationService)
        {
            this.catalog = catalog;
            this.addProductViewModel = addProductViewModel;
            this.addProductViewNavigationService = navigationService;
            this.addProductViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return
                !string.IsNullOrWhiteSpace(addProductViewModel.ExternalId) &&
                !string.IsNullOrWhiteSpace(addProductViewModel.Description) &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Product product = new Product()
            {
                ExternalId = addProductViewModel.ExternalId,
                Description = addProductViewModel.Description,
                CurrentPrice = addProductViewModel.CurrentPrice,
                Tags = addProductViewModel.Tags?.Split(' ').ToList()
            };

            try
            {
                await catalog.AddProductAsync(product);
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
            if (e.PropertyName == nameof(AddProductViewModel.ExternalId) || e.PropertyName == nameof(AddProductViewModel.Description))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
