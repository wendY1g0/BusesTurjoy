using buses;
using Microsoft.EntityFrameworkCore;  
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Buses.Models;


namespace Buses.Pages
{
    public class RegistroViajeModel : PageModel
    {

        private readonly BusesContext contexto;

        public RegistroViajeModel(BusesContext context)
        {
            contexto = context;
        }

        public List<Bus> ListaBuses { get; set; }
        public List<Chofer> ListaChoferes { get; set; }
        public List<Tramo> ListaTramos { get; set; }
        public List<Viaje> ListaViajes { get; set; }

        public int ChoferId { get; set; }
        public int BusId { get; set; }
        public int TramoId { get; set; }


        public RegistroViaje NuevoRegistro { get; set; } = new RegistroViaje();

        public void OnGet()
        {
            ListaBuses = contexto.Buses.Where(b => b.EsHabilitado == 1).ToList();
            ListaChoferes = contexto.Choferes.Where(c => c.EsHabilitado == 1).ToList();
            ListaTramos = contexto.Tramos.ToList();
            ListaViajes = contexto.Viajes.Include(v=> v.Tramo)
                                            .Include(v=> v.Bus)
                                            .Include(v=> v.Chofer).ToList();
            
        }

        public IActionResult OnPost()
        {
            var chofer = contexto.Choferes.FirstOrDefault(c => c.IdChofer == ChoferId);
            
            var bus = contexto.Buses.FirstOrDefault(b => b.IdBus == BusId);
            
            var tramo = contexto.Tramos.FirstOrDefault(t => t.IdTramo == TramoId);

         
              
             var viaje = new Viaje
                {
                    Fecha = DateTime.Now,
                    TramoId = tramo.IdTramo,
                    BusId = bus.IdBus,
                    ChoferId = chofer.IdChofer
                };
                contexto.Viajes.Add(viaje);
                contexto.SaveChanges();

                bus.Kilometraje += tramo.Distancia;
                chofer.Kilometraje += tramo.Distancia;
                contexto.SaveChanges();
               

                return RedirectToPage();    
        }
    }
}