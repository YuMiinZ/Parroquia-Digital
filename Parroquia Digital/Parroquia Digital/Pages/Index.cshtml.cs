using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parroquia_Digital.Pages
{
    public class IndexModel : PageModel
    {
        public string CustomTitle { get; set; }

        public void OnGet()
        {
            CustomTitle = "Parroquia de San Miguel de Desamparados";
        }
    }
    
}
