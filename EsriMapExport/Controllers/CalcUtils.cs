using System.Diagnostics;
using EsriMapExport.Models;
using EsriMapExport.Forms;
using System;
using System.Collections.Generic;
using EsriMapExport.Inputs;

namespace EsriMapExport.Controllers
{
    public class CalcUtils
    {
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

        public static Extent FindPoints(List<SpatialCondition> spatialConditions)
        {
            Extent borders = new Extent
            {
                Xmin = Double.MaxValue,
                Xmax = 0,
                Ymin = Double.MaxValue,
                Ymax = 0
            };

            // features = particles:
            for (int j = 0; j < spatialConditions.Count; j++)
            {
                List<List<double>> points = spatialConditions[j].Geometry.Rings[0];

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

    }
}