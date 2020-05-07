using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HedgeLib.Sets;

namespace SONIC_THE_HEDGEHOG__2006__Object_Peeker
{
    public partial class Form1 : Form
    {
        string setName;
        string filepath;
        string[] setFiles;
        S06SetData s06SetData;
        string targetObject = "ambience_collision";
        string mode = "Unique Values of Specific Parameter";
        int specificParam = 3;
        List<string> specificParamList = new List<string>() { };
        public Form1()
        {
            InitializeComponent();

            targetObjectBox.SelectedIndex = 0;
            modeBox.SelectedIndex = 0;
        }

        private void RefreshSETList()
        {
            setFilesList.Items.Clear();
            if (Directory.Exists(filepath))
            {
                setFiles = Directory.GetFiles(filepath, "*.set", SearchOption.AllDirectories);
                foreach (var set in setFiles)
                {
                    setName = set.Remove(0, Path.GetDirectoryName(set).Length);
                    setName = setName.Remove(setName.Length - 4);
                    setName = setName.Replace("\\", "");
                    setFilesList.Items.Add(setName);
                }
                searchButton.Enabled = true;
            }
            else
            {
                searchButton.Enabled = false;
            }
        }

        private void setDirBox_TextChanged(object sender, EventArgs e)
        {
            filepath = setDirBox.Text;
            RefreshSETList();
        }

        private void setDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog customDirBrowser = new FolderBrowserDialog();
            if (customDirBrowser.ShowDialog() == DialogResult.OK)
            {
                filepath = customDirBrowser.SelectedPath;
                setDirBox.Text = filepath;
                RefreshSETList();
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            specificParamList.Clear();
            if (mode == "Unique Values of Specific Parameter")
            {
                int.TryParse(paramBox.Text, out specificParam);
                Console.WriteLine(specificParam);
            }

            outputBox.Clear();
            setFiles = Directory.GetFiles(filepath, "*.set", SearchOption.AllDirectories);
            foreach (var set in setFiles)
            {
                setName = set.Remove(0, Path.GetDirectoryName(set).Length);
                setName = setName.Remove(setName.Length - 4);
                setName = setName.Replace("\\", "");

                s06SetData = new S06SetData();
                s06SetData.Load(set);
                Console.WriteLine(set);

                foreach (SetObject s06Object in s06SetData.Objects)
                {
                    if (s06Object.ObjectType == targetObject)
                    {
                        switch (mode)
                        {
                            case "All Parameters":
                                outputBox.AppendText(targetObject + " in " + setName + " at " + s06Object.Transform.Position + "\n");
                                for (int i = 0; i < s06Object.Parameters.Count; i++)
                                {
                                    outputBox.AppendText(s06Object.Parameters[i].DataType + ": " + s06Object.Parameters[i].Data + "\n");
                                }
                                outputBox.AppendText("\n");
                                break;
                            case "Unique Values of Specific Parameter":
                                if (specificParam > s06Object.Parameters.Count)
                                {
                                    outputBox.Clear();
                                    outputBox.AppendText("Specified a parameter number higher than the number of parameters that exist in " + targetObject);
                                    return;
                                }
                                else
                                {
                                    if (!specificParamList.Contains(s06Object.Parameters[specificParam].Data.ToString()))
                                    {
                                        specificParamList.Add(s06Object.Parameters[specificParam].Data.ToString());
                                    }
                                }
                                break;
                            default:
                                outputBox.Clear();
                                outputBox.AppendText("You somehow managed to select a mode that doesn't exist. Well done?");
                                break;
                        }
                    }
                }
            }

            if (mode == "Unique Values of Specific Parameter")
            {
                specificParamList.Sort();
                specificParamList.ForEach(i => outputBox.AppendText(i + "\n"));
            }
            outputBox.AppendText("");
        }

        private void TargetObjectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            targetObject = targetObjectBox.SelectedItem.ToString();
        }

        private void ModeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mode = modeBox.SelectedItem.ToString();
            if (mode == "Unique Values of Specific Parameter")
            {
                paramBox.Enabled = true;
            }
            else
            {
                paramBox.Enabled = false;
            }
        }
    }
}
