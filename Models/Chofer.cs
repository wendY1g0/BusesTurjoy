using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Buses.Models;

namespace Buses.Models
{
   
    public class Chofer
    {
       
        public int IdChofer { get; set; }

        public string Rut { get; set; }

        public string Nombre { get; set; }

        public int Kilometraje { get; set; }

        public int EsHabilitado { get; set; }
    }
}