using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Google.Apis.Sheets.v4;

namespace Parroquia_Digital.Pages
{
    public class GruposModel : PageModel
    {
        static readonly string SpreadsheetId = "1lsOwRmj1-NcnuFh8j0naaoiWDd1r_NTg6CX_fBbgoGQ";

        public string CustomTitle { get; set; }

        public void OnGet()
        {
            CustomTitle = "Grupos";

            var result = SheetConection.ReadSheet(SpreadsheetId, "Grupos", "A2", "C5");

            foreach (var row in result)
            {
                Console.WriteLine("{0} | {1} | {2}", row[0], row[1], row[2]);
            }
        }
    }
}
