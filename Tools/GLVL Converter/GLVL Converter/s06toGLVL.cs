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
        public static List<string> listOfIDs = new List<string>() { };
        /*
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
    */

        static public void ConvertSET(string filepath, string output)
        {
            S06SetData setSource = new S06SetData();
            GensSetData setTarget = new GensSetData();
            uint objectID = 0;

            setSource.Load(filepath);
            foreach (SetObject s06Object in setSource.Objects)
            {
                listOfIDs.Add(s06Object.ObjectType + " - ID: " + objectID);
                SetObject gensObject = new SetObject();

                switch (s06Object.ObjectType)
                {
                    case "spring":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "enemy":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "objectphysics":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[1].Data));
                        break;
                    case "itemboxg":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "widespring":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "wvo_revolvingnet":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "wvo_doorB":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "common_guillotine":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        break;
                    case "wvo_waterslider":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "wvo_orca":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[5].Data));
                        break;
                    case "eventbox":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString()) / 50));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[3].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "cameraeventbox":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString()) / 50));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[3].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[5].Data.ToString())));
                        var cameraPosString_cev = s06Object.Parameters[6].Data.ToString();
                        string[] cameraPosArray_cev = cameraPosString_cev.Split(' ');
                        cameraPosArray_cev[0] = cameraPosArray_cev[0].Remove(0, 1);
                        cameraPosArray_cev[0] = cameraPosArray_cev[0].Remove(cameraPosArray_cev[0].Length - 1, 1);
                        cameraPosArray_cev[1] = cameraPosArray_cev[1].Remove(cameraPosArray_cev[1].Length - 1, 1);
                        cameraPosArray_cev[2] = cameraPosArray_cev[2].Remove(cameraPosArray_cev[2].Length - 1, 1);
                        float cameraPosX_cev = float.Parse(cameraPosArray_cev[0]) / 100;
                        float cameraPosY_cev = float.Parse(cameraPosArray_cev[1]) / 100;
                        float cameraPosZ_cev = float.Parse(cameraPosArray_cev[2]) / 100;
                        Vector3 s06CameraPos_cev = new Vector3(cameraPosX_cev, cameraPosY_cev, cameraPosZ_cev);
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06CameraPos_cev)); //Camera Position

                        var cameraTargetString_cev = s06Object.Parameters[7].Data.ToString();
                        string[] cameraTargetArray_cev = cameraTargetString_cev.Split(' ');
                        cameraTargetArray_cev[0] = cameraTargetArray_cev[0].Remove(0, 1);
                        cameraTargetArray_cev[0] = cameraTargetArray_cev[0].Remove(cameraTargetArray_cev[0].Length - 1, 1);
                        cameraTargetArray_cev[1] = cameraTargetArray_cev[1].Remove(cameraTargetArray_cev[1].Length - 1, 1);
                        cameraTargetArray_cev[2] = cameraTargetArray_cev[2].Remove(cameraTargetArray_cev[2].Length - 1, 1);
                        float cameraTargetX_cev = float.Parse(cameraTargetArray_cev[0]) / 100;
                        float cameraTargetY_cev = float.Parse(cameraTargetArray_cev[1]) / 100;
                        float cameraTargetZ_cev = float.Parse(cameraTargetArray_cev[2]) / 100;
                        Vector3 s06CameraTarget_cev = new Vector3(cameraTargetX_cev, cameraTargetY_cev, cameraTargetZ_cev);
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06CameraTarget_cev)); //Camera Target
                        break;
                    case "itemboxa":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "dashpanel":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "common_water_collision":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString()) / 50));
                        break;
                    case "enemyextra":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[4].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[5].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[6].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[7].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[8].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[9].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[10].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[11].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[12].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[13].Data.ToString())));
                        break;
                    case "ring":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[2].Data));
                        break;
                    case "player_start2":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[2].Data));
                        break;
                    case "pointsample":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "jumppanel":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "common_dashring":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "common_stopplayercollision":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString()) / 50));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "chainjump":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "wvo_jumpsplinter":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "common_hint_collision":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString()) / 50));
                        break;
                    case "wvo_battleship":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "player_goal":
                        var cameraPosString_pg = s06Object.Parameters[0].Data.ToString();
                        string[] cameraPosArray_pg = cameraPosString_pg.Split(' ');
                        cameraPosArray_pg[0] = cameraPosArray_pg[0].Remove(0, 1);
                        cameraPosArray_pg[0] = cameraPosArray_pg[0].Remove(cameraPosArray_pg[0].Length - 1, 1);
                        cameraPosArray_pg[1] = cameraPosArray_pg[1].Remove(cameraPosArray_pg[1].Length - 1, 1);
                        cameraPosArray_pg[2] = cameraPosArray_pg[2].Remove(cameraPosArray_pg[2].Length - 1, 1);
                        float cameraPosX_pg = float.Parse(cameraPosArray_pg[0]) / 100;
                        float cameraPosY_pg = float.Parse(cameraPosArray_pg[1]) / 100;
                        float cameraPosZ_pg = float.Parse(cameraPosArray_pg[2]) / 100;
                        Vector3 s06CameraPos_pg = new Vector3(cameraPosX_pg, cameraPosY_pg, cameraPosZ_pg);
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06CameraPos_pg)); //Camera Position

                        var cameraTargetString_pg = s06Object.Parameters[1].Data.ToString();
                        string[] cameraTargetArray_pg = cameraTargetString_pg.Split(' ');
                        cameraTargetArray_pg[0] = cameraTargetArray_pg[0].Remove(0, 1);
                        cameraTargetArray_pg[0] = cameraTargetArray_pg[0].Remove(cameraTargetArray_pg[0].Length - 1, 1);
                        cameraTargetArray_pg[1] = cameraTargetArray_pg[1].Remove(cameraTargetArray_pg[1].Length - 1, 1);
                        cameraTargetArray_pg[2] = cameraTargetArray_pg[2].Remove(cameraTargetArray_pg[2].Length - 1, 1);
                        float cameraTargetX_pg = float.Parse(cameraTargetArray_pg[0]) / 100;
                        float cameraTargetY_pg = float.Parse(cameraTargetArray_pg[1]) / 100;
                        float cameraTargetZ_pg = float.Parse(cameraTargetArray_pg[2]) / 100;
                        Vector3 s06CameraTarget_pg = new Vector3(cameraTargetX_pg, cameraTargetY_pg, cameraTargetZ_pg);
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06CameraTarget_pg)); //Camera Target
                        break;
                    case "townsgoal":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString()) / 50));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "dtd_pillar":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[3].Data));
                        break;
                    case "common_cage":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "common_thorn":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "dtd_movingfloor":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "physicspath":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[8].Data.ToString())));
                        break;
                    case "common_laser":
                        for (int i = 0; i < s06Object.Parameters.Count; i++)
                        {
                            Console.WriteLine(s06Object.Parameters[i].Data);
                        }

                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[8].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[9].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[10].Data));
                        break;
                    case "wap_pathsnowball":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "wap_brokensnowball":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "wap_door":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "snowboardjump":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        break;
                    case "wap_searchlight":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[8].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[9].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[10].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[11].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[12].Data));
                        break;
                    case "common_switch":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "amigo_collision":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString()) / 100));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString()) / 50));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[4].Data));
                        break;
                    case "objectphysics_item":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "bldgexplode":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "cscglassbuildbomb":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "cscglass":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "impulsesphere":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "common_rainbowring":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "pole":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        break;
                    case "positionSample":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "inclinedstonebridge":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "flc_door":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "crater":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "flc_volcanicbomb":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[8].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[9].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[10].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[11].Data.ToString())));
                        break;
                    case "flamesingle":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[8].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[9].Data.ToString())));
                        break;
                    case "flamesequence":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        break;
                    case "common_warphole":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[2].Data));
                        break;
                    case "normal_train":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[8].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[9].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[10].Data.ToString())));
                        break;
                    case "rct_door":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "rct_seesaw":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        break;
                    case "rct_belt":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "tarzan":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[4].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[8].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[9].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[10].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[11].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[12].Data));
                        break;
                    case "bungee":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "hangingrock":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[0].Data));
                        break;
                    case "turtle":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[4].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[5].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[6].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[7].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[8].Data));
                        break;
                    case "fruit":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "lotus":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[6].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        break;
                    case "brickwall":
                        break;
                    case "eagle":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[2].Data));
                        break;
                    case "rope":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        break;
                    case "windswitch":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "windroad":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[3].Data));
                        break;
                    case "inclinedbridge":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "cameraeventcylinder":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[5].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[6].Data));
                        break;
                    case "common_windcollision_box":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[6].Data));
                        break;
                    case "brokenstairs_right":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "kdv_door":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "kdv_rainbow":
                        break;
                    case "robustdoor":
                        break;
                    case "common_psimarksphere":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        break;
                    case "espstairs_left":
                        break;
                    case "gate":
                        break;
                    case "aqa_door":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "updownreel":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "aqa_mercury_small":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "aqa_lamp":
                        break;
                    case "aqa_balancer":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "aqa_magnet":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "aqa_launcher":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[6].Data.ToString())));
                        break;
                    case "aqa_pond":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[8].Data.ToString())));
                        break;
                    case "aqa_glass_blue":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "vehicle":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[3].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[4].Data));
                        break;
                    case "common_key":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        break;
                    case "common_terrain":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "player_npc":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "switch_collector":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[8].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[9].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[10].Data));
                        break;
                    case "eventcylinder":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "dtd_pillar_eagle":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[2].Data));
                        break;
                    case "dtd_door":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "common_hint":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        break;
                    case "dtd_sandwave":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "ironspring":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[5].Data.ToString())));
                        break;
                    case "espswing":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "dtd_billiardswitch":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[3].Data));
                        break;
                    case "dtd_billiard":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "dtd_billiardcounter":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[8].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[9].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[10].Data.ToString())));
                        break;
                    case "dtd_switchcounter":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[8].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[9].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[10].Data));
                        break;
                    case "common_jumpboard":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "rct_seesaw_silver":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[8].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[9].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[10].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[11].Data));
                        break;
                    case "espstairs_right":
                        break;
                    case "pendulum":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "flc_flamecore":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[4].Data));
                        break;
                    case "freezedmantle":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "lockonsphere":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "freight_train":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[7].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[8].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[9].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[10].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[11].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[12].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[13].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[14].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[15].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[16].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[17].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[18].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[19].Data));
                        break;
                    case "particle":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[4].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[5].Data));
                        break;
                    case "aqa_wyvern_fall":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "cameraeventsphere":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[4].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[5].Data));
                        break;
                    case "ambience_collision":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[4].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[5].Data));
                        break;
                    case "changelight":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[3].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[4].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[5].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[8].Data.ToString())));
                        break;
                    case "lightanimation":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[3].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[4].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[5].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[6].Data.ToString())));
                        break;
                    case "common_path_obj":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "pointlight":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        break;
                    case "medal_of_royal_silver":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "scrollbldg":
                        break;
                    case "csctornadochase":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[4].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[5].Data));
                        break;
                    case "csctornado":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[4].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[5].Data));
                        break;
                    case "common_chaosemerald":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "brokenstairs_left":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "kdv_decalog":
                        break;
                    case "eventsphere":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "trial_post":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "bell":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        break;
                    case "disk":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        break;
                    case "present":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        break;
                    case "download_obj":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        break;
                    case "townsman":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[4].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[5].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[6].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[7].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[8].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[9].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[10].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[11].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[12].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[13].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[14].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[15].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[16].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[17].Data.ToString())));
                        break;
                    case "gondola":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        break;
                    case "aqa_glass_red":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "common_ringswitch":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        break;
                    case "common_lensflare":
                        break;
                    case "dtd_sandeffect":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "end_soleannaswitch":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "end_outputwarp":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[3].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[4].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[5].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[6].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[7].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[8].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[9].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[10].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[11].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[12].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[13].Data.ToString())));
                        break;
                    case "end_inputwarp":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "end_timekeeper":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "common_object_event":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[3].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[5].Data.ToString())));
                        break;
                    case "eggman_train":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[6].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[7].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[8].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[9].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[10].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[11].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[12].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[13].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[14].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[15].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[16].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[17].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[18].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[19].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[20].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[21].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[22].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[23].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[24].Data.ToString())));
                        break;
                    case "player_start":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "glidewire":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[0].Data));
                        break;
                    case "twn_hardrock":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        break;
                    case "darkness":
                        break;
                    case "candlestick":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        break;
                    case "passring":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "warpgate":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "kingdomcrest":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "memory_of_past":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "shopTV":
                        break;
                    case "tamaire_collision_box":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        break;
                    case "twn_door":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[5].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[6].Data));
                        break;
                    case "venthole":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(Vector3), s06Object.Parameters[1].Data));
                        break;
                    case "medal_of_royal_bronze":
                        break;
                    case "wvo_doorA":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        break;
                    case "tpj_runninground":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "particlesphere":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[0].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[3].Data));
                        break;
                    case "objecttownscar":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[3].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[4].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[5].Data.ToString())));
                        break;
                    case "radarmapmark":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[2].Data));
                        break;
                    case "twn_gflag_stopplayercollision":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "twn_flagdoor":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "itembox_next":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[1].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(bool), s06Object.Parameters[2].Data));
                        break;
                    case "spring_twn":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(s06Object.Parameters[2].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                    case "tamaire_collision_sphere":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        break;
                    case "tamaire_collision_cylinder":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        break;
                    case "score_collision_cylinder":
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[0].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(s06Object.Parameters[1].Data.ToString())));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(string), s06Object.Parameters[2].Data));
                        gensObject.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(s06Object.Parameters[3].Data.ToString())));
                        break;
                }
                List<SetObjectParam> parameters = gensObject.Parameters;
                var trans = GenTransform(s06Object.Transform);
                SetObject item = new SetObject
                {
                    ObjectType = s06Object.ObjectType,
                    ObjectID = objectID,
                    Parameters = parameters,
                    Transform = trans,
                };
                setTarget.Objects.Add(item);
                objectID++;
            }
            setTarget.Save(output, true);
            //G:\test.set
        }


        public static SetObjectTransform GenTransform(SetObjectTransform trans)
        {
            return new SetObjectTransform
            {
                Position = trans.Position / 100,
                //Normalize Quaternions.
                Rotation = Quaternion.Normalize(trans.Rotation),
                Scale = trans.Scale
            };
        }
    }
}
