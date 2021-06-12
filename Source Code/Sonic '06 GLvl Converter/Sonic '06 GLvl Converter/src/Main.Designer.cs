namespace Sonic_06_GLvl_Converter
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Button_SourceSET = new System.Windows.Forms.Button();
            this.TextBox_SourceSET = new System.Windows.Forms.TextBox();
            this.Label_SourceSET = new System.Windows.Forms.Label();
            this.Label_Description_SourceSET = new System.Windows.Forms.Label();
            this.Button_GroupsXML = new System.Windows.Forms.Button();
            this.TextBox_GroupsXML = new System.Windows.Forms.TextBox();
            this.Label_GroupsXML = new System.Windows.Forms.Label();
            this.Label_Description_GroupsXML = new System.Windows.Forms.Label();
            this.Button_GLVLTemplates = new System.Windows.Forms.Button();
            this.TextBox_GLVLTemplates = new System.Windows.Forms.TextBox();
            this.Label_GLVLTemplates = new System.Windows.Forms.Label();
            this.Label_Description_GLVLTemplates = new System.Windows.Forms.Label();
            this.Button_TargetSET = new System.Windows.Forms.Button();
            this.TextBox_TargetSET = new System.Windows.Forms.TextBox();
            this.Label_TargetSET = new System.Windows.Forms.Label();
            this.Label_Description_TargetSET = new System.Windows.Forms.Label();
            this.Button_Convert = new System.Windows.Forms.Button();
            this.ListBox_ConversionLog = new System.Windows.Forms.ListBox();
            this.Button_GenerationsPatch = new System.Windows.Forms.Button();
            this.Button_About = new System.Windows.Forms.Button();
            this.TextBox_FilteredNames = new System.Windows.Forms.TextBox();
            this.Label_FilteredNames = new System.Windows.Forms.Label();
            this.Label_Description_FilteredNames = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Button_ReplaceNames = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Button_SourceSET
            // 
            this.Button_SourceSET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_SourceSET.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Button_SourceSET.FlatAppearance.BorderSize = 0;
            this.Button_SourceSET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_SourceSET.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Button_SourceSET.Location = new System.Drawing.Point(526, 34);
            this.Button_SourceSET.Name = "Button_SourceSET";
            this.Button_SourceSET.Size = new System.Drawing.Size(25, 23);
            this.Button_SourceSET.TabIndex = 164;
            this.Button_SourceSET.Text = "...";
            this.Button_SourceSET.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button_SourceSET.UseVisualStyleBackColor = false;
            this.Button_SourceSET.Click += new System.EventHandler(this.Button_SourceSET_Click);
            // 
            // TextBox_SourceSET
            // 
            this.TextBox_SourceSET.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_SourceSET.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBox_SourceSET.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_SourceSET.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBox_SourceSET.Location = new System.Drawing.Point(16, 34);
            this.TextBox_SourceSET.Name = "TextBox_SourceSET";
            this.TextBox_SourceSET.Size = new System.Drawing.Size(504, 23);
            this.TextBox_SourceSET.TabIndex = 163;
            this.TextBox_SourceSET.TextChanged += new System.EventHandler(this.TextBox_SourceSET_TextChanged);
            // 
            // Label_SourceSET
            // 
            this.Label_SourceSET.AutoSize = true;
            this.Label_SourceSET.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Label_SourceSET.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_SourceSET.Location = new System.Drawing.Point(13, 12);
            this.Label_SourceSET.Name = "Label_SourceSET";
            this.Label_SourceSET.Size = new System.Drawing.Size(73, 17);
            this.Label_SourceSET.TabIndex = 162;
            this.Label_SourceSET.Text = "Source SET";
            // 
            // Label_Description_SourceSET
            // 
            this.Label_Description_SourceSET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Description_SourceSET.AutoSize = true;
            this.Label_Description_SourceSET.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Description_SourceSET.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Label_Description_SourceSET.Location = new System.Drawing.Point(397, 14);
            this.Label_Description_SourceSET.Name = "Label_Description_SourceSET";
            this.Label_Description_SourceSET.Size = new System.Drawing.Size(123, 15);
            this.Label_Description_SourceSET.TabIndex = 165;
            this.Label_Description_SourceSET.Text = "The SET file to convert.";
            // 
            // Button_GroupsXML
            // 
            this.Button_GroupsXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_GroupsXML.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Button_GroupsXML.Enabled = false;
            this.Button_GroupsXML.FlatAppearance.BorderSize = 0;
            this.Button_GroupsXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_GroupsXML.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Button_GroupsXML.Location = new System.Drawing.Point(526, 86);
            this.Button_GroupsXML.Name = "Button_GroupsXML";
            this.Button_GroupsXML.Size = new System.Drawing.Size(25, 23);
            this.Button_GroupsXML.TabIndex = 168;
            this.Button_GroupsXML.Text = "...";
            this.Button_GroupsXML.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button_GroupsXML.UseVisualStyleBackColor = false;
            this.Button_GroupsXML.Click += new System.EventHandler(this.Button_GroupsXML_Click);
            // 
            // TextBox_GroupsXML
            // 
            this.TextBox_GroupsXML.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_GroupsXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBox_GroupsXML.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_GroupsXML.Enabled = false;
            this.TextBox_GroupsXML.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBox_GroupsXML.Location = new System.Drawing.Point(16, 86);
            this.TextBox_GroupsXML.Name = "TextBox_GroupsXML";
            this.TextBox_GroupsXML.Size = new System.Drawing.Size(504, 23);
            this.TextBox_GroupsXML.TabIndex = 167;
            this.TextBox_GroupsXML.TextChanged += new System.EventHandler(this.TextBox_GroupsXML_TextChanged);
            // 
            // Label_GroupsXML
            // 
            this.Label_GroupsXML.AutoSize = true;
            this.Label_GroupsXML.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Label_GroupsXML.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_GroupsXML.Location = new System.Drawing.Point(13, 64);
            this.Label_GroupsXML.Name = "Label_GroupsXML";
            this.Label_GroupsXML.Size = new System.Drawing.Size(81, 17);
            this.Label_GroupsXML.TabIndex = 166;
            this.Label_GroupsXML.Text = "Groups XML";
            // 
            // Label_Description_GroupsXML
            // 
            this.Label_Description_GroupsXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Description_GroupsXML.AutoSize = true;
            this.Label_Description_GroupsXML.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Description_GroupsXML.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Label_Description_GroupsXML.Location = new System.Drawing.Point(241, 66);
            this.Label_Description_GroupsXML.Name = "Label_Description_GroupsXML";
            this.Label_Description_GroupsXML.Size = new System.Drawing.Size(279, 15);
            this.Label_Description_GroupsXML.TabIndex = 169;
            this.Label_Description_GroupsXML.Text = "The XML to save/read the groups to/from (optional).";
            // 
            // Button_GLVLTemplates
            // 
            this.Button_GLVLTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_GLVLTemplates.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Button_GLVLTemplates.FlatAppearance.BorderSize = 0;
            this.Button_GLVLTemplates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_GLVLTemplates.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Button_GLVLTemplates.Location = new System.Drawing.Point(526, 138);
            this.Button_GLVLTemplates.Name = "Button_GLVLTemplates";
            this.Button_GLVLTemplates.Size = new System.Drawing.Size(25, 23);
            this.Button_GLVLTemplates.TabIndex = 172;
            this.Button_GLVLTemplates.Text = "...";
            this.Button_GLVLTemplates.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button_GLVLTemplates.UseVisualStyleBackColor = false;
            this.Button_GLVLTemplates.Click += new System.EventHandler(this.Button_GLVLTemplates_Click);
            // 
            // TextBox_GLVLTemplates
            // 
            this.TextBox_GLVLTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_GLVLTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBox_GLVLTemplates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_GLVLTemplates.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBox_GLVLTemplates.Location = new System.Drawing.Point(16, 138);
            this.TextBox_GLVLTemplates.Name = "TextBox_GLVLTemplates";
            this.TextBox_GLVLTemplates.Size = new System.Drawing.Size(504, 23);
            this.TextBox_GLVLTemplates.TabIndex = 171;
            this.TextBox_GLVLTemplates.TextChanged += new System.EventHandler(this.TextBox_GLVLTemplates_TextChanged);
            // 
            // Label_GLVLTemplates
            // 
            this.Label_GLVLTemplates.AutoSize = true;
            this.Label_GLVLTemplates.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Label_GLVLTemplates.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_GLVLTemplates.Location = new System.Drawing.Point(13, 116);
            this.Label_GLVLTemplates.Name = "Label_GLVLTemplates";
            this.Label_GLVLTemplates.Size = new System.Drawing.Size(94, 17);
            this.Label_GLVLTemplates.TabIndex = 170;
            this.Label_GLVLTemplates.Text = "GLvl Templates";
            // 
            // Label_Description_GLVLTemplates
            // 
            this.Label_Description_GLVLTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Description_GLVLTemplates.AutoSize = true;
            this.Label_Description_GLVLTemplates.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Description_GLVLTemplates.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Label_Description_GLVLTemplates.Location = new System.Drawing.Point(233, 118);
            this.Label_Description_GLVLTemplates.Name = "Label_Description_GLVLTemplates";
            this.Label_Description_GLVLTemplates.Size = new System.Drawing.Size(287, 15);
            this.Label_Description_GLVLTemplates.TabIndex = 173;
            this.Label_Description_GLVLTemplates.Text = "The folder containing the GLvl templates for Sonic \'06.";
            // 
            // Button_TargetSET
            // 
            this.Button_TargetSET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_TargetSET.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Button_TargetSET.Enabled = false;
            this.Button_TargetSET.FlatAppearance.BorderSize = 0;
            this.Button_TargetSET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_TargetSET.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Button_TargetSET.Location = new System.Drawing.Point(526, 190);
            this.Button_TargetSET.Name = "Button_TargetSET";
            this.Button_TargetSET.Size = new System.Drawing.Size(25, 23);
            this.Button_TargetSET.TabIndex = 176;
            this.Button_TargetSET.Text = "...";
            this.Button_TargetSET.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button_TargetSET.UseVisualStyleBackColor = false;
            this.Button_TargetSET.Click += new System.EventHandler(this.Button_TargetSET_Click);
            // 
            // TextBox_TargetSET
            // 
            this.TextBox_TargetSET.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_TargetSET.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBox_TargetSET.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_TargetSET.Enabled = false;
            this.TextBox_TargetSET.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBox_TargetSET.Location = new System.Drawing.Point(16, 190);
            this.TextBox_TargetSET.Name = "TextBox_TargetSET";
            this.TextBox_TargetSET.Size = new System.Drawing.Size(504, 23);
            this.TextBox_TargetSET.TabIndex = 175;
            this.TextBox_TargetSET.TextChanged += new System.EventHandler(this.TextBox_TargetSET_TextChanged);
            // 
            // Label_TargetSET
            // 
            this.Label_TargetSET.AutoSize = true;
            this.Label_TargetSET.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Label_TargetSET.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_TargetSET.Location = new System.Drawing.Point(13, 168);
            this.Label_TargetSET.Name = "Label_TargetSET";
            this.Label_TargetSET.Size = new System.Drawing.Size(70, 17);
            this.Label_TargetSET.TabIndex = 174;
            this.Label_TargetSET.Text = "Target SET";
            // 
            // Label_Description_TargetSET
            // 
            this.Label_Description_TargetSET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Description_TargetSET.AutoSize = true;
            this.Label_Description_TargetSET.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Description_TargetSET.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Label_Description_TargetSET.Location = new System.Drawing.Point(324, 170);
            this.Label_Description_TargetSET.Name = "Label_Description_TargetSET";
            this.Label_Description_TargetSET.Size = new System.Drawing.Size(196, 15);
            this.Label_Description_TargetSET.TabIndex = 177;
            this.Label_Description_TargetSET.Text = "The file to save the converted SET to.";
            // 
            // Button_Convert
            // 
            this.Button_Convert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Convert.BackColor = System.Drawing.Color.PaleGreen;
            this.Button_Convert.Enabled = false;
            this.Button_Convert.FlatAppearance.BorderSize = 0;
            this.Button_Convert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Convert.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Button_Convert.Location = new System.Drawing.Point(476, 598);
            this.Button_Convert.Name = "Button_Convert";
            this.Button_Convert.Size = new System.Drawing.Size(75, 23);
            this.Button_Convert.TabIndex = 178;
            this.Button_Convert.Text = "Convert";
            this.Button_Convert.UseVisualStyleBackColor = false;
            this.Button_Convert.Click += new System.EventHandler(this.Button_Convert_Click);
            // 
            // ListBox_ConversionLog
            // 
            this.ListBox_ConversionLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListBox_ConversionLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ListBox_ConversionLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListBox_ConversionLog.ForeColor = System.Drawing.SystemColors.Control;
            this.ListBox_ConversionLog.FormattingEnabled = true;
            this.ListBox_ConversionLog.ItemHeight = 15;
            this.ListBox_ConversionLog.Location = new System.Drawing.Point(16, 307);
            this.ListBox_ConversionLog.Name = "ListBox_ConversionLog";
            this.ListBox_ConversionLog.Size = new System.Drawing.Size(536, 287);
            this.ListBox_ConversionLog.TabIndex = 179;
            // 
            // Button_GenerationsPatch
            // 
            this.Button_GenerationsPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_GenerationsPatch.BackColor = System.Drawing.Color.Plum;
            this.Button_GenerationsPatch.Enabled = false;
            this.Button_GenerationsPatch.FlatAppearance.BorderSize = 0;
            this.Button_GenerationsPatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_GenerationsPatch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Button_GenerationsPatch.Location = new System.Drawing.Point(320, 598);
            this.Button_GenerationsPatch.Name = "Button_GenerationsPatch";
            this.Button_GenerationsPatch.Size = new System.Drawing.Size(150, 23);
            this.Button_GenerationsPatch.TabIndex = 181;
            this.Button_GenerationsPatch.Text = "Patch Generations SET";
            this.Button_GenerationsPatch.UseVisualStyleBackColor = false;
            this.Button_GenerationsPatch.Click += new System.EventHandler(this.Button_GenerationsPatch_Click);
            // 
            // Button_About
            // 
            this.Button_About.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_About.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Button_About.FlatAppearance.BorderSize = 0;
            this.Button_About.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_About.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Button_About.Location = new System.Drawing.Point(16, 598);
            this.Button_About.Name = "Button_About";
            this.Button_About.Size = new System.Drawing.Size(75, 23);
            this.Button_About.TabIndex = 182;
            this.Button_About.Text = "About";
            this.Button_About.UseVisualStyleBackColor = false;
            this.Button_About.Click += new System.EventHandler(this.Button_About_Click);
            // 
            // TextBox_FilteredNames
            // 
            this.TextBox_FilteredNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_FilteredNames.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBox_FilteredNames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_FilteredNames.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBox_FilteredNames.Location = new System.Drawing.Point(16, 242);
            this.TextBox_FilteredNames.Name = "TextBox_FilteredNames";
            this.TextBox_FilteredNames.Size = new System.Drawing.Size(535, 23);
            this.TextBox_FilteredNames.TabIndex = 184;
            this.TextBox_FilteredNames.TextChanged += new System.EventHandler(this.TextBox_FilteredNames_TextChanged);
            // 
            // Label_FilteredNames
            // 
            this.Label_FilteredNames.AutoSize = true;
            this.Label_FilteredNames.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Label_FilteredNames.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_FilteredNames.Location = new System.Drawing.Point(13, 220);
            this.Label_FilteredNames.Name = "Label_FilteredNames";
            this.Label_FilteredNames.Size = new System.Drawing.Size(138, 17);
            this.Label_FilteredNames.TabIndex = 183;
            this.Label_FilteredNames.Text = "Filtered Object Names";
            // 
            // Label_Description_FilteredNames
            // 
            this.Label_Description_FilteredNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Description_FilteredNames.AutoSize = true;
            this.Label_Description_FilteredNames.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Description_FilteredNames.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Label_Description_FilteredNames.Location = new System.Drawing.Point(258, 222);
            this.Label_Description_FilteredNames.Name = "Label_Description_FilteredNames";
            this.Label_Description_FilteredNames.Size = new System.Drawing.Size(293, 15);
            this.Label_Description_FilteredNames.TabIndex = 186;
            this.Label_Description_FilteredNames.Text = "Object names to NOT overwrite, seperated by commas.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(16, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 17);
            this.label3.TabIndex = 187;
            this.label3.Text = "Conversion Log";
            // 
            // Button_ReplaceNames
            // 
            this.Button_ReplaceNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_ReplaceNames.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Button_ReplaceNames.Enabled = false;
            this.Button_ReplaceNames.FlatAppearance.BorderSize = 0;
            this.Button_ReplaceNames.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_ReplaceNames.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Button_ReplaceNames.Location = new System.Drawing.Point(164, 598);
            this.Button_ReplaceNames.Name = "Button_ReplaceNames";
            this.Button_ReplaceNames.Size = new System.Drawing.Size(150, 23);
            this.Button_ReplaceNames.TabIndex = 188;
            this.Button_ReplaceNames.Text = "Replace Object Names";
            this.Button_ReplaceNames.UseVisualStyleBackColor = false;
            this.Button_ReplaceNames.Click += new System.EventHandler(this.Button_ReplaceNames_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(566, 636);
            this.Controls.Add(this.Button_ReplaceNames);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBox_FilteredNames);
            this.Controls.Add(this.Label_FilteredNames);
            this.Controls.Add(this.Label_Description_FilteredNames);
            this.Controls.Add(this.Button_About);
            this.Controls.Add(this.Button_GenerationsPatch);
            this.Controls.Add(this.ListBox_ConversionLog);
            this.Controls.Add(this.Button_Convert);
            this.Controls.Add(this.Button_TargetSET);
            this.Controls.Add(this.TextBox_TargetSET);
            this.Controls.Add(this.Label_TargetSET);
            this.Controls.Add(this.Label_Description_TargetSET);
            this.Controls.Add(this.Button_GLVLTemplates);
            this.Controls.Add(this.TextBox_GLVLTemplates);
            this.Controls.Add(this.Label_GLVLTemplates);
            this.Controls.Add(this.Label_Description_GLVLTemplates);
            this.Controls.Add(this.Button_GroupsXML);
            this.Controls.Add(this.TextBox_GroupsXML);
            this.Controls.Add(this.Label_GroupsXML);
            this.Controls.Add(this.Label_Description_GroupsXML);
            this.Controls.Add(this.Button_SourceSET);
            this.Controls.Add(this.TextBox_SourceSET);
            this.Controls.Add(this.Label_SourceSET);
            this.Controls.Add(this.Label_Description_SourceSET);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(582, 579);
            this.Name = "Main";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Sonic \'06 GLvl Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_SourceSET;
        private System.Windows.Forms.TextBox TextBox_SourceSET;
        private System.Windows.Forms.Label Label_SourceSET;
        private System.Windows.Forms.Label Label_Description_SourceSET;
        private System.Windows.Forms.Button Button_GroupsXML;
        private System.Windows.Forms.TextBox TextBox_GroupsXML;
        private System.Windows.Forms.Label Label_GroupsXML;
        private System.Windows.Forms.Label Label_Description_GroupsXML;
        private System.Windows.Forms.Button Button_GLVLTemplates;
        private System.Windows.Forms.TextBox TextBox_GLVLTemplates;
        private System.Windows.Forms.Label Label_GLVLTemplates;
        private System.Windows.Forms.Label Label_Description_GLVLTemplates;
        private System.Windows.Forms.Button Button_TargetSET;
        private System.Windows.Forms.TextBox TextBox_TargetSET;
        private System.Windows.Forms.Label Label_TargetSET;
        private System.Windows.Forms.Button Button_Convert;
        private System.Windows.Forms.ListBox ListBox_ConversionLog;
        private System.Windows.Forms.Button Button_GenerationsPatch;
        private System.Windows.Forms.Label Label_Description_TargetSET;
        private System.Windows.Forms.Button Button_About;
        private System.Windows.Forms.TextBox TextBox_FilteredNames;
        private System.Windows.Forms.Label Label_FilteredNames;
        private System.Windows.Forms.Label Label_Description_FilteredNames;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Button_ReplaceNames;
    }
}

