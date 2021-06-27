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

        public DbSet<PrsCapstoneBackEnd.Models.User> Users { get; set; }

        public DbSet<PrsCapstoneBackEnd.Models.Vendor> Vendor { get; set; }

        /*F NEED THIS FOR VENDOR, USER AND PRODUCT  ALL NEED TO BE UNIQUE
          */
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(u =>
            {
                u.HasIndex(u => u.Username).IsUnique();
            });

            builder.Entity<Vendor>(v =>
            {
                v.HasIndex(v => v.Code).IsUnique();
            });

            builder.Entity<Product>(p =>
            {
                p.HasIndex(p => p.PartNbr).IsUnique();
            });


        }
       
    }
}
