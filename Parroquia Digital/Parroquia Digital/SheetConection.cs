using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Google.Apis.Services;

namespace Parroquia_Digital
{
    public static class SheetConection
    {
        //Attributes
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private static SheetsService service;
        private static string SpreadsheetId; //Unique ID of the GSheet, found in the URL.
        private static string sheet; //Sheet to be read, found in the tabs at the bottom of the GSheet.

        //Static All-in-One Method
        //startCell: most up-left cell of the range  |  endCell: most down-right cell of the range
        public static IList<IList<object>> ReadSheet(string sheetId, string sheetName, string startCell, string endCell)
        {
            SpreadsheetId = sheetId;
            sheet = sheetName;

            CreateCredential(sheet + "Application");
            return ReadEntries(startCell, endCell);
        }

        //Grants credentials to the Service Account for it to access the GSheet
        private static void CreateCredential(string appName)
        {
            var applicationName = appName;

            GoogleCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read)) //Service Account's key
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });
        }

        //Reads the GSheet in the specified range and returns the data as a list of lists.
        //startCell: most up-left cell of the range  |  endCell: most down-right cell of the range
        //If returns null, then the sheet is empty.
        private static IList<IList<object>> ReadEntries(string startCell, string endCell)
        {
            var range = $"{sheet}!{startCell}:{endCell}";
            var request = service.Spreadsheets.Values.Get(SpreadsheetId, range); //Prepares the reading request.
            var response = request.Execute(); //Reads the GSheet as Google API Data.
            var values = response.Values; //Gets the values as a list of list.

            if (values != null && values.Count > 0)
            {
                return values;
            }
            else
            {
                return null;
            }
        }
    }
}
