using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrsCapstoneBackEnd.Models;

namespace PrsCapstoneBackEnd.Data
{
    public class PrsCapstoneBackEndContext : DbContext
    {
        public PrsCapstoneBackEndContext (DbContextOptions<PrsCapstoneBackEndContext> options)
            : base(options)
        {
        }

        public DbSet<PrsCapstoneBackEnd.Models.Product> Product { get; set; }

        public DbSet<PrsCapstoneBackEnd.Models.Request> Request { get; set; }

        public DbSet<PrsCapstoneBackEnd.Models.RequestLine> RequestLine { get; set; }

        public DbSet<PrsCapstoneBackEnd.Models.User> User { get; set; }

        public DbSet<PrsCapstoneBackEnd.Models.Vendor> Vendor { get; set; }

        /*FROM TqlPoWebApi PoContext.cs
         *   protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>(e =>
            {
                e.HasIndex(p => p.Login).IsUnique();
            });
        }
         * 
         */
    }
}
