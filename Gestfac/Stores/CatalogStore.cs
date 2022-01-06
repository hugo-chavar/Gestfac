using Gestfac.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestfac.Stores
{
    public class CatalogStore
    {
        private readonly List<Product> _products;
        private readonly Lazy<Task> _initializeLazy;
        private readonly Catalog _catalog;

        public CatalogStore(Catalog catalog)
        {
            _products = new List<Product>();
            _catalog = catalog;
            _initializeLazy = new Lazy<Task>(Initialize);
        }

        public IEnumerable<Product> Products => _products;

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        public async Task AddProduct(Product product)
        {
            await _catalog.AddProductAsync(product);
            
            _products.Add(product);
        }

        private async Task Initialize()
        {
            IEnumerable<Product> products = await _catalog.GetAllProductsAsync();

            _products.Clear();
            _products.AddRange(products);
        }
    }
}
