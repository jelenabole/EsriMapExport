using System.Collections.Generic;

namespace EsriMapExport.Forms
{
    public class QueryForm
    {
        public QueryForm()
        {
            ParticleNumbers = new List<string>();
        }

        // for where field - particle numbers for filter:
        public List<string> ParticleNumbers { get; set; }
    }
}