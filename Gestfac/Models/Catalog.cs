using Gestfac.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.Models
{
    public class Catalog
    {
        private readonly List<Product> _products;

        public Catalog()
        {
            _products = new List<Product>();
        }

        public IEnumerable<Product> GetProductsByDescription(string typedText)
        {
            return _products.Where(p => p.Description.Contains(typedText));
        }

        public void AddProduct(Product product)
        {
            if (_products.Contains(product))
            {
                throw new ExistingProductException(product);
            }

            _products.Add(product);
        }
    }
}
