using Gestfac.DbContexts;
using Gestfac.DTOs;
using Gestfac.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.Services.Providers.ProductProviders
{
    public class DatabaseProductProvider : IProvider<Product>
    {
        private GestfacDbContextFactory _dbContextFactory;

        public DatabaseProductProvider(GestfacDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            using (GestfacDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ProductDTO> productDTOs = await dbContext.Products.ToListAsync();
                return productDTOs.Select(p => ToProduct(p));
            }
        }

        private static Product ToProduct(ProductDTO p)
        {
            return new Product()
            {
                ExternalId = p.ExternalId,
                Description = p.Description,
                CurrentPrice = p.CurrentPrice
            };
        }
    }
}
