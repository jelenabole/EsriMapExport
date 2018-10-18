using System;
using System.Collections.Generic;

namespace EsriMapExport.Models
{
    public class Query
    {
        // enum:
        public String GeometryType { get; set; }
        public SpatialReference SpatialReference { get; set; }
        public List<Fields> Fields { get; set; }
        public List<Features> Features { get; set; }
    }

    public class SpatialReference
    {
        public int Wkid { get; set; }
        public int LatestWkid { get; set; }
    }

    public class Fields
    {
        public string Name { get; set; }
        // enum:
        public string Type { get; set; }
        // ???
        public int Length { get; set; }
    }

    public class Features
    {
        // nepotrebno ??
        public Attributes Attributes { get; set; }
        public Geometry Geometry { get; set; }
    }

    public class Attributes
    {
        public string KC_BROJ { get; set; }
    }

    public class Geometry
    {
        // Rings[0][0] = List <xPoint, yPoint>:
        public List<List<List<double>>> Rings { get; set; }
    }
}