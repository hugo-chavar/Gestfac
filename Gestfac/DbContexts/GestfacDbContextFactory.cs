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
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new GestfacDbContext(options);
        }
    }
}
