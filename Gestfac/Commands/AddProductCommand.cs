using Gestfac.Exceptions;
using Gestfac.Models;
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
    public class AddProductCommand : CommandBase
    {
        private readonly Catalog catalog;
        private readonly AddProductViewModel addProductViewModel;

        public AddProductCommand(AddProductViewModel addProductViewModel, Catalog catalog)
        {
            this.catalog = catalog;
            this.addProductViewModel = addProductViewModel;
            this.addProductViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return
                !string.IsNullOrWhiteSpace(addProductViewModel.ExternalId) &&
                !string.IsNullOrWhiteSpace(addProductViewModel.Description) &&
                base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
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
                catalog.AddProduct(product);
                MessageBox.Show("Producto agregado correctamente", "Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ExistingProductException ex)
            {

                MessageBox.Show("Producto ya existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
