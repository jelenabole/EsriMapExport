using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EsriMapExport.Models;
using System;

namespace EsriMapExport.Controllers
{
    public class HomeController : Controller
    {

        MapExport Map;

        public HomeController()
        {
            getMap();
        }

        async public void getMap()
        {
            Service restService = new Service();

            Extent Bounds = new Extent();
            Bounds.Xmin = 344245.2921116756;
            Bounds.Ymin = 4999090.151073363;
            Bounds.Xmax = 344698.726736262;
            Bounds.Ymax = 4999225.360926013;
            
            Map = await restService.getMapExport(Bounds.Xmin, Bounds.Ymin,
               Bounds.Xmax, Bounds.Ymax);

            Trace.WriteLine(Map.Height);
            Trace.WriteLine(Map.Width);
            Trace.WriteLine(Map.Href);
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
