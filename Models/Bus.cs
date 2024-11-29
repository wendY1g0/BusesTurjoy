using System;
using System.Collections.Generic;
using Buses.Models;
namespace Buses.Models
{
    public class Bus
    {
      
        public int IdBus { get; set; }

        public string? CodigoBus { get; set; }

        public int Kilometraje { get; set; }

        public int? EsHabilitado { get; set; }
    }
}

