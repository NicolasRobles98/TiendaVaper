using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SysAdmin> SysAdmins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .ToTable("Users")  // Mapea toda la jerarquía a la misma tabla "Usuarios"
                .HasDiscriminator<UserRole>("UserRole")
                .HasValue<Customer>(UserRole.Customer)
                .HasValue<Owner>(UserRole.Owner)
                .HasValue<SysAdmin>(UserRole.SysAdmin);
        }
    }
}
