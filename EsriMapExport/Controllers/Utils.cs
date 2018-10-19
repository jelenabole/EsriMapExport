using System.Diagnostics;
using EsriMapExport.Models;
using EsriMapExport.Forms;
using System;
using System.Collections.Generic;

namespace EsriMapExport.Controllers
{
    public class Utils
    {

        /* CREATE DEFAULT OBJECT */

        public static MapForm CreateMapObject()
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

            // TESTING:
            // SetLayerDefsShowOneEmpty(mapForm);
            // SetLayerDefsShowOneOnMap(mapForm);

            // SetPaperSize(mapForm);

            return mapForm;
        }

        public static void SetPaperSize(MapForm map)
        {
            // get the dpi (image = 96, vector = 300): 
            Decimal dpi = 96;

            // paper size in inches (without margins):
            Decimal paperWidth = 8.27M - (1.25M * 2.0M);
            Decimal paperHeight = 11.69M - 2;

            map.Width = (int)Math.Round(dpi * paperWidth);
            map.Height = (int)Math.Round(dpi * paperHeight);
        }


        /* SET LAYERS */

        public static void SetLayerDefsShowOnEmpty(MapForm mapForm)
        {
            mapForm.Layers.Add(5);
            mapForm.LayerDefs = new List<LayerDefinition>()
            {
                new LayerDefinition
                {
                    LayerId = 5,
                    Query = "KC_BROJ='1012/17'"
                }
            };
        }

        public static void SetLayerDefsShowOneOnMap(MapForm mapForm)
        {
            // 0 = Granice JLS, 5 = Kat.čestica, 13 = prostorni planovi
            mapForm.Layers.Add(5);
            mapForm.Layers.Add(0);
            mapForm.Layers.Add(13);
            mapForm.LayerDefs = new List<LayerDefinition>()
            {
                // shown only 2 particles:
                new LayerDefinition
                {
                    LayerId = 5,
                    Query = "KC_BROJ='1012/15' OR KC_BROJ='1012/17'"
                }
            };
        }

        public static Extent FindPoints(List<Features> features)
        {
            Extent borders = new Extent
            {
                Xmin = Double.MaxValue,
                Xmax = 0,
                Ymin = Double.MaxValue,
                Ymax = 0
            };

            // features = particles:
            for (int j = 0; j < features.Count; j++)
            {
                List<List<double>> points = features[j].Geometry.Rings[0];

                // particle border points:
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i][0] < borders.Xmin)
                        borders.Xmin = points[i][0];
                    if (points[i][0] > borders.Xmax)
                        borders.Xmax = points[i][0];

                    if (points[i][1] < borders.Ymin)
                        borders.Ymin = points[i][1];
                    if (points[i][1] > borders.Ymax)
                        borders.Ymax = points[i][1];
                }
            }

            return borders;
        }

        public static void SetMapPadding(MapForm mapForm)
        {
            double width = mapForm.Xmax - mapForm.Xmin;
            double height = mapForm.Ymax - mapForm.Ymin;

            double widthPadding = width / 2;
            double heightPadding = height / 2;

            mapForm.Xmax += widthPadding;
            mapForm.Xmin -= widthPadding;
            mapForm.Ymax += heightPadding;
            mapForm.Ymin -= heightPadding;
        }

    }
}