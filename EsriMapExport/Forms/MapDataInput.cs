using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EsriMapExport.Inputs
{
    [DataContract]
    public sealed class MapDataInput
    {
        [DataMember]
        public List<SpatialCondition> SpatialConditionList;
        [DataMember]
        public List<UrbanisticPlansResults> UrbanisticPlansResults;

        public MapDataInput()
        {
            SpatialConditionList = new List<SpatialCondition>();
            UrbanisticPlansResults = new List<UrbanisticPlansResults>();
        }
    }

    // for particles:
    public class SpatialCondition
    {
        public string Source;
        public string Type;
        public string Description;
        public Geometry Geometry;
    }

    public class Geometry
    {
        public List<List<List<double>>> Rings { get; set; }
    }

    // for map export:
    public class UrbanisticPlansResults
    {
        public int Id;
        public string Status;
        public string Type;
        public string GisCode;
        public string Name;

        // urls:
        public string Sn;
        public string PolygonRestURL;
        public string RasterRestURL;
        public string LegenRestURL;
        public string ComponentRestURL;

        public List<PlanMap> PlanMaps;

        public class PlanMap
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int MapScale { get; set; }
            public int? OriginalScale { get; set; }
        }
    }
}