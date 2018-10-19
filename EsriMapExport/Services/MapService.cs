using EsriMapExport.Forms;
using EsriMapExport.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace EsriMapExport.Services
{
	class MapService
	{
		HttpClient client;

		public MapService()
		{
            client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
        }

        public async Task<MapExport> GetMapExport(MapForm mapForm)
        {
            // create map link:
            String uri = CreateUrl(mapForm);

            // json settings = ignore null fields:
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            // get map data:
            var response = await client.GetAsync(uri);
            var Item = new MapExport();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Item = JsonConvert.DeserializeObject<MapExport>(content, jsonSettings);
            }
            return Item;
        }

        private String CreateUrl(MapForm mapForm)
        {
            // link to server:
            string server = "https://gdiportal.gdi.net/server/rest/services/PGZ/PGZ_UI_QUERY_DATA/MapServer/export";

            // arguments:
            string args = "?";

            // data format (html):
            args += "f=json";

            // extent (bounding box required):
            args += "&bbox=" + mapForm.Xmin.ToString(CultureInfo.InvariantCulture) + ","
                + mapForm.Ymin.ToString(CultureInfo.InvariantCulture) + ","
                + mapForm.Xmax.ToString(CultureInfo.InvariantCulture) + ","
                + mapForm.Ymax.ToString(CultureInfo.InvariantCulture);

            // image format: (png)
            mapForm.Format = mapForm.Format == null ? "png" : mapForm.Format;
            args += "&format=" + mapForm.Format;

            // image size (400x400):
            if (mapForm.Width != null && mapForm.Height != null)
                args += "&size=" + mapForm.Width + "," + mapForm.Height;

            // map scale (??):
            if (mapForm.MapScale != null)
                args += "&mapScale=" + mapForm.MapScale;

            args += AddLayersAndDefinitions(mapForm);

            return server + args;
        }

        private String AddLayersAndDefinitions(MapForm mapForm)
        {
            string args = "";

            // show specific layers (and all its sublayers) - by IDs:
            if (mapForm.Layers != null && mapForm.Layers.Count > 0)
            {
                args += "&layers=show:";
                int size = mapForm.Layers.Count;
                for (int i = 0; i < size; i++)
                {
                    args += mapForm.Layers[i];
                    if (i < size - 1)
                        args += ",";
                }
            }

            // show by definitions:
            if (mapForm.LayerDefs != null && mapForm.LayerDefs.Count > 0)
            {
                args += "&layerDefs=";
                int size = mapForm.LayerDefs.Count;

                for (int i = 0; i < size; i++)
                {
                    args += mapForm.LayerDefs[i].LayerId + ":";

                    // replace special chars:
                    string str = mapForm.LayerDefs[i].Query;
                    str = str.Replace(" ", "+");
                    str = str.Replace("\"", "%22");
                    str = str.Replace(":", "%3A");
                    str = str.Replace("'", "%27");
                    args += str;
                }
            }

            return args;
        }
    }
}
