using Gestfac.Models;
using Gestfac.Stores;
using Gestfac.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.Commands
{
    public class FindProductsCommand : AsyncCommandBase
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

        public override Task ExecuteAsync(object parameter)
        {
            throw new NotImplementedException();
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
