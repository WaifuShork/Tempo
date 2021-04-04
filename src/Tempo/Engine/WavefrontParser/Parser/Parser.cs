using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tempo.Wavefront.Parser
{
    public class Parser : IParser
    {
        public Model LoadModel(string fileName)
        {
            var materials = new Dictionary<string, Material>();
            var parts = new Dictionary<string, Part>();
            
            Part tmpPart = null;
            var newPart = true;
            
            Group tmpGroup = null;
            var geometry = new Geometry();

            try
            {
                foreach (var line in File.ReadLines(fileName))
                {
                    if (line != string.Empty)
                    {
                        var tmp = line.Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (tmp.Length > 0)
                        {
                            switch (tmp[0])
                            {
                                ///////////////////////////////////////////
                                case "mtllib":
                                    var directory = Path.GetDirectoryName(fileName);
                                    materials = LoadMaterials(directory, tmp);
                                    break;
                                ///////////////////////////////////////////
                                case "v":
                                    if (newPart)
                                    {
                                        tmpPart = new Part();
                                        parts.Add(tmpPart.Name, tmpPart);                                        
                                        newPart = false;
                                    }

                                    geometry.AddVertCoords(tmp.Skip(1));
                                    break;
                                //////////////////////////////////////////////
                                case "vn":
                                    geometry.AddNormals(tmp.Skip(1));
                                    break;
                                ///////////////////////////////////////////////
                                case "vt":
                                    geometry.AddTextCoords(tmp.Skip(1));
                                    break;
                                ////////////////////////////////////////////////
                                case "o":
                                case "g":
                                    if (newPart)
                                    {
                                        tmpPart = new Part();
                                        parts.Add(tmpPart.Name, tmpPart);                                        
                                        newPart = false;
                                    }
                                    else
                                    {
                                        parts.Remove(tmpPart.Name);
                                        tmpPart.Name = string.Join(" ", tmp.Skip(1));
                                        parts.Add(tmpPart.Name, tmpPart);
                                    }
                                    break;
                                /////////////////////////////////////////////////
                                case "usemtl":                                    
                                    var materialName = string.Join(" ", tmp.Skip(1));
                                    if (materials.ContainsKey(materialName))
                                    {
                                        tmpGroup = new Group { Material = materials[materialName] };
                                        parts[tmpPart!.Name].Groups.Add(tmpGroup);
                                    }
                                    else
                                    {
                                        throw new ArgumentException($"Material: \"{ materialName }\" isnt exist");
                                    }
                                    break;
                                /////////////////////////////////////////////////
                                case "f":
                                    if (!newPart)
                                    {
                                        newPart = true;
                                    }
                                    Face face = Face.Parse(tmp.Skip(1));
                                    tmpGroup!.Faces.Add(face);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException f)
            {
                throw new FileNotFoundException($"OBJ file not found: {f.FileName}",f);
            }
            catch(ArgumentException ae)
            {
                throw new ArgumentException("OBJ file is damaged", ae);
            }

            var model = new Model();
            model.Materials = materials;
            model.Geometry = geometry;
            model.Parts = parts;

            return model;
        }

        private static Dictionary<string, Material> LoadMaterials(string directory, string[] tmp)
        {
            Dictionary<string, Material> materials;

            var matFileName = string.Join(" ", tmp.Skip(1));
            var path = Path.Combine(directory, matFileName);
            materials = MaterialParser.LoadMaterials(path);
            return materials;
        }
    }
}