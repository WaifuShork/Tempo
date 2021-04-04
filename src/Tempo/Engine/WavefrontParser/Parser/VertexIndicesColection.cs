using System.Collections.Generic;
using System.Linq;

namespace Tempo.Wavefront.Parser
{
    public class VertexIndicesCollection : IVertexCollection
    {
        private int[] _items;

        public VertexIndicesCollection(IEnumerable<int> vertexes)
        {
            _items = vertexes.ToArray();
        }

        public ElementType Type => ElementType.Vertex;

        public bool Contains(ElementType faceType)
        {
            if (faceType == Type)
            {
                return true;
            }

            return false;
        }

        public int[] GetElements(ElementType faceType)
        {
            if (faceType == Type)
            {
                return _items;
            }

            return null;
        }
    }
}