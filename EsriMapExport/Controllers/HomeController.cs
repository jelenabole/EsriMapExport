using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EsriMapExport.Models;
using EsriMapExport.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsriMapExport.Controllers
{
    public class HomeController : Controller
    {

        MapExport ExportedMap;

        public HomeController()
        {
            Start();
        }

        // async method
        async private void Start()
        {
            MapForm MapForm = CreateMapObject();

            MapExport MapExport = await GetMap(MapForm);

            SaveImage(MapExport, "image", MapForm.Format);
        }

        private MapForm CreateMapObject()
        {
            MapForm Map = new MapForm
            {
                Xmin = 344245.2921116756,
                Ymin = 4999090.151073363,
                Xmax = 344698.726736262,
                Ymax = 4999225.360926013,

                // cm to inches (96 dpi) = inches of paper
                Width = 4096,
                Height = 4096,
                MapScale = 10000,

                Format = "png",
            };

            Map.Layers = new List<int>();
            Map.Layers.Add(0);
            Map.Layers.Add(3);

            return Map;
        }


        async private Task<MapExport> GetMap(MapForm mapForm)
        {
            // get map data from server:
            MapService restService = new MapService();
            ExportedMap = await restService.GetMapExport(mapForm);

            return ExportedMap;
        }

        async private void SaveImage(MapExport mapExport, String filename, String format)
        {
            if (format == null)
                format = "png";
            await DownloadService.DownloadImage(new Uri(mapExport.Href), filename + "." + format);
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
