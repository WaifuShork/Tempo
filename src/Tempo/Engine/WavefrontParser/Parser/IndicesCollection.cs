using System.Collections.Generic;
using System.Linq;

namespace Tempo.Wavefront.Parser
{
    public class IndicesCollection : IVertexCollection
    {
        private IVertexCollection _collection;
        private int[] _items;

        public IndicesCollection(ElementType collectionType, IEnumerable<int> indices)
        {
            Type = collectionType;
            _items = indices.ToArray();
        }
        
        public ElementType Type { get; private set; }

        public bool Contains(ElementType faceType)
        {
            if (faceType == Type)
            {
                return true;
            }

            else
            {
                if (_collection != null)
                {
                    return _collection.Contains(faceType);
                }
            }

            return false;
        }

        public int[] GetElements(ElementType faceType)
        {
            if (faceType == Type)
            {
                return _items;
            }
            else
            {
                if (_collection != null)
                {
                    return _collection.GetElements(faceType);
                }
            }

            return null;
        }

        public void Extend(IVertexCollection collection)
        {
            _collection = collection;
        }
    }
}