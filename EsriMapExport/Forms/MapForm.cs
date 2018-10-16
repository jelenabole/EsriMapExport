using System.Collections.Generic;

namespace EsriMapExport.Forms
{

    public class MapForm
    {
        public MapForm()
        {
            List<int> Layers = new List<int>();
        }

        // additional info:
        public string Format { get; set; }

        // extent (bbox):
        public double Xmin { get; set; }
        public double Ymin { get; set; }
        public double Xmax { get; set; }
        public double Ymax { get; set; }

        // map size:
        public int? Width { get; set; }
        public int? Height { get; set; }

        // map scale:
        public int? MapScale { get; set; }

        // layer IDs and definitions:
        public List<int> Layers { get; set; }
        public LayerDefinition layerDefinition { get; set; }
    }

    // feature layer
    public class LayerDefinition
    {
        public int Id { get; set; }
    }

}