using Gestfac.Models;
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
        public ICommand FindProductsCommand { get; }

        public ProductListingViewModel()
        {
            _products = new ObservableCollection<ProductViewModel>();

            //TODO: hardcoded values
            _products.Add(new ProductViewModel(new Product() { ExternalId = "AABB11", Description = "DISYUNTOR 2 X 40 SUPER INMUNIZADO ", CurrentPrice = 22.55 }));
            _products.Add(new ProductViewModel(new Product() { ExternalId = "BBBB22", Description = "CAÑO ESTRUCTURAL 60 X 40 X 1,6 mm ", CurrentPrice = 17.05 }));
            _products.Add(new ProductViewModel(new Product() { ExternalId = "CCBB11", Description = "BALASTO PARA LAMPARA DE MERCURIO HALOGENADO 400W ", CurrentPrice = 5.50 }));
        }

    }
}
