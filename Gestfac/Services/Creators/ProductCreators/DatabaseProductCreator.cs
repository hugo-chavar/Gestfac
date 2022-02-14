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
                var priceUpdateDTO = dbContext.PriceUpdates.Add(new PriceUpdateDTO() {  Date = product.CurrentPriceUpdate.Date, Price = product.CurrentPriceUpdate.Price });
                productDTO.PriceUpdates.Add(priceUpdateDTO.Entity);
                
                dbContext.Products.Add(productDTO);
                await dbContext.SaveChangesAsync();

                productDTO.CurrentPriceUpdate = priceUpdateDTO.Entity;
                dbContext.Products.Update(productDTO);
                await dbContext.SaveChangesAsync();
            }
        }

        private ProductDTO ToProductDTO(Product product)
        {
            return new ProductDTO()
            {
                ExternalId = product.ExternalId,
                Description = product.Description,
                PriceUpdates = new List<PriceUpdateDTO>(),
                TagsSerialized = product.Tags != null ? string.Join(";", product.Tags) : string.Empty
            };
        }
    }
}
