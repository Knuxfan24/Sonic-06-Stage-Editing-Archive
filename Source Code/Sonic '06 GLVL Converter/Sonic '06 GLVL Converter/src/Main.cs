using System;
using System.IO;
using Ookii.Dialogs;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using Sonic_06_GLVL_Converter.Serialisers;

namespace Sonic_06_GLVL_Converter
{
    public partial class Main : Form
    {
        public static List<string> listOfIDs = new List<string>();

        public Main() {
            InitializeComponent();
            Properties.Settings.Default.SettingsSaving += Settings_SettingsSaving;
            LoadSettings();
        }

        /// <summary>
        /// If 'Properties.Settings.Default.Save()' is called, the 'LoadSettings()' function will be executed.
        /// </summary>
        private void Settings_SettingsSaving(object sender, CancelEventArgs e) => LoadSettings();

        private void LoadSettings() {
            if (Paths.CheckFileLegitimacy(Properties.Settings.Default.SourceSET)) TextBox_SourceSET.Text = Properties.Settings.Default.SourceSET;
            if (Paths.CheckFileLegitimacy(Properties.Settings.Default.GroupXML)) TextBox_GroupsXML.Text = Properties.Settings.Default.GroupXML;
            if (Paths.CheckPathLegitimacy(Properties.Settings.Default.GLVLTemplates)) TextBox_GLVLTemplates.Text = Properties.Settings.Default.GLVLTemplates;
            TextBox_TargetSET.Text = Properties.Settings.Default.TargetSET;

            // Enable several controls if the source path is legitimate.
            if (!(Button_TargetSET.Enabled =
                TextBox_TargetSET.Enabled =
                Button_GroupsXML.Enabled =
                TextBox_GroupsXML.Enabled = Paths.CheckFileLegitimacy(TextBox_SourceSET.Text)))
            {
                Label_TargetSET.ForeColor =
                Label_GroupsXML.ForeColor =
                Label_Description_TargetSET.ForeColor =
                Label_Description_GroupsXML.ForeColor = SystemColors.GrayText;
            }
            else {
                Label_TargetSET.ForeColor =
                Label_GroupsXML.ForeColor = SystemColors.Control;

                Label_Description_GroupsXML.ForeColor =
                Label_Description_TargetSET.ForeColor = SystemColors.ControlDark;
            }

            // Enable Patch Generations SET button if the source and templates paths are legitimate.
            Button_GenerationsPatch.Enabled = Paths.CheckFileLegitimacy(TextBox_SourceSET.Text) && Paths.CheckPathLegitimacy(TextBox_GLVLTemplates.Text);

            // Enable Convert button if the source and target paths are legitimate.
            Button_Convert.Enabled = Paths.CheckFileLegitimacy(TextBox_SourceSET.Text) || Paths.CheckFileLegitimacy(TextBox_TargetSET.Text);
        }

        private void TextBox_SourceSET_TextChanged(object sender, EventArgs e) {
            Properties.Settings.Default.SourceSET = TextBox_SourceSET.Text;
            Properties.Settings.Default.Save();
        }

        private void TextBox_TargetSET_TextChanged(object sender, EventArgs e) {
            Properties.Settings.Default.TargetSET = TextBox_TargetSET.Text;

            // Enable Convert button if the source and target paths are legitimate.
            Button_Convert.Enabled = Paths.CheckFileLegitimacy(TextBox_SourceSET.Text) || Paths.CheckFileLegitimacy(TextBox_TargetSET.Text);

            // Save changes...
            Properties.Settings.Default.Save();
        }

