using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Buses.Models;
namespace Buses.Models
{
    public class Viaje
    {
        public int IdViaje { get; set; }
        public DateTime Fecha { get; set; }

        public int TramoId { get; set; }
        public Tramo Tramo { get; set; }

        public int BusId { get; set; }
        public Bus Bus { get; set; }

        public int ChoferId { get; set; }
        public Chofer Chofer { get; set; }
    }
    
}