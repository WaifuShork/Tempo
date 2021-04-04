using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.IO;
using System.Threading;

namespace Tempo.Wavefront
{
    public class Model
    {

        private DispMode _dispMode = DispMode.Normal;

        public DispMode DisplayMode
        {
            get => _dispMode;
            set
            {
                foreach (var part in Parts.Values)
                {
                    part.DisplayMode = value;
                }
                _dispMode = value;
            }
        }
        
        public Geometry Geometry { get; set; }
        public Dictionary<string, Material> Materials { get; set; } = new();
        public Dictionary<string, Part> Parts { get; set; } = new();

        public static Model Load(SourceType sourceType, string path)
        {
            switch (sourceType)
            {
                case SourceType.OBJ:
                    return new Parser.Parser().LoadModel(path);
                default:
                    throw new Exception("File type not supported");
            }
        }
    }
}