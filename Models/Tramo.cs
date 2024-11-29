using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Buses.Models;

namespace Buses.Models
{
    public class Tramo
    {
    
        public int IdTramo { get; set; }

        public string Origen { get; set; }
    
        public string Destino { get; set; }

        public int Distancia { get; set; }
    }
}
