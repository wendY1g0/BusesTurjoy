using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Buses.Models;

namespace Buses.Pages
{
    public class listaChoferModel : PageModel
    {
        public List<Chofer> listaChofer { get; set; }
        public void OnGet()
        {
            BusesContext busesContext = new BusesContext();

            listaChofer = busesContext.Choferes.ToList();
        }
    }
}
