using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.DbContexts
{
    public class GestfacDesignTimeDbContextFactory : IDesignTimeDbContextFactory<GestfacDbContext>
    {
        public GestfacDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data source=gestfac.db").Options;

            return new GestfacDbContext(options);
        }
    }
}
