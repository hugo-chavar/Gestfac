using Gestfac.Models;
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
        private readonly Catalog catalog;
        private readonly ProductListingViewModel productListingViewModel;

        public FindProductsCommand(Catalog catalog, ProductListingViewModel productListingViewModel)
        {
            this.catalog = catalog;
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
