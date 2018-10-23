using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EsriMapExport.Models;
using EsriMapExport.Forms;
using System;
using Newtonsoft.Json;
using System.IO;
using EsriMapExport.Inputs;
using EsriMapExport.Services;
using EsriMapExport.Adds;
using System.Globalization;
using System.Collections.Generic;
using static EsriMapExport.Inputs.UrbanisticPlansResults;
using System.Threading.Tasks;

namespace EsriMapExport.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
            MapDataInput mapData = DeserializeJsonFile();
        }

        // async method
        async private void StartWithJsonFile()
        {
            MapDataInput mapData = DeserializeJsonFile();
        }

        private static MapDataInput DeserializeJsonFile()
        {
            // stream from a file:
            var serializer = new JsonSerializer();
            string str = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                    + "\\" + "copy.json";

            using (var sr = new StreamReader(str))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return (MapDataInput)serializer.Deserialize(sr, typeof(MapDataInput));
            }
        }


        /* REST - functions to delete */

        public IActionResult Index()
        {
            return View();
        }
    }
}
