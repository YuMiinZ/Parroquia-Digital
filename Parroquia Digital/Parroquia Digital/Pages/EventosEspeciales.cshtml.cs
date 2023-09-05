using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Parroquia_Digital.Pages
{
    public class EventosEspecialesModel : PageModel
    {
        public string CustomTitle { get; set; }

        public void OnGet()
        {
            CustomTitle = "Eventos Especiales";
        }
    }
}
