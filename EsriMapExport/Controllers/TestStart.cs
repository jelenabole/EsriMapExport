using EsriMapExport.Models;
using EsriMapExport.Forms;
using EsriMapExport.Services;
using System.Diagnostics;
using System;
using System.Threading.Tasks;

namespace EsriMapExport.TestUtils
{
    public class TestStart
    {


        async public static void SaveMap(MapForm mapForm)
        {
            MapExport MapExport = await GetMap(mapForm);
            if (MapExport.Href != null)
                SaveImage(MapExport, "map_image", mapForm.Format);
        }

        async public static Task<MapExport> GetMap(MapForm mapForm)
        {
            // get map data from server:
            MapService restService = new MapService();
            MapExport ExportedMap = await restService.GetMapExport(mapForm);

            Trace.WriteLine(" .. " + ExportedMap.Width + " x " + ExportedMap.Height);
            Trace.WriteLine(ExportedMap.Href);

            return ExportedMap;
        }

        async public static void SaveImage(MapExport mapExport, String filename, string format = "png")
        {
            if (format == null)
                format = "png";
            await DownloadService.DownloadImage(new Uri(mapExport.Href), filename + "." + format);
        }


    }
}