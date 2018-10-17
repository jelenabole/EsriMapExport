using System;

namespace EsriMapExport.Models
{
    public class MapExport
    {
        public String Href { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Extent Extent { get; set; }
        public double Scale { get; set; }
    }

    public class Extent
    {
        public double Xmin { get; set; }
        public double Ymin { get; set; }
        public double Xmax { get; set; }
        public double Ymax { get; set; }
        // public SpatialReference SpatialReference { get; set; }
    }
}