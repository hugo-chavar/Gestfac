using Gestfac.Models;
using Gestfac.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gestfac.Commands
{
    public class LoadProductsCommand : AsyncCommandBase
    {
        private readonly Catalog catalog;
        private readonly ProductListingViewModel productListingViewModel;

        public LoadProductsCommand(Catalog catalog, ProductListingViewModel productListingViewModel)
        {
            this.catalog = catalog;
            this.productListingViewModel = productListingViewModel;

        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                IEnumerable<Product> products = await catalog.GetAllProductsAsync();
                productListingViewModel.UpdateProducts(products);

            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo cargar los productos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
