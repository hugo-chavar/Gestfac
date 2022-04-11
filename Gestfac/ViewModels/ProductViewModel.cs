using Gestfac.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public string PriceString
        {
            get
            {
                return string.Format(CultureInfo.GetCultureInfo("es-AR"), "{0:N2}", _product.CurrentPriceUpdate.Price);
            }

            set { }
        }

        public string Tags => string.Join(", ", _product.Tags.ToArray());

        public Product Product => _product;
    }
}
