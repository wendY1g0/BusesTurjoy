using buses;  // Asegúrate de usar el espacio de nombres correcto
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;  // Asegúrate de importar este espacio de nombres

namespace buses.Pages
{
    public class ListaBusModel : PageModel
    {
        private readonly TurjoyContext _context;

        public List<Bus> ListaBus { get; set; }

        // Inyección del contexto a través del constructor
        public ListaBusModel(TurjoyContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // Cargar los datos de Bus desde la base de datos
            ListaBus = _context.Bus.ToList();
        }
    }
}
