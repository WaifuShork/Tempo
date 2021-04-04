using System;
using System.Collections.Generic;
using System.Linq;

namespace Tempo.Wavefront.Parser
{
    partial class Face
    {
        private class Parser
        {
            private List<int> _vertexIndices;
            private List<int> _normalIndices;
            private List<int> _textureCoordinateIndices;

            public static Face CreateFace(IEnumerable<string> faces)
            {
                var faceList = faces.ToList();

                var parser = new Parser();
                var faceType = SetType(faceList);

                if (faceType.HasFlag(ElementType.Vertex))
                {
                    parser._vertexIndices = new List<int>();
                }

                if (faceType.HasFlag(ElementType.TextureCoord))
                {
                    parser._textureCoordinateIndices = new List<int>();
                }

                if (faceType.HasFlag(ElementType.Normal))
                {
                    parser._normalIndices = new List<int>();
                }

                foreach (var item in faceList)
                {
                    parser.ParseFaceItem(item, faceType);
                }

                return new Face(faceType, parser._vertexIndices, parser._textureCoordinateIndices, parser._normalIndices);
            }

            private void ParseFaceItem(string faceItem, ElementType faceType)
            {
                int position;
                int texCoord;
                int normal;
                bool success;

                var partsOfFaceItem = faceItem.Split(new char[] {'/', ' '});

                if (faceType.HasFlag(ElementType.Vertex))
                {
                    success = int.TryParse(partsOfFaceItem[0], out position);
                    if (success)
                    {
                        _vertexIndices.Add(position);
                    }
                }

                if (faceType.HasFlag(ElementType.TextureCoord))
                {
                    success = int.TryParse(partsOfFaceItem[1], out texCoord);                    
                    if (success)
                    {
                        _textureCoordinateIndices.Add(texCoord);
                    }
                }

                if (faceType.HasFlag(ElementType.Normal))
                {
                    success = int.TryParse(partsOfFaceItem[2], out normal);
                    if (success)
                    {
                        _normalIndices.Add(normal);
                    }
                }
            }
            
            
            
            

            private static ElementType SetType(IEnumerable<string> face)
            {
                var faceType = ElementType.Vertex;

                var faceItemGroups = face.GroupBy((item) => item.Where(c => c == '/').Count());

                if (faceItemGroups.Count() == 1)
                {
                    var faceGroup = faceItemGroups.Single();

                    switch (faceGroup.Key)
                    {
                        case 1:
                            faceType |= ElementType.TextureCoord;
                            break;
                        case 2:
                            faceType |= ElementType.Normal;

                            if (!faceGroup.First().Contains("//"))
                            {
                                faceType |= ElementType.TextureCoord;
                            }

                            break;
                    }
                }
                else
                {
                    throw new ArgumentException("Face damaged ");
                }

                return faceType;
            }
        }
    }
}