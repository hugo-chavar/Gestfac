using Gestfac.DbContexts;
using Gestfac.DTOs;
using Gestfac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.Services.Creators.ProductCreators
{
    public class DatabaseProductCreator : ICreator<Product>
    {
        private GestfacDbContextFactory _dbContextFactory;

        public DatabaseProductCreator(GestfacDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task Create(Product product)
        {
            using (GestfacDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                ProductDTO productDTO = ToProductDTO(product);
                dbContext.Products.Add(productDTO);
                await dbContext.SaveChangesAsync();
            }
        }

        private ProductDTO ToProductDTO(Product product)
        {
            return new ProductDTO()
            {
                ExternalId = product.ExternalId,
                Description = product.Description,
                CurrentPrice = product.CurrentPrice,
                TagsSerialized = product.Tags != null ? string.Join(";", product.Tags) : string.Empty
            };
        }
    }
}
