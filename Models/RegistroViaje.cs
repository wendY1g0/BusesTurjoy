
using System.ComponentModel.DataAnnotations;
using System;
using Buses.Models;
using buses;

namespace Buses.Models
{
    public class RegistroViaje
    {
        public int Id { get; set; }
        public int IdViaje { get; set; }
        public int IdBus { get; set; }
        public int IdChofer { get; set; }
        public int IdTramo { get; set; }
        public DateTime Fecha { get; set; }

        
        public Bus Bus { get; set; }
        public Chofer Chofer { get; set; }
        public Tramo Tramo { get; set; }
        public Viaje Viaje { get; set; }
    }
}

