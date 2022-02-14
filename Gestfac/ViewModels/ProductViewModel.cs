using Gestfac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private Product _product;
        public ProductViewModel(Product product)
        {
            _product = product;
        }

        public string ExternalId => _product.ExternalId;
        public string Description => _product.Description;

        public double Price => _product.CurrentPriceUpdate.Price;

        public string Tags => string.Join(", ", _product.Tags.ToArray());
    }
}
