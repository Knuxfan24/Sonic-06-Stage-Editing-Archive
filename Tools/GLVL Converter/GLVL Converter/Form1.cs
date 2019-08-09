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

namespace GLVL_Converter
{
    public partial class Form1 : Form
    {
        string filepath = "";
        string templates = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void FilepathBox_TextChanged(object sender, EventArgs e)
        {
            filepath = filepathBox.Text;
            CheckIfValid();
        }
        private void FilepathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog setBrowser = new OpenFileDialog();
            setBrowser.Title = "Select SET Data";
            setBrowser.Filter = "Sonic Generations SET Data (*.set.xml)|*.set.xml";
            setBrowser.FilterIndex = 1;
            setBrowser.RestoreDirectory = true;
            if (setBrowser.ShowDialog() == DialogResult.OK)
            {
                filepath = setBrowser.FileName;
                filepathBox.Text = filepath;
                CheckIfValid();
            }
        }

        private void TemplatesBox_TextChanged(object sender, EventArgs e)
        {
            templates = templatesBox.Text;
            CheckIfValid();
        }
        private void TemplatesButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog templatesBrowser = new FolderBrowserDialog();
            if (templatesBrowser.ShowDialog() == DialogResult.OK)
            {
                templates = templatesBrowser.SelectedPath;
                templatesBox.Text = templates;
                CheckIfValid();
            }
        }

        private void CheckIfValid()
        {
            if (filepath == "" || templates == "")
            {
                convertButton.Enabled = false;
            }
            if (filepath != "" && templates != "")
            {
                convertButton.Enabled = true;
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            Program.ConvertSET(filepath, templates);
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog configSave = new SaveFileDialog();
            configSave.Title = "Save Config";
            configSave.Filter = "Converter Config (*.c06)|*.c06";
            configSave.FilterIndex = 1;
            configSave.RestoreDirectory = true;
            if (configSave.ShowDialog() == DialogResult.OK)
            {
                using (Stream configFileLocation = File.Open(configSave.FileName, FileMode.Create))
                using (StreamWriter configFile = new StreamWriter(configFileLocation))
                {
                    configFile.WriteLine("SET File Path = " + filepath);
                    configFile.WriteLine("Template Directory = " + templates);
                    configFile.Close();
                }
            }
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog configLoad = new OpenFileDialog();
            configLoad.Title = "Load Config";
            configLoad.Filter = "Converter Config (*.c06)|*.c06";
            configLoad.FilterIndex = 1;
            configLoad.RestoreDirectory = true;
            if (configLoad.ShowDialog() == DialogResult.OK)
            {
                using (Stream configFileLocation = File.Open(configLoad.FileName, FileMode.Open))
                using (StreamReader configFile = new StreamReader(configFileLocation))
                {
                    string line;
                    string entryName = "";
                    string entryValue = "";
                    while ((line = configFile.ReadLine()) != null)
                    {
                        entryName = line.Split('=')[0];
                        entryName = entryName.Remove(entryName.Length - 1);
                        entryValue = line.Substring(line.IndexOf("=") + 2);

                        switch (entryName)
                        {
                            case "SET File Path":
                                filepath = entryValue;
                                filepathBox.Text = entryValue;
                                CheckIfValid();
                                break;
                            case "Template Directory":
                                templates = entryValue;
                                templatesBox.Text = entryValue;
                                CheckIfValid();
                                break;
                        }
                    }
                }
            }
        }
    }
}
