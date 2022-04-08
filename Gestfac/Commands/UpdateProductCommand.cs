using Gestfac.Models;
using Gestfac.Stores;
using Gestfac.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace Gestfac.Commands
{
    public class UpdateProductCommand : CommandBase
    {
        private readonly CatalogStore catalogStore;
        private readonly ProductListingViewModel productListingViewModel;

        public UpdateProductCommand(CatalogStore catalogStore, ProductListingViewModel productListingViewModel)
        {
            this.catalogStore = catalogStore;
            this.productListingViewModel = productListingViewModel;

            this.productListingViewModel.PropertyChanged += OnViewModelPropertyChanged;

        }

        public override bool CanExecute(object parameter)
        {
            double valuePriceUpdate;
            double.TryParse(productListingViewModel.ValuePriceUpdate, out valuePriceUpdate);
            return
                productListingViewModel.SelectedPriceUpdateType != null &&
                valuePriceUpdate > 0 &&
                (productListingViewModel.SelectedPriceUpdateType.Id == 1 ? valuePriceUpdate  <= 100 : true) &&
                !productListingViewModel.IsLoading &&
                productListingViewModel.Products.Any() &&
                base.CanExecute(parameter);
        }

        public override async void Execute(object parameter)
        {
            productListingViewModel.IsLoading = true;

            try
            {
                IEnumerable<Product> products = productListingViewModel.Products.Select(pvm => pvm.Product);

                double valuePriceUpdate;
                double.TryParse(productListingViewModel.ValuePriceUpdate, out valuePriceUpdate);
                productListingViewModel.ValuePriceUpdate = "";

                if (productListingViewModel.SelectedPriceUpdateType.Id == 1)
                    valuePriceUpdate = valuePriceUpdate / 100;

                foreach (Product product in products)
                {
                    product.CurrentPriceUpdate.Date = DateTime.Today;

                    if (productListingViewModel.SelectedPriceUpdateType.Id == 1)
                    {
                        product.CurrentPriceUpdate.Price += product.CurrentPriceUpdate.Price * valuePriceUpdate;
                    }
                    else
                    {
                        product.CurrentPriceUpdate.Price += valuePriceUpdate;
                    }

                    product.CurrentPriceUpdate.Price = Math.Round(product.CurrentPriceUpdate.Price, 2);
                }

                IEnumerable<List<Product>> chunks = SplitList(products.ToList());

                foreach (List<Product> chunk in chunks)
                {
                    await Task.Run(() => catalogStore.UpdateProducts(chunk));
                }

                

                productListingViewModel.UpdateProducts(catalogStore.Products);

                productListingViewModel.IsLoading = false;


                MessageBox.Show("Producto actualizado correctamente", "Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception e)
            {

                productListingViewModel.IsLoading = false;

                MessageBox.Show("No se pudo actualizar los precios de los productos-  " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ProductListingViewModel.SelectedPriceUpdateType) || e.PropertyName == nameof(ProductListingViewModel.ValuePriceUpdate))
            {
                OnCanExecuteChanged();
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }
    }
}
