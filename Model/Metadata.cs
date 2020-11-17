using System.Collections.Generic;

namespace Model
{
    public class Metadata
    {
        public string Name { get; set; }
        public List<Instruments> Instruments { get; set; }
        public string Location { get; set; }

        public Metadata()
        {
            Instruments = new();
        }
    }
}