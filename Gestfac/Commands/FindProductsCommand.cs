using Gestfac.Models;
using Gestfac.Stores;
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
    public class FindProductsCommand : CommandBase
    {
        private readonly CatalogStore catalogStore;
        private readonly ProductListingViewModel productListingViewModel;

        public FindProductsCommand(CatalogStore catalogStore, ProductListingViewModel productListingViewModel)
        {
            this.catalogStore = catalogStore;
            this.productListingViewModel = productListingViewModel;

            this.productListingViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return
                !string.IsNullOrWhiteSpace(productListingViewModel.SearchText) &&
                productListingViewModel.SearchText.Length > 2 &&
                base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {

            try
            {
                
                productListingViewModel.UpdateProducts(catalogStore.Products);

            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo filtrar los productos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }


        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ProductListingViewModel.SearchText))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
