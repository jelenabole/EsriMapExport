using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EsriMapExport.Models;
using EsriMapExport.Forms;
using System;
using System.Collections.Generic;

namespace EsriMapExport.Controllers
{
    public class HomeController : Controller
    {

        MapExport ExportedMap;

        public HomeController()
        {
            getMap();
        }

        async public void getMap()
        {
            Service restService = new Service();

            MapForm MapForm = new MapForm
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

            MapForm.Layers = new List<int>();
            MapForm.Layers.Add(0);
            MapForm.Layers.Add(3);
            
            ExportedMap = await restService.getMapExport(MapForm);

            Trace.WriteLine(ExportedMap.Height);
            Trace.WriteLine(ExportedMap.Width);
            Trace.WriteLine(ExportedMap.Href);

            // save image:
            if (MapForm.Format == null)
                MapForm.Format = "png";
            await DownloadService.DownloadImage(new Uri(ExportedMap.Href), "image." + MapForm.Format);
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
