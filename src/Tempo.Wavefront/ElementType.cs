using System;

namespace Tempo.Wavefront
{
    [Flags]
    public enum ElementType
    {
        Vertex = 0x01,
        TextureCoord = 0x02,
        Normal = 0x04
    }
}