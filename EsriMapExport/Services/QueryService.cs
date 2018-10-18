using EsriMapExport.Forms;
using EsriMapExport.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace EsriMapExport.Services
{
	class QueryService
	{
		HttpClient client;

		public QueryService()
		{
            client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
        }

        public async Task<Query> GetMapQuery(QueryForm queryForm)
        {
            // create map link:
            String uri = CreateUrl(queryForm);
            Trace.WriteLine(uri);

            // json settings = ignore null fields:
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            // get map data:
            var response = await client.GetAsync(uri);
            var Item = new Query();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Trace.Write(content);
                Item = JsonConvert.DeserializeObject<Query>(content, jsonSettings);
            }
            return Item;
        }

        private String CreateUrl(QueryForm queryForm)
        {
            // link to server:
            // link za katastarsku česticu:
            string server = "https://gdiportal.gdi.net/server/rest/services/PGZ/PGZ_UI_QUERY_DATA/MapServer/5/query";
            
            // arguments
            string args = "?";
            args += "f=json&";

            // broj čestice:
            args += "where=";

            string kcArg = "";
            int size = queryForm.ParticleNumbers.Count;
            for (int i = 0; i < size; i++)
            {
                kcArg = "";
                
                kcArg = "KC_BROJ=" + "'" + queryForm.ParticleNumbers[i] + "'";
                if (i < size - 1)
                {
                    kcArg += " OR ";
                }

                args += HttpUtility.UrlEncode(kcArg);
            }

            // stuff to return:
            /*
            args += "&returnGeometry=true&returnTrueCurves=true"
                + "&returnIdsOnly=false&returnCountOnly=false"
                + "&returnZ=true&returnM=true"
                + "&returnDistinctValues=false&returnExtentOnly=false"
                + "&f=json";
            */
            Trace.WriteLine("args: \n" + args);

            return server + args;
        }
    }
}
