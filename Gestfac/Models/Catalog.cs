using Gestfac.Exceptions;
using Gestfac.Services.Creators;
using Gestfac.Services.Providers;
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


        public Catalog(IProvider<Product> productProvider, ICreator<Product> productCreator)
        {
            this.productProvider = productProvider;
            this.productCreator = productCreator;
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
            IEnumerable<Product> allProducts = await productProvider.GetAll();

            List<Product> _products = allProducts.ToList();

            foreach (Product product in updatedProducts)
            {
                var current = _products.Find(p => p.Id == product.Id);
                
                if (current == null)
                {
                    throw new NotExistingProductException(product);
                }


                current.UpdatePrice(product.CurrentPrice);

            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            IEnumerable<Product> allProducts = await productProvider.GetAll();
            return allProducts;
        }
    }
}
