using System;
using HedgeLib;
using HedgeLib.Sets;
using System.Xml.Linq;

namespace Sonic_06_GLvl_Converter
{
    class Conversion
    {
        /// <summary>
        /// Converts the SET to the respective format.
        /// </summary>
        /// <param name="sourceSETPath">Location of the source SET.</param>
        /// <param name="groupXMLPath">Location of the group XML data.</param>
        /// <param name="targetSETPath">Final location of the created SET data.</param>
        /// <param name="mode">[true: GLvl] [false: '06]</param>
        /// <returns>Converted SET data.</returns>
        public static void ConvertSET(string sourceSETPath, string groupXMLPath, string targetSETPath, bool mode)
        {
            uint objectID = 0;
            S06SetData _S06SetData = new S06SetData();
            GensSetData _GensSetData = new GensSetData();

            //Objects
            if (mode) {
                _S06SetData.Load(sourceSETPath);

                foreach (SetObject sourceObj in _S06SetData.Objects) {
                    SetObject targetObj = ObjectConversion(sourceObj, objectID, mode);
                    targetObj.DrawDistance = sourceObj.DrawDistance;
                    targetObj.UnknownBytes = sourceObj.UnknownBytes;
                    if (sourceObj.ObjectName != "") targetObj.ObjectName = sourceObj.ObjectName;
                    _GensSetData.Objects.Add(targetObj);
                }

                for (int i = 0; i < _GensSetData.Objects.Count; i++) {
                    _GensSetData.Objects[i].Parameters.Add(new SetObjectParam(typeof(float), _S06SetData.Objects[i].DrawDistance));
                    _GensSetData.Objects[i].Parameters.Add(new SetObjectParam(typeof(string), _S06SetData.Objects[i].ObjectName));
                    if (_S06SetData.Objects[i].UnknownBytes[3] == 1)
                        _GensSetData.Objects[i].Parameters.Add(new SetObjectParam(typeof(bool), true));
                    else
                        _GensSetData.Objects[i].Parameters.Add(new SetObjectParam(typeof(bool), false));

                    Main.listOfIDs.Add($"[{DateTime.Now:hh:mm:ss tt}] Object: {_GensSetData.Objects[i].ObjectType} | Name: {_GensSetData.Objects[i].ObjectName} | ID: {objectID}");
                    objectID++;
                }

                _GensSetData.Save(targetSETPath, true);
            } else {
                _GensSetData.Load(sourceSETPath);
                _S06SetData.Name = "test";

                foreach (SetObject sourceObj in _GensSetData.Objects) {
                    SetObject targetObj = ObjectConversion(sourceObj, objectID, mode);
                    targetObj.DrawDistance = sourceObj.DrawDistance;
                    targetObj.UnknownBytes = sourceObj.UnknownBytes;
                    if (sourceObj.ObjectName != "") targetObj.ObjectName = sourceObj.ObjectName;
                    _S06SetData.Objects.Add(targetObj);
                }

                for (int i = 0; i < _S06SetData.Objects.Count; i++) {
                    int numOfParams = _GensSetData.Objects[i].Parameters.Count;
                    _S06SetData.Objects[i].DrawDistance = float.Parse(_GensSetData.Objects[i].Parameters[numOfParams - 3].Data.ToString());
                    _S06SetData.Objects[i].ObjectName = _GensSetData.Objects[i].Parameters[numOfParams - 2].Data.ToString();

                    if (_GensSetData.Objects[i].Parameters[numOfParams - 1].Data.ToString() == "False") {
                        byte[] bytesGLVL = new byte[16];
                        bytesGLVL[0] = 64;
                        bytesGLVL[1] = 0;
                        bytesGLVL[2] = 0;
                        bytesGLVL[3] = 0;
                        bytesGLVL[4] = 0;
                        bytesGLVL[5] = 0;
                        bytesGLVL[6] = 0;
                        bytesGLVL[7] = 0;
                        bytesGLVL[8] = 0;
                        bytesGLVL[9] = 0;
                        bytesGLVL[10] = 0;
                        bytesGLVL[11] = 0;
                        bytesGLVL[12] = 0;
                        bytesGLVL[13] = 0;
                        bytesGLVL[14] = 0;
                        bytesGLVL[15] = 0;
                        _S06SetData.Objects[i].UnknownBytes = bytesGLVL;
                    } else if (_GensSetData.Objects[i].Parameters[numOfParams - 1].Data.ToString() == "True") {
                        byte[] bytesGLVL = new byte[16];
                        bytesGLVL[0] = 64;
                        bytesGLVL[1] = 0;
                        bytesGLVL[2] = 0;
                        bytesGLVL[3] = 1;
                        bytesGLVL[4] = 0;
                        bytesGLVL[5] = 0;
                        bytesGLVL[6] = 0;
                        bytesGLVL[7] = 0;
                        bytesGLVL[8] = 0;
                        bytesGLVL[9] = 0;
                        bytesGLVL[10] = 0;
                        bytesGLVL[11] = 0;
                        bytesGLVL[12] = 0;
                        bytesGLVL[13] = 0;
                        bytesGLVL[14] = 0;
                        bytesGLVL[15] = 0;
                        _S06SetData.Objects[i].UnknownBytes = bytesGLVL;
                    }

                    Main.listOfIDs.Add($"[{DateTime.Now:hh:mm:ss tt}] Object: {_S06SetData.Objects[i].ObjectType} | Name: {_S06SetData.Objects[i].ObjectName} | ID: {objectID}");
                    objectID++;
                }

                //Groups
                if (groupXMLPath.Length != 0)
                {
                    XDocument xml = XDocument.Load(groupXMLPath);

                    foreach (XElement groupElem in xml.Root.Elements("Group"))
                    {
                        XElement groupNameElem = groupElem.Element("Name");
                        XElement groupObjectCountElem = groupElem.Element("ObjectCount");
                        XElement groupTypeElem = groupElem.Element("Type");

                        SetGroup group = new SetGroup()
                        {
                            GroupName = groupNameElem.Value,
                            GroupObjectCount = uint.Parse(groupObjectCountElem.Value),
                            GroupType = groupTypeElem.Value
                        };

                        XElement objectsElem = groupElem.Element("ObjectIDs");
                        if (objectsElem != null)
                            foreach (XElement objectIDElem in objectsElem.Elements())
                                group.ObjectIDs.Add(uint.Parse(objectIDElem.Value));

                        _S06SetData.Groups.Add(group);
                    }
                }

                _S06SetData.Save(targetSETPath, true);
            }
        }

