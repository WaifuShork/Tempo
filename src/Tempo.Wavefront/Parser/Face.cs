using System.Collections.Generic;
using System.Linq;

namespace Tempo.Wavefront.Parser
{
    public partial class Face
    {
        private IVertexCollection _collection;

        private Face(ElementType faceType, IEnumerable<int> vertexes, IEnumerable<int> texCoords, IEnumerable<int> normals)
        {
            var vertexList = vertexes.ToList();
            MeshSize = vertexList.Count();
            
            if (faceType.HasFlag(ElementType.Vertex))
            {
                Type = ElementType.Vertex;
                _collection = new VertexIndicesCollection(vertexList);
            }

            if (faceType.HasFlag(ElementType.Normal))
            {
                Type |= ElementType.Normal;
                var normal = new IndicesCollection(ElementType.Normal, normals);

                if (_collection != null)
                {
                    normal.Extend(_collection);
                }

                _collection = normal;
            }

            if (faceType.HasFlag(ElementType.TextureCoord))
            {
                Type |= ElementType.TextureCoord;
                var texCoord = new IndicesCollection(ElementType.TextureCoord, texCoords);

                if (_collection != null)
                {
                    texCoord.Extend(_collection);
                }

                _collection = texCoord;
            }
        }

        public int MeshSize { get; set; }
        public ElementType Type { get; private set; }

        public static Face Parse(IEnumerable<string> face)
        {
            return Parser.CreateFace(face);
        }

        public int[] GetElements(ElementType faceType)
        {
            return _collection.GetElements(faceType);
        }

        public bool Contains(ElementType faceType)
        {
            return _collection.Contains(faceType);
        }
        
        
    }
}