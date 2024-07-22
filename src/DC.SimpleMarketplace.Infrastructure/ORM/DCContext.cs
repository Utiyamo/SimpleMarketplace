using DC.SimpleMarketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Infrastructure.ORM
{
    public class DCContext : DbContext
    {
        public DCContext(DbContextOptions<DCContext> options) : base(options) { }

        public DbSet<Enterprise> Enterprise { get; set; }
    }
}
