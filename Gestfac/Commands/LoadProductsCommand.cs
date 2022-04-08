using Gestfac.Stores;
using Gestfac.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Gestfac.Commands
{
    public class LoadProductsCommand : AsyncCommandBase
    {
        private readonly CatalogStore catalogStore;
        private readonly ProductListingViewModel productListingViewModel;

        public LoadProductsCommand(CatalogStore catalogStore, ProductListingViewModel productListingViewModel)
        {
            this.catalogStore = catalogStore;
            this.productListingViewModel = productListingViewModel;

        }

        public override async Task ExecuteAsync(object parameter)
        {
            productListingViewModel.IsLoading = true;

            try
            {
                await catalogStore.Load();
                productListingViewModel.UpdateProducts(catalogStore.Products);

            }
            catch (Exception e)
            {

                MessageBox.Show("No se pudo cargar los productos. " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            productListingViewModel.IsLoading = false;
        }
    }
}
