using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HedgeLib.Sets;

namespace GLvl_Converter
{
    class s06toGLVL
    {
        static public void ConvertSET(string filepath, string templatesFolder, string output)
        {
            XmlWriter xmlWriter = XmlWriter.Create(output);
            List<string> paramNames = new List<string> { };
            List<string> positionParams = new List<string> { "0", "0", "0" };
            List<string> rotationParams = new List<string> { "0", "0", "0", "1" };
            int objectID = 0;
            S06SetData setSource = new S06SetData();
            setSource.Load(filepath);
            List<string> objectWarn = new List<string> { "aqa_pond", "cameraeventbox", "cameraeventcylinder", "cameraeventsphere", "ambience_collision", "amigo_collision", "changelight", "common_hint_collision", "common_object_event", "common_stopplayercollision", "common_terrain", "common_water_collision", "common_windcollision_box", "eventbox", "eventsphere", "impulsesphere", "snowboardjump" };

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("SetObject");

            foreach (SetObject s06Object in setSource.Objects)
            {
                paramNames.Clear();
                var file = Directory.GetFiles(templatesFolder, s06Object.ObjectType + ".xml", SearchOption.AllDirectories)
                .FirstOrDefault();
                if (file != null)
                {
                    Console.WriteLine(s06Object.ObjectType);
                    string[] template = File.ReadAllLines(file);
                    foreach (string line in template)
                    {
                        if (line.Contains("type") && !line.Contains("Extra"))
                        {
                            var test = line.IndexOf("<");
                            var test2 = line.Substring(test + 1);
                            var test3 = test2.IndexOf(" ");
                            test2 = test2.Remove(test3);
                            paramNames.Add(test2);
                        }
                    }

                    xmlWriter.WriteStartElement(s06Object.ObjectType);

                    if (objectWarn.Contains(s06Object.ObjectType))
                    {
                        Console.WriteLine("Need to write some code to handle these objects requiring Maths.");
                    }
                    for (int i = 0; i < s06Object.Parameters.Count; i++)
                    {
                        xmlWriter.WriteStartElement(paramNames[i]);
                        if (s06Object.Parameters[i].DataType.ToString() == "System.Numerics.Vector3")
                        {
                            var temp = s06Object.Parameters[i].Data;
                            string dumbShit = temp.ToString();
                            string[] words = dumbShit.Split(' ');
                            words[0] = words[0].Remove(words[0].Length - 1);
                            words[0] = words[0].Substring(1);
                            words[1] = words[1].Remove(words[1].Length - 1);
                            words[2] = words[2].Remove(words[2].Length - 1);

                            float vecX = float.Parse(words[0]) / 100;
                            float vecY = float.Parse(words[1]) / 100;
                            float vecZ = float.Parse(words[2]) / 100;

                            xmlWriter.WriteStartElement("x");
                            xmlWriter.WriteString(vecX.ToString());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteStartElement("y");
                            xmlWriter.WriteString(vecY.ToString());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteStartElement("z");
                            xmlWriter.WriteString(vecZ.ToString());
                            xmlWriter.WriteEndElement();
                        }
                        else
                        {
                            xmlWriter.WriteString(s06Object.Parameters[i].Data.ToString());
                        }
                        xmlWriter.WriteEndElement();
                    }

                    positionParams[0] = (s06Object.Transform.Position.X / 100).ToString();
                    positionParams[1] = (s06Object.Transform.Position.Y / 100).ToString();
                    positionParams[2] = (s06Object.Transform.Position.Z / 100).ToString();

                    rotationParams[0] = s06Object.Transform.Rotation.X.ToString();
                    rotationParams[1] = s06Object.Transform.Rotation.Y.ToString();
                    rotationParams[2] = s06Object.Transform.Rotation.Z.ToString();
                    rotationParams[3] = s06Object.Transform.Rotation.W.ToString();
                    coordinates(xmlWriter, positionParams, rotationParams);

                    SetObjectID(xmlWriter, objectID);

                    xmlWriter.WriteEndElement();
                }
            }
            objectID++;

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        static public void SetObjectID(XmlWriter xmlWriter, int objectID)
        {
            xmlWriter.WriteStartElement("SetObjectID");
            xmlWriter.WriteString(objectID.ToString());
            xmlWriter.WriteEndElement();
        }

        static public void coordinates(XmlWriter xmlWriter, List<String> positionParams, List<String> objectRotationQuat)
        {
            xmlWriter.WriteStartElement("Position");
            xmlWriter.WriteStartElement("x");
            xmlWriter.WriteString(positionParams[0]);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("y");
            xmlWriter.WriteString(positionParams[1]);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("z");
            xmlWriter.WriteString(positionParams[2]);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Rotation");
            xmlWriter.WriteStartElement("x");
            xmlWriter.WriteString(objectRotationQuat[0]);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("y");
            xmlWriter.WriteString(objectRotationQuat[1]);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("z");
            xmlWriter.WriteString(objectRotationQuat[2]);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("w");
            xmlWriter.WriteString(objectRotationQuat[3]);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
        }
    }
}
