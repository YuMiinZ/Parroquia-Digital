using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Google.Apis.Services;

namespace Parroquia_Digital.Pages
{
    public class GruposModel : PageModel
    {
        public string CustomTitle { get; set; }

        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string SpreadsheetId = "1lsOwRmj1-NcnuFh8j0naaoiWDd1r_NTg6CX_fBbgoGQ";
        static readonly string sheet = "Grupos";
        static SheetsService service;


        public void OnGet()
        {
            CustomTitle = "Grupos";

            CreateCredential();

            ReadEntries();
        }

        static void CreateCredential()
        {
            var applicationName = "Grupos";

            GoogleCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });
        }

        static void ReadEntries()
        {
            var range = $"{sheet}!A2:C5";
            var request = service.Spreadsheets.Values.Get(SpreadsheetId, range);

            var response = request.Execute();
            var values = response.Values;

            if(values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    Console.WriteLine("{0} | {1} | {2}", row[0], row[1], row[2]);
                }
            }
            else
            {
                Console.WriteLine("No hay datos");
            }
        }
    }
}
