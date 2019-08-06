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

namespace GLvl_Converter
{
    public partial class Main : Form
    {
        string version = "0.1-indev";
        string filepath = "";
        string templates = "";
        string outputpath = "";
        bool bodgeWorkaround = true;

        public Main()
        {
            InitializeComponent();
            s06toGLVLCheckbox.Checked = Properties.Settings.Default.s06toGLVLCheck;
        }

        private void FilepathBox_TextChanged(object sender, EventArgs e)
        {
            filepath = filepathBox.Text;
            Properties.Settings.Default.lastSavedSET = filepathBox.Text;
            Properties.Settings.Default.Save();
            CheckIfValid();
        }
        private void FilepathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog setBrowser = new OpenFileDialog();
            setBrowser.Title = "Select SET Data";
            if (s06toGLVLCheckbox.Checked) { setBrowser.Filter = "Sonic '06 SET Data (*.set)|*.set"; }
            else { setBrowser.Filter = "Sonic Generations SET Data (*.set.xml)|*.set.xml"; }
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
            Properties.Settings.Default.glvlTemplates = templatesBox.Text;
            Properties.Settings.Default.Save();
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
            if (!s06toGLVLCheckbox.Checked) { GLVLtoS06.ConvertSET(filepath, templates); }
            else { s06toGLVL.ConvertSET(filepath, templates, outputpath); }
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

        private void Main_Load(object sender, EventArgs e)
        {
            filepathBox.Text = Properties.Settings.Default.lastSavedSET;
            templatesBox.Text = Properties.Settings.Default.glvlTemplates;
        }

        private void OutputpathButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog setBrowser = new SaveFileDialog();
            setBrowser.Title = "Select Output Data";

            if (!s06toGLVLCheckbox.Checked) { setBrowser.Filter = "Sonic '06 SET Data (*.set)|*.set"; }
            else { setBrowser.Filter = "Sonic Generations SET Data (*.set.xml)|*.set.xml"; }
            setBrowser.FilterIndex = 1;
            setBrowser.RestoreDirectory = true;
            if (setBrowser.ShowDialog() == DialogResult.OK)
            {
                outputpath = setBrowser.FileName;
                outputpathBox.Text = outputpath;
                CheckIfValid();
            }
        }

        private void OutputpathBox_TextChanged(object sender, EventArgs e)
        {
            outputpath = outputpathBox.Text;
            Properties.Settings.Default.lastSavedOutput = outputpathBox.Text;
            Properties.Settings.Default.Save();
            CheckIfValid();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"GLvl Converter\n\n{version}\n\nContributors:\nKnuxfan24 - Lead Developer\nHyper - Co-developer", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void S06toGLVLCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!bodgeWorkaround)
            {
                filepathBox.Text = "";
                outputpathBox.Text = "";
            }
            else
            {
                bodgeWorkaround = false;
            }
            Properties.Settings.Default.s06toGLVLCheck = s06toGLVLCheckbox.Checked;
            Properties.Settings.Default.Save();
            if (s06toGLVLCheckbox.Checked)
            {
                filepathLabel.Text = "Sonic '06 SET:";
                outputLabel.Text = "Output XML:";
            }
            if (!s06toGLVLCheckbox.Checked)
            {
                filepathLabel.Text = "Generations SET:";
                outputLabel.Text = "Output SET:";
            }
        }
    }
}
