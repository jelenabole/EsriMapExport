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
            // create object, set values
            MapForm mapForm = Utils.CreateMapObject();
            Utils.SetPaperSize(mapForm);
            Utils.SetLayerDefsShowOneOnMap(mapForm);

            SaveMap(mapForm);
        }

        async private void SaveMap(MapForm mapForm)
        {
            MapExport MapExport = await GetMap(mapForm);
            if (MapExport.Href != null)
                SaveImage(MapExport, "map_image", mapForm.Format);
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



        /* REST - functions to delete */

        public IActionResult Index()
        {
            return View();
        }
    }
}
