using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace EsriMapExport.Controllers
{
    class DownloadService
    {
        public static async Task DownloadImage(Uri requestUri, string filename)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            using (
                Stream contentStream = await (await client.SendAsync(request)).Content.ReadAsStreamAsync(),
                stream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    + "\\" + filename, FileMode.Create, FileAccess.Write, FileShare.None, 3145728, true))
            {
                await contentStream.CopyToAsync(stream);
            }
        }
    }
}
