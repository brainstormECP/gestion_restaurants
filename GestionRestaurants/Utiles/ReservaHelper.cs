using GestionRestaurants.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.Utiles
{
    public class ReservaHelper
    {
        DbContext _db;

        public ReservaHelper(DbContext context)
        {
            _db = context;
        }

        public bool SePuedeReservar(Reserva reserva)
        {
            var reservas = _db.Set<Reserva>()
                .Where(r => r.Fecha == reserva.Fecha && r.HoraId == reserva.HoraId && r.RestaurantId == reserva.RestaurantId)
                .OrderBy(r => r.CantidadDePersonas)
                .Select(r => r.CantidadDePersonas);
            var disponibilidades = _db.Set<RestriccionDeReserva>()
                .Where(r => r.RestaurantId == reserva.RestaurantId)
                .OrderBy(r => r.DisponibilidadDeMesa)
                .Select(r => r.DisponibilidadDeMesa)
                .ToList();            
            foreach (var item in reservas)
            {
                if (disponibilidades.Any(d => d == item))
                {
                    disponibilidades.Remove(item);
                }
                else
                {
                    bool seReserva = false;
                    foreach (var d in disponibilidades)
                    {
                        if (item < d)
                        {
                            disponibilidades.Remove(d);
                            seReserva = true;
                        }
                    }
                    if (!seReserva)
                    {

                    }
                }
            }
            //casos:
            //1. se pueden poner en una sola mesa exactamente
            //2. se pueden poner en una sola mesa pero sobran sillas
            //3. se pueden distribuir en varias mesas exactamente
            //4. se pueden distribuir en varias mesas y sobran sillas
            return true;
        }
    }
}