        /// <summary>
        /// Converts '06 Groups to an XML to save alongside the GLVL Set
        /// </summary>
        /// <param name="sourceSETPath">Path to the original SET</param>
        /// <param name="groupXMLPath">Path to the file to save the Groups to</param>
        public static void GLVLGroupExport(string sourceSETPath, string groupXMLPath)
        {
            S06SetData sourceSET = new S06SetData();
            sourceSET.Load(sourceSETPath);

            XElement rootElem = new XElement("SetGroup");
            foreach (var group in sourceSET.Groups)
            {
                XElement groupElem = new XElement("Group");
                XElement groupNameElem = new XElement($"Name", group.GroupName);
                XElement groupTypeElem = new XElement($"Type", group.GroupType);
                XElement groupObjectCountElem = new XElement($"ObjectCount", group.GroupObjectCount);
                XElement objectIDsElem = new XElement("ObjectIDs");

                for (int i = 0; i < group.ObjectIDs.Count; i++)
                {
                    XElement objectIDElem = new XElement($"ObjectID{i}", group.ObjectIDs[i]);
                    objectIDsElem.Add(objectIDElem);
                }

                groupElem.Add(groupNameElem, groupTypeElem, groupObjectCountElem, objectIDsElem);
                rootElem.Add(groupElem);
            }

            XDocument xml = new XDocument(rootElem);
            xml.Save(groupXMLPath);
        }


