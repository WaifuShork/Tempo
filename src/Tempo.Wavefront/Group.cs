using System.Collections.Generic;
using Tempo.Wavefront.Parser;

namespace Tempo.Wavefront
{
    public class Group
    {
        public Material Material { get; set; }
        public bool Selected { get; set; }

        public bool Visible { get; set; } = true;
        public bool Transparent { get; set; }

        public List<Face> Faces = new List<Face>();
    }
}