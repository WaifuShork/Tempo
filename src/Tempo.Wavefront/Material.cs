using OpenTK.Mathematics;

namespace Tempo.Wavefront
{
    public class Material
    {
        public Material() {}

        public Material(Vector3 ambient, Vector3 diffuse, Vector3 specular, Vector3 transFilter, float specularExp = 1.0f, float dissolve = 1.0f, int illum = 0)
        {
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            TransmissionFilter = transFilter;
            SpecularExponent = specularExp;
            Dissolve = dissolve;
            IllumModel = illum;
        }
        
        public string Name { get; set; }
        public Vector3 Ambient { get; set; }
        public Vector3 Diffuse { get; set; }
        public Vector3 Specular { get; set; }
        public float SpecularExponent { get; set; }
        public float Dissolve { get; set; }
        public float IllumModel { get; set; }
        public Vector3 TransmissionFilter { get; set; }
        
        public bool HasTextures { get; set; }
        
        public string AmbientMap { get; set; }
        public string DiffuseMap { get; set; }
        public string SpecularMap { get; set; }
        public string DissolveMap { get; set; }
        public string SpecularExponentMap { get; set; }
    }
}