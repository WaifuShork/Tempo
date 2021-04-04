using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using Tempo.Wavefront.Parser;

namespace Tempo.Wavefront
{
    public class Geometry
    {
        public List<Vector3> VertCoords { get; set; } = new List<Vector3>();
        public List<Vector3> Normals { get; set; } = new List<Vector3>();
        public List<Vector2> TexCoords { get; set; } = new List<Vector2>();

        private Dictionary<int, string> _vertCoordsDamaged = new Dictionary<int, string>();
        private Dictionary<int, string> _normalsDamaged = new Dictionary<int, string>();
        private Dictionary<int, string> _texCoordsDamaged = new Dictionary<int, string>();

        public void AddVertCoords(IEnumerable<string> vertices)
        {
            var tmpVertex = vertices.ToArray();

            try
            {
                if (tmpVertex.Length != 3)
                {
                    throw new ArgumentException();
                }
                var vert3 = VertexParser.Parse(tmpVertex[0], tmpVertex[1], tmpVertex[2]);
                VertCoords.Add(vert3);
            }
            catch
            {
                _vertCoordsDamaged.Add(VertCoords.Count, string.Join(" ", vertices));
                VertCoords.Add(Vector3.Zero);
            }
            
        }

        public void AddNormals(IEnumerable<string> vertices)
        {
            var tmpVertex = vertices.ToArray();

            try
            {
                if (tmpVertex.Length != 3)
                {
                    throw new ArgumentException();
                }
                var vert3 = VertexParser.Parse(tmpVertex[0], tmpVertex[1], tmpVertex[2]);
                Normals.Add(vert3);
            }
            catch
            {
                _normalsDamaged.Add(Normals.Count, string.Join(" ", vertices));
                //Console.WriteLine(String.Join(" ", vertexes));
                Normals.Add(Vector3.Zero);
            }
        }

        public void AddTextCoords(IEnumerable<string> vertices)
        {
            var tmpVertex = vertices.ToArray();

            try
            {
                if (tmpVertex.Length != 2 && tmpVertex.Length != 3)
                {
                    throw new ArgumentException();
                }
                var vert2 = VertexParser.Parse(tmpVertex[0], tmpVertex[1]);
                TexCoords.Add(vert2);
            }
            catch
            {
                _texCoordsDamaged.Add(TexCoords.Count, string.Join(" ", vertices));
                //Console.WriteLine(String.Join(" ", vertexes));
                TexCoords.Add(Vector2.Zero);
            }
        }        

        public List<Vector3> GetVertices(Face face)
        {
            if (face.Contains(ElementType.Vertex))
            {
                var tmpList = new List<Vector3>();

                var indexes = face.GetElements(ElementType.Vertex);

                for (var i = 0; i < face.MeshSize; i++)
                {
                    tmpList.Add(VertCoords[indexes[i] - 1]);
                }
                return tmpList;
            }
            else
            {
                return null;
            }
        }

        public List<Vector3> GetVertices(IEnumerable<Face> faces)
        {
            var tmpList = new List<Vector3>();

            foreach (var face in faces)
            {
                tmpList.AddRange(GetVertices(face));
            }
            return tmpList;
        }
        
        public List<Vector3> GetNormals(Face face)
        {
            if (face.Contains(ElementType.Normal))
            {
                var tmpList = new List<Vector3>();

                var indexes = face.GetElements(ElementType.Normal);

                for (var i = 0; i < face.MeshSize; i++)
                {
                    tmpList.Add(Normals[indexes[i] - 1]);
                }
                return tmpList;
            }
            else
            {
                return null;
            }
        }

        public List<Vector3> GetNormals(IEnumerable<Face> faces)
        {
            var tmpList = new List<Vector3>();

            foreach (var face in faces)
            {
                tmpList.AddRange(GetNormals(face));
            }
            return tmpList;
        }

        public List<Vector2> GetTextCoords(Face face)
        {
            if (face.Contains(ElementType.TextureCoord))
            {
                var tmpList = new List<Vector2>();

                var indexes = face.GetElements(ElementType.TextureCoord);

                for (var i = 0; i < face.MeshSize; i++)
                {
                    tmpList.Add(TexCoords[indexes[i]-1]);
                }
                return tmpList;
            }
            else
            {
                return null;
            }
        }

        public List<Vector2> GetTextCoords(IEnumerable<Face> faces)
        {
            var tmpList = new List<Vector2>();

            foreach (var face in faces)
            {
                tmpList.AddRange(GetTextCoords(face));
            }

            return tmpList;
        }
    }
}