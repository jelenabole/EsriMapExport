using System.Collections.Generic;

namespace EsriMapExport.Forms
{
    

    public class MapForm
    {
        public MapForm()
        {
            List<int> Layers = new List<int>();
        }

        // extent:
        public double Xmin { get; set; }
        public double Ymin { get; set; }
        public double Xmax { get; set; }
        public double Ymax { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        // layer IDs
        public List<int> Layers { get; set; }
        public LayerDefinition layerDefinition { get; set; }

        // map scale: 5 000 000 ili 5E6
        public int MapScale { get; set; }
    }

    // feature layer
    public class LayerDefinition
    {
        public int Id { get; set; }
    }

}