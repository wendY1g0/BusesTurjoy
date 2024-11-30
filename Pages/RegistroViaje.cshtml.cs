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

        public List<Bus> ListaBuses { get; set; } = new List<Bus>();
        public List<Chofer> ListaChoferes { get; set; } = new List<Chofer>();
        public List<Tramo> ListaTramos { get; set; } = new List<Tramo>();
        public List<Viaje> ListaViajes { get; set; } = new List<Viaje>();

        public int ChoferId { get; set; }
        public int BusId { get; set; }
        public int TramoId { get; set; }


        public RegistroViaje NuevoRegistro { get; set; } = new RegistroViaje();
        

        public void OnGet()
        {
        
                ListaBuses = contexto.Buses.Where(b => b.EsHabilitado == 1).ToList() ;
                ListaChoferes = contexto.Choferes.Where(c => c.EsHabilitado == 1).ToList();
                ListaTramos = contexto.Tramos.ToList();
                ListaViajes = contexto.Viajes.Include(v=> v.Tramo)
                                                .Include(v=> v.Bus)
                                                .Include(v=> v.Chofer).ToList();
                
          
        }

public IActionResult OnPost()
{
    ListaBuses = contexto.Buses.Where(b => b.EsHabilitado == 1).ToList();
    ListaChoferes = contexto.Choferes.Where(c => c.EsHabilitado == 1).ToList();
    ListaTramos = contexto.Tramos.ToList();
    ListaViajes = contexto.Viajes.Include(v => v.Tramo)
                                   .Include(v => v.Bus)
                                   .Include(v => v.Chofer).ToList();
    try
    {  
        using (BusesContext context = new BusesContext())
        {
            var chofer = contexto.Choferes.FirstOrDefault(c => c.IdChofer == ChoferId);
            if (chofer == null)
            {
                throw new Exception($"El chofer no existe.");
            }
            
            var bus = contexto.Buses.FirstOrDefault(b => b.IdBus == BusId);
            if (bus == null)
            {
                throw new Exception($"No existe el bus.");
            }
            
            var tramo = contexto.Tramos.FirstOrDefault(t => t.IdTramo == TramoId);
            if (tramo == null)
            {
                throw new Exception($"No existe un tramo.");
            }

            Viaje viaje = new Viaje
            {
                Fecha = DateTime.Now,
                TramoId = tramo.IdTramo,
                BusId = bus.IdBus,
                ChoferId = chofer.IdChofer
            };

            // Depuración: Imprimir el objeto Viaje
            Console.WriteLine($"Viaje creado: Fecha={viaje.Fecha}, TramoId={viaje.TramoId}, BusId={viaje.BusId}, ChoferId={viaje.ChoferId}");

            bus.Kilometraje += tramo.Distancia;
            chofer.Kilometraje += tramo.Distancia;

            contexto.Viajes.Add(viaje);

            // Depuración: Imprimir cambios antes de guardar
            Console.WriteLine($"Bus Kilometraje: {bus.Kilometraje}");
            Console.WriteLine($"Chofer Kilometraje: {chofer.Kilometraje}");

            contexto.SaveChanges();                    

            return RedirectToPage();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        ModelState.AddModelError(string.Empty, "Ocurrió un error al registrar el viaje.");
        return Page();
    }
}

}
}