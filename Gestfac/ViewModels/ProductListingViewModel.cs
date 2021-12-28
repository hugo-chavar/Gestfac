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

        public IEnumerable<ProductViewModel> Products => _products;
        public ICommand FindProductsCommand { get; }

        public ProductListingViewModel()
        {
            _products = new ObservableCollection<ProductViewModel>();

            //TODO: hardcoded values
            _products.Add(new ProductViewModel(new Models.Product() { ExternalId = "AABB11", Description = "Tornillo", CurrentPrice = 22.55 }));
            _products.Add(new ProductViewModel(new Models.Product() { ExternalId = "BBBB22", Description = "Arandela", CurrentPrice = 17.05 }));
            _products.Add(new ProductViewModel(new Models.Product() { ExternalId = "CCBB11", Description = "Tuerca", CurrentPrice = 5.50 }));
        }

    }
}
