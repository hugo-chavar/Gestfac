using Gestfac.Commands;
using Gestfac.Models;
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
        private readonly Catalog _catalog;

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

        public ProductListingViewModel(Catalog catalog, Services.NavigationService addProductViewNavigationService)
        {
            _catalog = catalog;
            FindCommand = new FindProductsCommand(catalog, this);
            NewProductCommand = new NavigateCommand(addProductViewNavigationService);
            _products = new ObservableCollection<ProductViewModel>();

            UpdateProducts();
        }

        private void UpdateProducts()
        {
            foreach (var product in _catalog.GetAllProducts())
            {
                ProductViewModel productViewModel = new ProductViewModel(product);
                _products.Add(productViewModel);
            }
        }
    }
}
