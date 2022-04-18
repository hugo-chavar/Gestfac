using Gestfac.Commands;
using Gestfac.Models;
using Gestfac.Services;
using Gestfac.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                FindCommand.Execute(null);
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

        private string _valuePriceUpdate;

        public string ValuePriceUpdate
        {
            get
            {
                return _valuePriceUpdate;
            }
            set
            {
                _valuePriceUpdate = value;
                OnPropertyChanged(nameof(ValuePriceUpdate));
            }
        }

        private PriceUpdateType _selectedPriceUpdateType;

        public PriceUpdateType SelectedPriceUpdateType
        {
            get
            {
                return _selectedPriceUpdateType;
            }
            set
            {
                _selectedPriceUpdateType = value;
                OnPropertyChanged(nameof(SelectedPriceUpdateType));
            }
        }

        public string ProductsCount
        {
            get
            {
                return $"{_products.Count}";
            }
        }

        private readonly ObservableCollection<PriceUpdateType> _priceUpdateTypes;

        public IEnumerable<PriceUpdateType> PriceUpdateTypes => _priceUpdateTypes;

        public IEnumerable<ProductViewModel> Products => _products;
        public ICommand FindCommand { get; }

        public ICommand NewProductCommand { get; }

        public ICommand LoadProductsCommand { get; }

        public ICommand UpdatePricesCommand { get; }

        public ProductListingViewModel(CatalogStore catalogStore, NavigationService<AddProductViewModel> addProductViewNavigationService)
        {
            FindCommand = new FindProductsCommand(catalogStore, this);
            LoadProductsCommand = new LoadProductsCommand(catalogStore, this);
            NewProductCommand = new NavigateCommand<AddProductViewModel>(addProductViewNavigationService);
            UpdatePricesCommand = new UpdateProductCommand(catalogStore, this);
            _products = new ObservableCollection<ProductViewModel>();
            _priceUpdateTypes = new ObservableCollection<PriceUpdateType>();

            _priceUpdateTypes.Add(new PriceUpdateType() { Id = 1, Description = "Porcentaje" });
            _priceUpdateTypes.Add(new PriceUpdateType() { Id = 2, Description = "Valor fijo" });

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

            OnPropertyChanged(nameof(ProductsCount));
        }
    }
}
