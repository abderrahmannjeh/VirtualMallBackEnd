using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VirtualMallBackEnd.Entity;

namespace VirtualMallBackEnd.Context
{
    public class AppDbContext : IdentityDbContext<Account>
    {


        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company>Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasKey(table => new {
                table.CompanyID,
                table.ProductCode
            });

            base.OnModelCreating(builder);
        }

    }
}
