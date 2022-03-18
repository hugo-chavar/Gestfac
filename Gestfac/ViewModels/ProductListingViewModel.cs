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

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public IEnumerable<ProductViewModel> Products => _products;
        public ICommand FindCommand { get; }

        public ICommand NewProductCommand { get; }

        public ICommand LoadProductsCommand { get; }

        public ProductListingViewModel(CatalogStore catalogStore, NavigationService<AddProductViewModel> addProductViewNavigationService)
        {
            FindCommand = new FindProductsCommand(catalogStore, this);
            LoadProductsCommand = new LoadProductsCommand(catalogStore, this);
            NewProductCommand = new NavigateCommand<AddProductViewModel>(addProductViewNavigationService);
            _products = new ObservableCollection<ProductViewModel>();

        }

        public static ProductListingViewModel LoadViewModel(CatalogStore catalogStore, NavigationService<AddProductViewModel> addProductViewNavigationService)
        {
            ProductListingViewModel viewModel = new ProductListingViewModel(catalogStore, addProductViewNavigationService);

            viewModel.LoadProductsCommand.Execute(null);

            return viewModel;
        }

        public void UpdateProducts(IEnumerable<Product> products)
        {
            _products.Clear();

            Func<Product, bool> filter = (product) =>
            {
                if (_searchText == null || _searchText.Length < 2)
                {
                    return true;
                }
                return product.Description.ToUpper().Contains(_searchText.ToUpper());
            };
            
            foreach (var product in products.Where(filter))
            {
                ProductViewModel productViewModel = new ProductViewModel(product);
                _products.Add(productViewModel);
            }
        }
    }
}
