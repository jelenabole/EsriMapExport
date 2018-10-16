using EsriMapExport.Forms;
using EsriMapExport.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

        public async Task<MapExport> getMapExport(double Xmin, double Ymin, double Xmax, double Ymax)
        {
            // link to map:
            string server = "https://gdiportal.gdi.net/server/rest/services/PGZ/PGZ_UI_QUERY_DATA/MapServer/export";
            string rest = "?";

            rest += "f=json";
            rest += "&bbox=" + Xmin + "," + Ymin + "," + Xmax + "," + Ymax;

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

        public async Task<MapExport> getMapExport(MapForm MapForm)
        {
            // link to map:
            string server = "https://gdiportal.gdi.net/server/rest/services/PGZ/PGZ_UI_QUERY_DATA/MapServer/export";
            string rest = "?";

            rest += "f=json";
            // extent (bounding box) 
            rest += "&bbox=" + MapForm.Xmin.ToString(CultureInfo.InvariantCulture) + "," 
                + MapForm.Ymin.ToString(CultureInfo.InvariantCulture) + "," 
                + MapForm.Xmax.ToString(CultureInfo.InvariantCulture) + ","
                + MapForm.Ymax.ToString(CultureInfo.InvariantCulture);

            Trace.WriteLine(rest);
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
