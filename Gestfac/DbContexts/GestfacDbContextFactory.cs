using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.DbContexts
{
    public class GestfacDbContextFactory
    {
        private readonly string _connectionString;

        public GestfacDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public GestfacDbContext CreateDbContext()
        {
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
            dbContextOptionsBuilder.UseSqlite("Data source=gestfac.db");
            DbContextOptions options = dbContextOptionsBuilder.Options;

            return new GestfacDbContext(options);
        }
    }
}
