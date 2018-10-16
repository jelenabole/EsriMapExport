using EsriMapExport.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EsriMapExport.Controllers
{
	class Service
	{
		HttpClient client;
		public Service()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
		}

        public async Task<MapExport> getMapExport()
        {
            // link to map:
            string server = "https://gdiportal.gdi.net/server/rest/services/PGZ/PGZ_UI_QUERY_DATA/MapServer/export";
            string rest = "?";

            rest += "f=json";
            rest += "&bbox=344245.2921116756,4999090.151073363,344698.726736262,4999225.360926013";

            string uri = string.Format(server + rest);

            var response = await client.GetAsync(uri);
            var Item = new MapExport();

            // json settings = ignore null fields:
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // json content:
                Trace.Write(content);
                Item = JsonConvert.DeserializeObject<MapExport>(content, jsonSettings);
            }
            return Item;
        }
    }
	
}
