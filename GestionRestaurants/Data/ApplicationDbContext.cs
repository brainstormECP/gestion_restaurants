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
            modelBuilder.Entity<RestriccionDeReserva>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<GestionRestaurants.Models.Restaurant> Restaurant { get; set; }

        public DbSet<GestionRestaurants.Models.Reserva> Reserva { get; set; }

        public DbSet<GestionRestaurants.Models.HorarioDeReserva> HorarioDeReserva { get; set; }
    }
}
