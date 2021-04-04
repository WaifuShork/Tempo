namespace Tempo.Wavefront.Parser
{
    public interface IVertexCollection
    {
        int[] GetElements(ElementType faceType);

        bool Contains(ElementType faceType);
        
        ElementType Type { get; }
    }
}