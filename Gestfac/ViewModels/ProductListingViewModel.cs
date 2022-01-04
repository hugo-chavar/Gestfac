using Gestfac.Commands;
using Gestfac.Models;
using Gestfac.Services;
using Gestfac.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Gestfac.ViewModels
{
    public class ProductListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ProductViewModel> _products;

        private string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public IEnumerable<ProductViewModel> Products => _products;
        public ICommand FindCommand { get; }

        public ICommand NewProductCommand { get; }

        public ICommand LoadProductsCommand { get; }

        public ProductListingViewModel(Catalog catalog, NavigationService addProductViewNavigationService)
        {
            FindCommand = new FindProductsCommand(catalog, this);
            LoadProductsCommand = new LoadProductsCommand(catalog, this);
            NewProductCommand = new NavigateCommand(addProductViewNavigationService);
            _products = new ObservableCollection<ProductViewModel>();

        }

        public static ProductListingViewModel LoadViewModel(Catalog catalog, NavigationService addProductViewNavigationService)
        {
            ProductListingViewModel viewModel = new ProductListingViewModel(catalog, addProductViewNavigationService);

            viewModel.LoadProductsCommand.Execute(null);

            return viewModel;
        }

        public void UpdateProducts(IEnumerable<Product> products)
        {

            foreach (var product in products)
            {
                ProductViewModel productViewModel = new ProductViewModel(product);
                _products.Add(productViewModel);
            }
        }
    }
}
