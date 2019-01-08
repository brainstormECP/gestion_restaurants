using System;
using System.Collections.Generic;
using System.Text;
using GestionRestaurants.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionRestaurants.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>();
            modelBuilder.Entity<Reserva>().HasOne(r => r.Restaurant).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<IndisponibilidadRestaurant>();
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<GestionRestaurants.Models.Restaurant> Restaurant { get; set; }
    }
}
