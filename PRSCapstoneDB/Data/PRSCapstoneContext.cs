using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRSCapstoneDB.Models;

namespace PRSCapstoneDB.Data
{
    public class PRSCapstoneContext : DbContext
    {
        public PRSCapstoneContext (DbContextOptions<PRSCapstoneContext> options)
            : base(options)
        {
        }

        public DbSet<PRSCapstoneDB.Models.User> Users { get; set; }
        public DbSet<PRSCapstoneDB.Models.Vendor> Vendors { get; set; }
        public DbSet<PRSCapstoneDB.Models.Product> Products { get; set; }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            builder.Entity<User>(e =>
            {
                e.HasIndex(x => x.Username).IsUnique();
            });

            builder.Entity<Vendor>(e =>
            {
                e.HasIndex(x => x.Code).IsUnique();
            });

            builder.Entity<Product>(e =>
            {
                e.HasIndex(x => x.PartNbr).IsUnique();
            });
        }


    }
}
