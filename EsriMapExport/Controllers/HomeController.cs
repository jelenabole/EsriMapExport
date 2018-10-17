using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EsriMapExport.Models;
using EsriMapExport.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EsriMapExport.Services;

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


            // get map once (with defined layers):
            MapExport MapExport = await GetMap(MapForm);
            if (MapExport.Href != null)
                SaveImage(MapExport, "image", MapForm.Format);
        }
        
        async private Task<MapExport> GetMap(MapForm mapForm)
        {
            // get map data from server:
            MapService restService = new MapService();
            ExportedMap = await restService.GetMapExport(mapForm);

            return ExportedMap;
        }

        async private void SaveImage(MapExport mapExport, String filename, string format = "png")
        {
            if (format == null)
                format = "png";
            await DownloadService.DownloadImage(new Uri(mapExport.Href), filename + "." + format);
        }

        private MapForm CreateMapObject()
        {
            MapForm mapForm = new MapForm
            {
                Xmin = 344245.2921116756,
                Ymin = 4999090.151073363,
                Xmax = 344698.726736262,
                Ymax = 4999225.360926013,

                MapScale = 1000,
                Format = "png",

                // Layers = { 3 },
            };

            SetSize(mapForm);

            return mapForm;
        }

        private void SetSize(MapForm map)
        {
            // get the dpi (image = 96, vector = 300): 
            Decimal dpi = 96;

            // paper size in inches (without margins):
            Decimal paperWidth = 8.27M;
            Decimal paperHeight = 11.69M;


            map.Width = (int) Math.Round(dpi * paperWidth);
            map.Height = (int) Math.Round(dpi * paperHeight);
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
