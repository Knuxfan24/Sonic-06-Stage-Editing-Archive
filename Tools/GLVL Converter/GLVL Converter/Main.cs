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
using Ookii.Dialogs;

namespace GLvl_Converter
{
    public partial class Main : Form
    {
        string version = "0.2-indev";
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
            VistaFolderBrowserDialog templatesBrowser = new VistaFolderBrowserDialog();
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
            idList.Items.Clear();
            tm_UpdateList.Start();
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
            this.MaximumSize = new System.Drawing.Size(int.MaxValue, 478);
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

        private void Tm_UpdateList_Tick(object sender, EventArgs e)
        {
            foreach (var item in GLVLtoS06.listOfIDs)
            {
                idList.Items.Add(item);
            }
            tm_UpdateList.Stop();
        }

        public static bool showIDs;

        private void Btn_ShowIDs_Click(object sender, EventArgs e)
        {
            if (showIDs)
            {
                idList.Visible = true;
                Height = 478;
                btn_ShowIDs.Text = "Hide IDs";

                showIDs = false;
            }
            else
            {
                idList.Visible = false;
                Height = 180;
                btn_ShowIDs.Text = "Show IDs";

                showIDs = true;
            }
        }
    }
}
