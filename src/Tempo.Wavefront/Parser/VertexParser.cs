using System.Globalization;
using OpenTK.Mathematics;

namespace Tempo.Wavefront.Parser
{
    public class VertexParser
    {
        private static NumberFormatInfo _numberFormatInfo = new NumberFormatInfo();

        static VertexParser()
        {
            _numberFormatInfo.NumberDecimalSeparator = ".";
        }

        internal static Vector2 Parse(string x, string y)
        {
            var vertex = new Vector2();

            vertex.X = float.Parse(x, _numberFormatInfo);
            vertex.Y = float.Parse(y, _numberFormatInfo);
            return vertex;
        }

        internal static Vector3 Parse(string x, string y, string z)
        {
            var vertex = new Vector3(Parse(x, y));
            vertex.Z = float.Parse(z, _numberFormatInfo);
            return vertex;
        }
    }
}