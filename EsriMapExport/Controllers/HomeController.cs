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
            RestService restService = new RestService();
            Map = await restService.getMapExport();

            Trace.WriteLine(Map.Height);
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
