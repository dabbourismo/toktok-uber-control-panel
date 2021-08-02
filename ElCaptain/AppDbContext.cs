using ElCaptain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ElCaptain
{
    public class AppDbContext : System.Data.Entity.DbContext
    {
        public AppDbContext() : base("name=remote")
        {
            Database.Log = e => Debug.WriteLine(e);
        }
        //ControlPanel
        public DbSet<Client> Clients { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<DeliveryMan> DeliveryMen { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<OrderRefuses> OrderRefuses { get; set; }
        public DbSet<Pricing> Pricing { get; set; }
        public DbSet<VehicleOwner> VehicleOwners { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Store>().ToTable("Stores");
            modelBuilder.Entity<DeliveryMan>().ToTable("DeliveryMen");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Payment>().ToTable("Payments");
            modelBuilder.Entity<Menu>().ToTable("Menus");
            modelBuilder.Entity<Notification>().ToTable("Notifications");
            modelBuilder.Entity<OrderRefuses>().ToTable("OrderRefuses");
            modelBuilder.Entity<Pricing>().ToTable("Pricing");
            modelBuilder.Entity<VehicleOwner>().ToTable("VehicleOwners");
        }
    }
}