        /// <summary>
        /// Properly scales a Vector3 to the target scale
        /// </summary>
        /// <param name="mode">[true: GLvl] [false: '06]</param>
        /// <param name="origVector">A string representation of the original Vector3</param>
        /// <returns></returns>
        public static Vector3 VectorMaths(bool mode, string origVector)
        {
            string[] origVectorArray = origVector.Split(' ');
            origVectorArray[0] = origVectorArray[0].Remove(0, 1);
            origVectorArray[0] = origVectorArray[0].Remove(origVectorArray[0].Length - 1, 1);
            origVectorArray[1] = origVectorArray[1].Remove(origVectorArray[1].Length - 1, 1);
            origVectorArray[2] = origVectorArray[2].Remove(origVectorArray[2].Length - 1, 1);

            return new Vector3(VolumeConversion(mode, float.Parse(origVectorArray[0]), 100),
                               VolumeConversion(mode, float.Parse(origVectorArray[1]), 100),
                               VolumeConversion(mode, float.Parse(origVectorArray[2]), 100));
        }

        /// <summary>
        /// Converts the volume to the respective format.
        /// </summary>
        /// <param name="mode">[true: GLvl] [false: '06]</param>
        /// <param name="volume">Volume from SET data.</param>
        /// <param name="modifier">Float modifier for conversion.</param>
        /// <returns>Corrected float.</returns>
        public static float VolumeConversion(bool mode, float volume, float modifier)
        {
            switch (mode)
            {
                // Convert to GLvl
                case true: return volume / modifier;
                // Convert to '06
                case false: return volume * modifier;
                // Default
                default: return 0;
            };
        }

