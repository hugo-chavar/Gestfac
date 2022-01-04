using Gestfac.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Gestfac.DbContexts
{
    public class GestfacDbContext : DbContext
    {
        public GestfacDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProductDTO> Products { get; set; }
    }
}
