using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Buses.Models;

namespace Buses.Pages
{
    public class listaBusModel : PageModel
    {
        public List<Bus> listaBus { get; set; }
        public void OnGet()
        {
            BusesContext busesContext= new BusesContext();
            listaBus = busesContext.Buses.ToList();

        }
    }
}
