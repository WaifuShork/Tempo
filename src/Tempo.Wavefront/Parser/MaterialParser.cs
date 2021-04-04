using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using OpenTK.Mathematics;

namespace Tempo.Wavefront.Parser
{
    public class MaterialParser
    {
        private Dictionary<string, Material> _materials = new();

        private static NumberFormatInfo _numbFormatInfo = new();        

        static MaterialParser()
        {
            _numbFormatInfo.NumberDecimalSeparator = ".";
        }

        private MaterialParser()
        {
        }

        public static Dictionary<string, Material> LoadMaterials(string fileName)
        {
            var parser = new MaterialParser();
            Material tmpMaterial = null;

            try
            {
                foreach (var line in File.ReadLines(fileName))
                {
                    var readLineItems = line.Trim().Split(' ');

                    switch (readLineItems[0])
                    {
                        case "newmtl":
                            tmpMaterial = new Material();
                            tmpMaterial.Name = string.Join(" ", readLineItems.Skip(1));
                            parser._materials.Add(tmpMaterial.Name, tmpMaterial);
                            break;
                        case "Ns":
                            tmpMaterial!.SpecularExponent = (float)Convert.ToDouble(readLineItems[1], _numbFormatInfo);
                            break;
                        case "d":
                            tmpMaterial!.Dissolve = (float)Convert.ToDouble(readLineItems[1], _numbFormatInfo);
                            break;
                        case "Tf":
                            tmpMaterial!.TransmissionFilter = GetVec3FromTabStr(readLineItems.Skip(1).ToArray(), _numbFormatInfo);
                            break;
                        case "illum":
                            tmpMaterial!.IllumModel = Convert.ToInt32(readLineItems[1]);
                            break;
                        case "Ka":
                            tmpMaterial!.Ambient = GetVec3FromTabStr(readLineItems.Skip(1).ToArray(), _numbFormatInfo);
                            break;
                        case "Kd":
                            tmpMaterial!.Diffuse = GetVec3FromTabStr(readLineItems.Skip(1).ToArray(), _numbFormatInfo);
                            break;
                        case "Ks":
                            tmpMaterial!.Specular = GetVec3FromTabStr(readLineItems.Skip(1).ToArray(), _numbFormatInfo);
                            break;
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException("MTL file not exist", e);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("MTL file is damaged", e);
            }

            return parser._materials;
        }

        private static Vector3 GetVec3FromTabStr(string[] vecStr, IFormatProvider provider)
        {
            if (vecStr.Length != 3)
            {
                throw new ArgumentException("Invalid data in MTL file");
            }

            var tmpVector3 = new Vector3();

            tmpVector3.X = float.Parse(vecStr[0], provider);
            tmpVector3.Y = float.Parse(vecStr[1], provider);
            tmpVector3.Z = float.Parse(vecStr[2], provider);

            return tmpVector3;
        }
    }
}