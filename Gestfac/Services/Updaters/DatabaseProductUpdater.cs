using Gestfac.DbContexts;
using Gestfac.DTOs;
using Gestfac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.Services.Updaters
{
    public class DatabaseProductUpdater : IUpdater<Product>
    {
        private GestfacDbContextFactory _dbContextFactory;

        public DatabaseProductUpdater(GestfacDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task Update(IEnumerable<Product> t)
        {
            using (GestfacDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                foreach (Product product in t)
                {
                    ProductDTO productDTO = ToProductDTO(product);
                    if (product.CurrentPriceUpdate.Date == DateTime.Today)
                    {
                        var priceUpdateDTO = dbContext.PriceUpdates.Add(new PriceUpdateDTO() { Date = product.CurrentPriceUpdate.Date, Price = product.CurrentPriceUpdate.Price });
                        productDTO.PriceUpdates.Add(priceUpdateDTO.Entity);
                        productDTO.CurrentPriceUpdate = priceUpdateDTO.Entity;
                    }

                    dbContext.Products.Update(productDTO);
                    await dbContext.SaveChangesAsync();
                }
                
            }
        }

        private static ProductDTO ToProductDTO(Product product)
        {
            return new ProductDTO()
            {
                ProductId = product.Id,
                ExternalId = product.ExternalId,
                Description = product.Description,
                PriceUpdates = new List<PriceUpdateDTO>(),
                TagsSerialized = product.Tags != null ? string.Join(";", product.Tags) : string.Empty
            };
        }
    }
}
