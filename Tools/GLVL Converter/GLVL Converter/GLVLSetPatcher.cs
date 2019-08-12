using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLvl_Converter
{
    class GLVLSetPatcher
    {
        static public void Patcher(string filepath, string templatesFolder)
        {
            string[] editedLua = File.ReadAllLines(filepath);
            bool objectFound = false;
            int objectParam = 0;
            string file;
            int lineNumber = 0;
            string objectName = "";
            List<string> paramNames = new List<string> { };

            foreach (string line in editedLua)
            {
                if (line.StartsWith("  <") && !line.StartsWith("  </"))
                {
                    if (!objectFound)
                    {
                        int startIndex = line.IndexOf("<") + 1;
                        int endIndex = line.IndexOf(">") + 1;
                        objectName = line.Substring(startIndex, endIndex - startIndex - 1);
                        file = Directory.GetFiles(templatesFolder, line.Substring(startIndex, endIndex - startIndex - 1) + ".xml", SearchOption.AllDirectories)
                        .FirstOrDefault();

                        paramNames.Clear();
                        string[] template = File.ReadAllLines(file);
                        foreach (string param in template)
                        {
                            if (param.Contains("type") && !param.Contains("Extra"))
                            {
                                var test = param.IndexOf("<");
                                var test2 = param.Substring(test + 1);
                                var test3 = test2.IndexOf(" ");
                                test2 = test2.Remove(test3);
                                paramNames.Add(test2);
                            }
                        }

                        objectParam = 0;
                        objectFound = true;
                    }
                }
                else
                {
                    if (line.StartsWith("    <") && objectFound)
                    {
                        if (line == "    <Position>")
                        {
                            objectFound = false;
                        }
                        else
                        {
                            if (line.Contains("</") && line.Contains("<") && line.Contains(">") && !line.Contains("  </"))
                            {
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

                                temp.ForEach(i => Console.Write("{0}", i));
                                Console.WriteLine();

                                //Change temp[1] & temp[5]
                                if (temp[1] != paramNames[objectParam]) 
                                {
                                    GLVLtoS06.listOfIDs.Add($"{temp[1]} in {objectName} has been replaced with {paramNames[objectParam]}");
                                }

                                temp[1] = paramNames[objectParam];
                                temp[5] = paramNames[objectParam];

                                temp.ForEach(i => Console.Write("{0}", i));
                                Console.WriteLine();
                                objectParam++;
                                editedLua[lineNumber] = String.Join("", temp);
                            }
                        }
                    }
                }
                lineNumber++;
            }
            File.WriteAllLines(filepath, editedLua);
        }
    }
}