        private void Button_SourceSET_Click(object sender, EventArgs e) {
            OpenFileDialog sourceSETBrowser = new OpenFileDialog {
                Title = "Select source SET data...",
                Filter = "Sonic Generations SET Data (*.set.xml)|*.set.xml|Sonic '06 SET Data (*.set)|*.set",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (sourceSETBrowser.ShowDialog() == DialogResult.OK)
                TextBox_SourceSET.Text = sourceSETBrowser.FileName;
        }

        private void Button_TargetSET_Click(object sender, EventArgs e) {
            string filetypeFilter = "Sonic '06 SET Data (*.set)|*.set";
            if (Path.GetExtension(TextBox_SourceSET.Text) == ".set") filetypeFilter = "Sonic Generations SET Data (*.set.xml)|*.set.xml";

            SaveFileDialog targetSETBrowser = new SaveFileDialog {
                Title = "Select target SET data...",
                Filter = filetypeFilter,
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (targetSETBrowser.ShowDialog() == DialogResult.OK)
                TextBox_TargetSET.Text = targetSETBrowser.FileName;
        }

        private void Button_GroupsXML_Click(object sender, EventArgs e) {
            if (Path.GetExtension(TextBox_SourceSET.Text) == ".xml") {
                OpenFileDialog groupXMLBrowser = new OpenFileDialog {
                    Title = "Select group data...",
                    Filter = "Sonic '06 Group Data (*.group.xml)|*.group.xml",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };

                if (groupXMLBrowser.ShowDialog() == DialogResult.OK)
                    TextBox_GroupsXML.Text = groupXMLBrowser.FileName;
            }
            else if (Path.GetExtension(TextBox_SourceSET.Text) == ".set") {
                SaveFileDialog groupXMLBrowser = new SaveFileDialog {
                    Title = "Select Group Data XML",
                    Filter = "Sonic '06 Group Data (*.group.xml)|*.group.xml",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };

                if (groupXMLBrowser.ShowDialog() == DialogResult.OK)
                    TextBox_GroupsXML.Text = groupXMLBrowser.FileName;
            }
        }

        private void Button_GLVLTemplates_Click(object sender, EventArgs e) {
            VistaFolderBrowserDialog templatesBrowser = new VistaFolderBrowserDialog() {
                Description = "Select the directory containing your GLvl templates...",
                UseDescriptionForTitle = true
            };

            if (templatesBrowser.ShowDialog() == DialogResult.OK)
                TextBox_GLVLTemplates.Text = templatesBrowser.SelectedPath;
        }

        private void Button_Convert_Click(object sender, EventArgs e) {
            listOfIDs.Clear();
            ListBox_ConversionLog.Items.Clear();

            //GLvl to S06
            if (Path.GetExtension(TextBox_SourceSET.Text) == ".xml"){
                Conversion.ConvertSET(TextBox_SourceSET.Text, TextBox_GroupsXML.Text, TextBox_TargetSET.Text, false);
                for (int i = listOfIDs.Count - 1; i >= 0; i--)
                    ListBox_ConversionLog.Items.Add(listOfIDs[i]);
            }

            //S06 to GLvl
            if (Path.GetExtension(TextBox_SourceSET.Text) == ".set")  {
                Conversion.ConvertSET(TextBox_SourceSET.Text, TextBox_GroupsXML.Text, TextBox_TargetSET.Text, true);
                for (int i = listOfIDs.Count - 1; i >= 0; i--)
                    ListBox_ConversionLog.Items.Add(listOfIDs[i]);

                if (Path.GetExtension(TextBox_GroupsXML.Text) == ".xml")
                    Conversion.GLVLGroupExport(TextBox_SourceSET.Text, TextBox_GroupsXML.Text);

                listOfIDs.Clear();
                GLvlPatcher.PatchObjectIDs(TextBox_TargetSET.Text);

                for (int i = 0; i < listOfIDs.Count; i++)
                    ListBox_ConversionLog.Items.Add(listOfIDs[i]);

                listOfIDs.Clear();
                GLvlPatcher.PatchParameterNames(TextBox_TargetSET.Text, TextBox_GLVLTemplates.Text);

                for (int i = 0; i < listOfIDs.Count; i++)
                    ListBox_ConversionLog.Items.Add(listOfIDs[i]);
            }
        }

        private void TextBox_GLVLTemplates_TextChanged(object sender, EventArgs e) {
            Properties.Settings.Default.GLVLTemplates = TextBox_GLVLTemplates.Text;
            Properties.Settings.Default.Save();
        }

        private void TextBox_GroupsXML_TextChanged(object sender, EventArgs e) {
            Properties.Settings.Default.GroupXML = TextBox_GroupsXML.Text;
            Properties.Settings.Default.Save();
        }

        private void Button_GenerationsPatch_Click(object sender, EventArgs e) {
            ListBox_ConversionLog.Items.Clear();
            listOfIDs.Clear();
            GLvlPatcher.PatchObjectIDs(TextBox_SourceSET.Text);

            for (int i = 0; i < listOfIDs.Count; i++)
                ListBox_ConversionLog.Items.Add(listOfIDs[i]);

            listOfIDs.Clear();
            GLvlPatcher.PatchParameterNames(TextBox_SourceSET.Text, TextBox_GLVLTemplates.Text);

            for (int i = 0; i < listOfIDs.Count; i++)
                ListBox_ConversionLog.Items.Add(listOfIDs[i]);
        }

        private void Button_About_Click(object sender, EventArgs e) {
            MessageBox.Show("Sonic '06 GLvl Converter\n" +
                            $"{Program.GlobalVersion}\n\n" +
                            "" +
                            "Knuxfan24 - Lead programmer and reverse-engineer\n" +
                            "HyperPolygon64 - UI stuff and slaved away at code",
                            "About Sonic '06 GLvl Converter", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
