using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EsriMapExport.Models;
using EsriMapExport.Forms;
using System;
using Newtonsoft.Json;
using System.IO;
using EsriMapExport.Inputs;
using EsriMapExport.TestUtils;

namespace EsriMapExport.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
            StartWithJsonFile();
        }

        // async method
        async private void StartWithJsonFile()
        {
            MapDataInput mapData = DeserializeJsonFile();

            // create map object to export - borders, layers:
            MapForm mapForm = getDataToForm(mapData);

            // set paper size (with padding or scale):
            CalcUtils.SetPaperSize(mapForm);

            TestStart.SaveMap(mapForm);
        }


        private MapForm getDataToForm(MapDataInput mapData)
        {
            // get borders from geometry:
            Extent borders = CalcUtils.FindPoints(mapData.SpatialConditionList);

            // create map object to export - borders, layers:
            MapForm mapForm = new MapForm
            {
                Xmin = borders.Xmin,
                Xmax = borders.Xmax,
                Ymin = borders.Ymin,
                Ymax = borders.Ymax,
            };

            return mapForm;
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
