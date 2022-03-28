using Gestfac.Exceptions;
using Gestfac.Services.Creators;
using Gestfac.Services.Providers;
using Gestfac.Services.Updaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.Models
{
    public class Catalog
    {

        private readonly IProvider<Product> productProvider;
        private readonly ICreator<Product> productCreator;
        private readonly IUpdater<Product> productUpdater;


        public Catalog(IProvider<Product> productProvider, ICreator<Product> productCreator, IUpdater<Product> productUpdater)
        {
            this.productProvider = productProvider;
            this.productCreator = productCreator;
            this.productUpdater = productUpdater;
        }

        public async Task<IEnumerable<Product>> GetProductsByDescriptionAsync(string typedText)
        {
            IEnumerable<Product> allProducts = await productProvider.GetAll();
            
            return allProducts.Where(p => p.Description.Contains(typedText));
        }

        public async Task<IEnumerable<Product>> GetProductsByTagAsync(string tag)
        {
            IEnumerable<Product> allProducts = await productProvider.GetAll();

            return allProducts.Where(p => p.Tags.Contains(tag));
        }

        public async Task AddProductAsync(Product product)
        {
            IEnumerable<Product> allProducts = await productProvider.GetAll();
            if (allProducts.Contains(product))
            {
                throw new ExistingProductException(product);
            }

            await productCreator.Create(product);
        }

        public async Task UpdatePriceAsync(IEnumerable<Product> updatedProducts)
        {
            
            await productUpdater.Update(updatedProducts);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            IEnumerable<Product> allProducts = await productProvider.GetAll();
            return allProducts;
        }
    }
}
