using System;
using System.IO;
using System.Linq;
using HedgeLib.Sets;
using System.Collections.Generic;

namespace Sonic_06_GLvl_Converter
{
    class GLvlPatcher
    {
        public static void PatchParameterNames(string sourceSETPath, string templatesPath)
        {
            string[] sourceSET = File.ReadAllLines(sourceSETPath);
            bool objectFound = false;
            int objectParam = 0;
            string file;
            int lineNumber = 0;
            string objectName = "";
            List<string> paramNames = new List<string> { };

            foreach (string line in sourceSET) {
                if (line.StartsWith("  <") && !line.StartsWith("  </")) {
                    if (!objectFound) {
                        int startIndex = line.IndexOf("<") + 1;
                        int endIndex = line.IndexOf(">") + 1;

                        objectName = line.Substring(startIndex, endIndex - startIndex - 1);
                        file = Directory.GetFiles(templatesPath, line.Substring(startIndex, endIndex - startIndex - 1) + ".xml", SearchOption.AllDirectories)
                        .FirstOrDefault();

                        if (file == null) {
                            lineNumber++;
                            continue;
                        }

                        paramNames.Clear();
                        string[] template = File.ReadAllLines(file);
                        foreach (string param in template)
                            if (param.Contains("type") && !param.Contains("Extra")) {
                                var test = param.IndexOf("<");
                                var test2 = param.Substring(test + 1);
                                var test3 = test2.IndexOf(" ");
                                test2 = test2.Remove(test3);
                                paramNames.Add(test2);
                            }

                        objectParam = 0;
                        objectFound = true;
                    }
                } else {
                    if (line.StartsWith("    <") && objectFound)
                        if (line == "    <Position>") objectFound = false;
                        else {
                            if (line.Contains("</") && line.Contains("<") && line.Contains(">") && !line.Contains("  </")) {
                                List<string> temp = new List<string> { "    <" };
                                int startIndex = line.IndexOf("<") + 1;
                                int endIndex = line.IndexOf(">") + 1;
                                temp.Add(line.Substring(startIndex, endIndex - startIndex - 1));
                                temp.Add(">");

                                startIndex = line.IndexOf(">") + 1;
                                endIndex = line.IndexOf("</") + 1;
                                temp.Add(line.Substring(startIndex, endIndex - startIndex - 1));

                                temp.Add("</");
                                temp.Add(temp[1]);
                                temp.Add(">");

                                //Change temp[1] & temp[5]
                                if (temp[1] != paramNames[objectParam])
                                    Main.listOfIDs.Add($"[{DateTime.Now:HH:mm:ss tt}] '{temp[1]}' in '{objectName}' has been replaced with '{paramNames[objectParam]}'");

                                temp[1] = paramNames[objectParam];
                                temp[5] = paramNames[objectParam];
                                objectParam++;
                                sourceSET[lineNumber] = string.Join("", temp);
                            }
                        }
                }
                lineNumber++;
            }

            File.WriteAllLines(sourceSETPath, sourceSET);
        }
    
        public static void PatchObjectIDs(string sourceSETPath) {
            GensSetData sourceSET = new GensSetData();
            sourceSET.Load(sourceSETPath);
            uint objectID = 0;

            foreach (SetObject obj in sourceSET.Objects) {
                Main.listOfIDs.Add($"[{DateTime.Now:HH:mm:ss tt}] Object '{objectID}' ID changed from {obj.ObjectID} to {objectID}");
                obj.ObjectID = objectID;
                objectID++;
            }

            sourceSET.Save(sourceSETPath, true);
        }
    }
}
