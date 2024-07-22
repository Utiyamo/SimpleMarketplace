using DC.SimpleMarketplace.Infrastructure.ORM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Test.Mocks
{
    public static class DCContextMocker
    {
        public static DCContext GetMarketplaceContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DCContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var dbContext = new DCContext(options);
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