        /// <summary>
        /// Converts the default target value.
        /// </summary>
        /// <param name="mode">[true: GLvl] [false: '06]</param>
        /// <param name="sourceObj">Source object parameter.</param>
        /// <param name="paramNumber">Parameter index to change.</param>
        /// <returns>Modified object parameter.</returns>
        public static SetObjectParam TargetConversion(bool mode, SetObject sourceObj, int paramNumber)
        {
            uint uintCheck;
            if (!mode)
            {
                // Convert to '06
                if (uint.TryParse(sourceObj.Parameters[paramNumber].Data.ToString(), out uintCheck)) return new SetObjectParam(typeof(uint), uintCheck);
                else return new SetObjectParam(typeof(uint), 4294967295u);
            }
            else
            {
                // Convert to GLvl
                if (uint.TryParse(sourceObj.Parameters[paramNumber].Data.ToString(), out uintCheck))
                {
                    if (uintCheck != 4294967295u)
                        return new SetObjectParam(typeof(uint), uintCheck);
                    else
                        return new SetObjectParam(typeof(string), "4.29497e+009");
                }
                else
                    return new SetObjectParam(typeof(uint), 4294967295u);
            }
        }
        /// <summary>
        /// Actual conversion of the objects
        /// </summary>
        /// <param name="sourceObj">The object to convert</param>
        /// <param name="objectID">The object ID</param>
        /// <param name="mode">[true: GLvl] [false: '06]</param></param>
        /// <returns></returns>
        public static SetObject ObjectConversion(SetObject sourceObj, uint objectID, bool mode)
        {
            SetObject targetObj = new SetObject
            {
                ObjectType = sourceObj.ObjectType,
                ObjectID = objectID,
                ObjectName = $"{sourceObj.ObjectType}{objectID}",
                DrawDistance = 0,
                UnknownBytes = new byte[] { 64, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00 }
            };

            switch (sourceObj.ObjectType)
            {
                //actor_aquaticbase
                case "aqa_mercury_small":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //hp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //friction
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //air_friction
                    break;
                case "aqa_pond":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //num
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //hp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //friction
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //air_friction
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //vel
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[8].Data.ToString()))); //time
                    break;
                case "aqa_door":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //signal
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //esp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //open
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //close
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //content
                    break;
                case "aqa_magnet":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //kind
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //force
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //offtime
                    break;
                case "aqa_glass_blue":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //speed
                    break;
                case "aqa_glass_red":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //speed
                    break;
                case "aqa_balancer":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //power
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //deceleration
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //psi
                    break;
                case "aqa_wyvern_fall":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //name
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //wait
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //hight
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //speed
                    break;
                case "aqa_launcher":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //num
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //hp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //friction
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //air_friction
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //time
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 6)); //target
                    break;
                case "aqa_wyvern_fall_dust":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //name
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //min_range
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //max_range
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //min_height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //max_height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //min_time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //max_time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[7].Data.ToString()))); //num
                    break;

                //actor_common
                case "ring":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[0].Data)); //shadow
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //pathparam
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //path
                    break;
                case "spring":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //velocity
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //time
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 2)); //target
                    break;
                case "spring_twn":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //velocity
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //time
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 2)); //target
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //globalflag
                    break;
                case "dashpanel":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //velocity
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //time
                    break;
                case "eventbox":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[3].Data)); //onintersect
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //default
                    break;
                case "eventsphere":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //onintersect
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //default
                    break;
                case "eventcylinder":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //onintersect
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //default
                    break;
                case "jumppanel":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //pitch
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //velocity
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //time
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 3)); //target
                    break;
                case "widespring":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //velocity
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //time
                    break;
                case "itemboxg":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //content
                    break;
                case "itemboxa":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //content
                    break;
                case "itembox_next":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //content
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[1].Data)); //flag1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[2].Data)); //flag2
                    break;
                case "pole":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //power
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //pitch
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //yaw
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //rotation_time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //out_time
                    break;
                case "updownreel":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //height2
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //time
                    break;
                case "firstiblis":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //name
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //type
                    break;
                case "particle":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //bank
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //name
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //cycle
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //cyclewidth
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[4].Data)); //sebank
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[5].Data)); //sename
                    break;
                case "particlesphere":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //bank
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //name
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //sebank
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[3].Data)); //sename
                    break;
                case "ambience":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //bank
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //name
                    break;
                case "ambience_collision":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //fade
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[4].Data)); //bank
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[5].Data)); //name
                    break;
                case "objectphysics":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //objectName
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[1].Data)); //restart
                    break;
                case "chainjump":
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 0)); //target
                    break;
                case "enemy":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //typeName
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //typeNumber
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //scriptName
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[3].Data.ToString()), 100))); //radius
                    break;
                case "enemyextra":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //typeName
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //typeNumber
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //scriptName
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[3].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[4].Data)); //restart
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[5].Data)); //findPlayer
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[6].Data)); //isBoss
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[7].Data)); //isFixed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[8].Data)); //appearPath
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[9].Data.ToString()))); //appearVelocity
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[10].Data)); //searchPath
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[11].Data)); //actionPath
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 12)); //target
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 13)); //homingTarget
                    break;
                case "vehicle":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //type
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //downfilename
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[2].Data)); //isGetOut
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[3].Data)); //isShoot
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[4].Data)); //RoutePath
                    break;
                case "positionSample":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //type
                    break;
                case "snowboardjump":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //power
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //pitch
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //rate
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //bpower_rate
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //time
                    break;
                case "physicspath":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //objectName
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[1].Data)); //restart
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //pathName
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //velocity
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //startMode
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[5].Data.ToString()))); //pathMode
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[6].Data.ToString()))); //motionMode
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //motionSpeed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[8].Data.ToString()))); //motionParam
                    break;
                case "lockonsphere":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //lockOn
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //homing
                    break;
                case "firstmefiress":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //vs
                    break;
                case "impulsesphere":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //impulse
                    break;
                case "common_windcollision_box":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //power
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //floatheight
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[5].Data.ToString()))); //content
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[6].Data)); //signal
                    break;
                case "common_windcollision_cylinder":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //power
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //floatheight
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //content
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[5].Data)); //signal
                    break;
                case "common_switch":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //content
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //eventcolli
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //event_off
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //time
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 4)); //target
                    break;
                case "common_hint":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //event
                    break;
                case "common_hint_collision":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //event
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[3].Data.ToString()), 50))); //height
                    break;
                case "common_rainbowring":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //pitch
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //yaw
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //velocity
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //time
                    break;
                case "common_warphole":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //content
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //event
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[2].Data.ToString()))); //target
                    break;
                case "common_key":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //event
                    break;
                case "common_chaosemerald":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //content
                    break;
                case "common_jumpboard":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //pitch
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //velocity
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //speed_max
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //speed_mid
                    break;
                case "common_dashring":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //pitch
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //yaw
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //velocity
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //time
                    break;
                case "common_psimarksphere":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //event
                    break;
                case "common_ringswitch":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //interval
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //path
                    break;
                case "common_cage":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //scale
                    break;
                case "common_laser":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //alone
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 1)); //pair
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //num
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //interval
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //ontime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //offtime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //range
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[8].Data.ToString()))); //radrange
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[9].Data.ToString()))); //radspeed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[10].Data)); //nactivate
                    break;
                case "common_object_event":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[3].Data)); //event
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //content
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[5].Data.ToString()))); //default
                    break;
                case "common_path_obj":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //objectName
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //path
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //interpolation
                    break;
                case "common_thorn":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //content
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //outtime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //intime
                    break;
                case "common_guillotine":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //content
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //upwidth
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //downwidth
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //distance
                    break;
                case "player_npc":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //content
                    break;
                case "player_start":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //player_no
                    break;
                case "player_start2":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //player_no
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //player_name
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[2].Data)); //amigo
                    break;
                case "player_goal":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[0].Data.ToString()))); //cam_pos
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[1].Data.ToString()))); //cam_tgt
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), "sonic_new"));
                    break;
                case "common_stopplayercollision":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //default
                    break;
                case "common_terrain":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //default
                    break;
                case "changelight":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[3].Data)); //mainlight
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[4].Data)); //sublight
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[5].Data)); //ambient
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[6].Data.ToString()))); //default
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //into
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[8].Data.ToString()))); //out
                    break;
                case "lightanimation":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[3].Data)); //mainlight
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[4].Data)); //sublight
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[5].Data)); //ambient
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[6].Data.ToString()))); //default
                    break;
                case "pointlight":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //lightname
                    break;
                case "common_water_collision":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    break;
                case "pointsample":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //content
                    break;
                case "amigo_collision":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 3)); //target
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[4].Data)); //chase
                    break;
                case "radarmapmark":
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 0)); //target
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //mark
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[2].Data)); //animation
                    break;
                case "switch_collector":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //count
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 1)); //target0
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 2)); //target1
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 3)); //target2
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 4)); //target3
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 5)); //target4
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 6)); //target5
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 7)); //target6
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 8)); //target7
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 9)); //target8
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[10].Data)); //event
                    break;

                //actor_crisiscity
                case "carcoli":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    break;
                case "bldgexplode":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //time
                    break;
                case "cscglass":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //time
                    break;
                case "cscglassbuildbomb":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //time
                    break;
                case "cscflybillboard":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //appeartime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //disappeartime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //pathspd
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[3].Data)); //path
                    break;
                case "csctornado":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[0].Data.ToString()))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //posy
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //chasespd
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //homing
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[4].Data.ToString()))); //throwspd
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[5].Data.ToString()))); //topos
                    break;
                case "csctornadochase":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[0].Data.ToString()))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //posy
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //chasespd
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //homing
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[4].Data.ToString()))); //throwspd
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[5].Data.ToString()))); //topos
                    break;
                case "csctrailercolli":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    break;
                case "ironspring":
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 0)); //target
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //pitch
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //yaw
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //power
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //out_time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //on_time
                    break;

                //actor_dustydesert
                case "dtd_sandwave":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //speed
                    break;
                case "dtd_pillar":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //content
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[3].Data)); //noeffect
                    break;
                case "dtd_pillar_eagle":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[2].Data)); //noeffect
                    break;
                case "dtd_door":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //signal
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //esp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //open
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //close
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //content
                    break;
                case "dtd_billiard":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //count
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //power
                    break;
                case "dtd_billiardswitch":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[3].Data)); //event
                    break;
                case "dtd_billiardcounter":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //count
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //time
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 2)); //target0
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 3)); //target1
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 4)); //target2
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 5)); //target3
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 6)); //target4
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 7)); //target5
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 8)); //target6
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 9)); //target7
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 10)); //target8
                    break;
                case "dtd_switchcounter":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //count
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 1)); //target0
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 2)); //target1
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 3)); //target2
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 4)); //target3
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 5)); //target4
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 6)); //target5
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 7)); //target6
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 8)); //target7
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 9)); //target8
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[10].Data)); //event
                    break;
                case "dtd_movingfloor":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //on_time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //off_time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //appear_time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //disappear_time
                    break;
                case "dtd_sandeffect":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    break;

                //actor_endoftheworld
                case "end_soleannaswitch":
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 0)); //target
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //offtime
                    break;
                case "end_outputwarp":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //kind0
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //kind1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //kind2
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[3].Data)); //kind3
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[4].Data)); //kind4
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[5].Data)); //kind5
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[6].Data)); //kind6
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[7].Data)); //kind7
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[8].Data)); //kind8
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[9].Data)); //kind9
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[10].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[11].Data.ToString()))); //time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[12].Data.ToString()))); //error
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[13].Data.ToString()))); //ypos
                    break;
                case "end_inputwarp":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //force
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //radmin
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //radmax
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //appear
                    break;
                case "end_timekeeper":
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 0)); //target
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //starttime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //endtime
                    break;

                //actor_flamecore
                case "flamesingle":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //shinetime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[1].Data)); //espmode
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //light_r
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //light_g
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //light_b
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //light_att_a
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //light_att_b
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //light_att_c
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[8].Data.ToString()))); //light_range
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 9)); //signalobj
                    break;
                case "crater":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //attackpower
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //waittime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //placetype
                    break;
                case "inclinedstonebridge":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //time1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //time2
                    break;
                case "flc_door":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //signal
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //esp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //open
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //close
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //content
                    break;
                case "flamesequence":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //preparetime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //event
                    break;
                case "flc_flamecore":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //attacktime1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //attacktime2
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //attackrange
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //firstmode
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[4].Data)); //event
                    break;
                case "flc_volcanicbomb":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 3)); //volcanoobj
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //cycletime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //cycletimr
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //volcanocycle
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //volcanodelay
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[8].Data.ToString()))); //volcanotime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[9].Data.ToString()))); //bombheight
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[10].Data.ToString()))); //bombrange
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[11].Data.ToString()))); //bombangle
                    break;
                case "freezedmantle":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //motionspd
                    break;

                //actor_kingdomvalley
                case "rope":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[0].Data.ToString()))); //target
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //height1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //height2
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //height3
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //dec
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //time1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //time2
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //time3
                    break;
                case "eagle":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //speed1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //path
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[2].Data)); //noloop
                    break;
                case "eagle_ex":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //speed1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //path
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[2].Data)); //noloop
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //temp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[5].Data)); //flag
                    break;
                case "brokenstairs_right":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //time
                    break;
                case "brokenstairs_left":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //time
                    break;
                case "inclinedbridge":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //time1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //time2
                    break;
                case "windswitch":
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 0)); //target1
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 1)); //target2
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 2)); //target3
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 3)); //target4
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //base
                    break;
                case "windroad":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //appeartime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //disappeartime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[3].Data)); //path
                    break;
                case "pendulum":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //length
                    break;
                case "kdv_door":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //signal
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //esp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //open
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //close
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //content
                    break;

                //actor_radicaltrain
                case "rct_train":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //path
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //interpolation
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //num
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //interval
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[5].Data.ToString()))); //loop
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //start
                    break;
                case "normal_train":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //path
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //interpolation
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //num
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //interval
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[5].Data.ToString()))); //loop
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //start
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //acc
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[8].Data.ToString()))); //dec
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[9].Data.ToString()))); //camera
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[10].Data.ToString()))); //end
                    break;
                case "eggman_train":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //path
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //interpolation
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //num
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //interval
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[5].Data.ToString()))); //loop
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //start
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[7].Data.ToString()))); //type
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[8].Data.ToString()))); //bombtime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[9].Data.ToString()))); //hp1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[10].Data.ToString()))); //hp2
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[11].Data.ToString()))); //brake
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[12].Data)); //event
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[13].Data)); //timeover
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[14].Data.ToString()))); //break
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[15].Data.ToString()))); //waittime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[16].Data.ToString()))); //distance
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[17].Data.ToString()))); //timing
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[18].Data.ToString()))); //vel
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[19].Data.ToString()))); //acc
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[20].Data.ToString()))); //dec
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[21].Data)); //left
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[22].Data.ToString()))); //camera
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[23].Data.ToString()))); //end
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[24].Data.ToString()))); //minspeed
                    break;
                case "freight_train":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //path
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //interpolation
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //num
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //interval
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[5].Data.ToString()))); //loop
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //start
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[7].Data)); //on1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[8].Data)); //on2
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[9].Data)); //on3
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[10].Data)); //on4
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[11].Data)); //on5
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[12].Data)); //on6
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[13].Data)); //on7
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[14].Data)); //on8
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[15].Data)); //on9
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[16].Data)); //on10
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[17].Data.ToString()))); //acc
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[18].Data.ToString()))); //dec
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[19].Data)); //boss
                    break;
                case "rct_belt":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //forward
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //backward
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //time1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //time2
                    break;
                case "rct_seesaw":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //retspeed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //time
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //maxlen
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //difference
                    break;
                case "rct_seesaw_silver":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //retspeed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //maxlen
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //difference
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[5].Data.ToString()))); //right
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //right_x
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //right_z
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[8].Data.ToString()))); //left
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[9].Data.ToString()))); //left_x
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[10].Data.ToString()))); //left_z
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[11].Data)); //lua
                    break;
                case "rct_door":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //signal
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //esp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //open
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //close
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //content
                    break;

                //actor_town
                case "townsman":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //actorname
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //mantype
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //bodycolor
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //haircolor
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[4].Data)); //mankind
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[5].Data)); //pathway
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[6].Data)); //action
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[7].Data)); //talkevent
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[8].Data.ToString()))); //target
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 9)); //target_actor
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[10].Data.ToString()))); //manvariation
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[11].Data.ToString()))); //turnmode
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[12].Data)); //brain
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[13].Data)); //brainparam
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[14].Data)); //norangeout
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[15].Data)); //nododge
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 16)); //target_ride
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[17].Data.ToString()))); //globalflag
                    break;
                case "warpgate":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //stageName
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //appearances
                    break;
                case "passring":
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 0)); //nextring
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //lifetime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //scale
                    break;
                case "medal_of_royal_silver":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //global_flag
                    break;
                case "objectphysics_item":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //objectName
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[1].Data)); //restart
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //numItemObjects
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //itemIndex
                    break;
                case "objecttownscar":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //type
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //pathname
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //brainname
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[3].Data)); //downfilename
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //color
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[5].Data.ToString()))); //hp
                    break;
                case "townsgoal":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //collisionsize
                    break;
                case "disk":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //textID
                    break;
                case "present":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //textID
                    break;
                case "bell":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //eventname
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //group_name
                    break;
                case "candlestick":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //fire_lifetime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //eventname
                    break;
                case "twn_door":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //signal
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //esp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //open
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //close
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //content
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[5].Data.ToString()))); //lifepoint
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[6].Data)); //onbroken
                    break;
                case "trial_post":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //eventname
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //global_flag_no
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //my_color
                    break;
                case "kingdomcrest":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //global_flag1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //global_flag2
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //global_flag3
                    break;
                case "venthole":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //eventname
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[1].Data.ToString()))); //warp_target
                    break;
                case "memory_of_past":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //event
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //textID
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 3)); //targetID
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //lifetime
                    break;
                case "invisible_collision_sphere":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //lifepoint
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //onbroken
                    break;
                case "invisible_collision_box":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //lifepoint
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[4].Data)); //onbroken
                    break;
                case "invisible_collision_cylinder":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //lifepoint
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[3].Data)); //onbroken
                    break;
                case "gondola":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //ky_path
                    break;
                case "glidewire":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[0].Data.ToString()))); //target
                    break;
                case "gliderope":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[0].Data.ToString()))); //target
                    break;
                case "twn_hardrock":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //objectName
                    break;
                case "tamaire_collision_sphere":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    break;
                case "tamaire_collision_box":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    break;
                case "tamaire_collision_cylinder":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 50))); //height
                    break;
                case "score_collision_cylinder":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //target_name
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //score_point
                    break;
                case "download_obj":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //textID
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //download
                    break;
                case "twn_flagdoor":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //global_flag
                    break;
                case "twn_gflag_stopplayercollision":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //global_flag
                    break;

                //actor_tropicaljungle
                case "espswing":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //hitpower
                    break;
                case "hangingrock":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[0].Data)); //nocheckdamage
                    break;
                case "tarzan":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //top
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //bottom
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //jumpspeed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //jumpcheckspeed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[4].Data)); //fixdirection
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //hittutaspeed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //weight
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //hitangle
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[8].Data.ToString()))); //jumpcheckangle
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 9)); //targetobj
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[10].Data.ToString()))); //camz
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[11].Data.ToString()))); //camy
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[12].Data)); //isrouge
                    break;
                case "bungee":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //spd
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[1].Data)); //pathname
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 2)); //targetobj
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //motspd
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //jumpframe
                    break;
                case "lotus":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //spd1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //spd2
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //spd3
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //charge1
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //charge2
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //charge3
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[6].Data)); //espmode
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //nocontime
                    break;
                case "turtle":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //redowntime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //damagetime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //turntime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //speedswim
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[4].Data)); //doreverse
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[5].Data)); //doloop
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[6].Data)); //waitfruit
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[7].Data)); //waitride
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[8].Data)); //pathname
                    break;
                case "fruit":
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 0)); //turtle
                    break;
                case "tpj_runninground":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //path
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //radius
                    break;

                //actor_waveocean
                case "wvo_waterslider":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //path
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //radius
                    break;
                case "wvo_revolvingnet":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //velocity
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //control
                    break;
                case "wvo_doorA":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //signal
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //esp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //open
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //close
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //content
                    break;
                case "wvo_doorB":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //signal
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //esp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //open
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //close
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //content
                    break;
                case "wvo_orca":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //content
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[2].Data)); //path
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //distance
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //coefficient
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[5].Data)); //onintersect
                    break;
                case "wvo_jumpsplinter":
                    targetObj.Parameters.Add(TargetConversion(mode, sourceObj, 0)); //target
                    break;
                case "wvo_battleship":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //path
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //distance
                    break;

                //actor_whiteacropolis
                case "wap_searchlight":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //pitchangle
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //pitchrange
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //pitchspeed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //pitchtime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[4].Data.ToString()))); //yawangle
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[5].Data.ToString()))); //yawrange
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[6].Data.ToString()))); //yawspeed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[7].Data.ToString()))); //yawtime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[8].Data.ToString()))); //findtime
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[9].Data.ToString()))); //loselength
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[10].Data)); //onintersect
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[11].Data)); //brokenhead
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[12].Data)); //allbrk
                    break;
                case "wap_pathsnowball":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(string), sourceObj.Parameters[0].Data)); //path
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //speed
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[2].Data.ToString()))); //distance
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[3].Data.ToString()))); //coefficient
                    break;
                case "wap_brokensnowball":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[0].Data.ToString()))); //power
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(sourceObj.Parameters[1].Data.ToString()))); //rate
                    break;
                case "wap_door":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //signal
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[1].Data.ToString()))); //esp
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //open
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //close
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //content
                    break;
                case "wap_snow":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[0].Data.ToString()))); //number
                    break;

                //camera_class
                case "cameraeventsphere":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[1].Data)); //cancelable
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[2].Data.ToString()))); //prio
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //onintersect
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[4].Data.ToString()))); //data0
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[5].Data.ToString()))); //data1
                    break;
                case "cameraeventbox":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //length
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 100))); //width
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[2].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[3].Data)); //cancelable
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //prio
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[5].Data.ToString()))); //onintersect
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[6].Data.ToString()))); //data0
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[7].Data.ToString()))); //data1
                    break;
                case "cameraeventcylinder":
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[0].Data.ToString()), 100))); //radius
                    targetObj.Parameters.Add(new SetObjectParam(typeof(float), VolumeConversion(mode, float.Parse(sourceObj.Parameters[1].Data.ToString()), 50))); //height
                    targetObj.Parameters.Add(new SetObjectParam(typeof(bool), sourceObj.Parameters[2].Data)); //cancelable
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[3].Data.ToString()))); //prio
                    targetObj.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(sourceObj.Parameters[4].Data.ToString()))); //onintersect
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[5].Data.ToString()))); //data0
                    targetObj.Parameters.Add(new SetObjectParam(typeof(Vector3), VectorMaths(mode, sourceObj.Parameters[6].Data.ToString()))); //data1
                    break;
            }

            targetObj.Transform = sourceObj.Transform;
            targetObj.Transform.Position.X = VolumeConversion(mode, targetObj.Transform.Position.X, 100);
            targetObj.Transform.Position.Y = VolumeConversion(mode, targetObj.Transform.Position.Y, 100);
            targetObj.Transform.Position.Z = VolumeConversion(mode, targetObj.Transform.Position.Z, 100);
            return targetObj;
        }
    }
}