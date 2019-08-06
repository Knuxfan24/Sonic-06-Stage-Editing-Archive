namespace GLvl_Converter
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
            this.filepathBox = new System.Windows.Forms.TextBox();
            this.templatesBox = new System.Windows.Forms.TextBox();
            this.templatesLabel = new System.Windows.Forms.Label();
            this.templatesButton = new System.Windows.Forms.Button();
            this.filepathButton = new System.Windows.Forms.Button();
            this.convertButton = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputpathButton = new System.Windows.Forms.Button();
            this.outputLabel = new System.Windows.Forms.Label();
            this.outputpathBox = new System.Windows.Forms.TextBox();
            this.s06toGLVLCheckbox = new System.Windows.Forms.CheckBox();
            this.filepathLabel = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // filepathBox
            // 
            this.filepathBox.Location = new System.Drawing.Point(105, 30);
            this.filepathBox.Name = "filepathBox";
            this.filepathBox.Size = new System.Drawing.Size(335, 20);
            this.filepathBox.TabIndex = 0;
            this.filepathBox.TextChanged += new System.EventHandler(this.FilepathBox_TextChanged);
            // 
            // templatesBox
            // 
            this.templatesBox.Location = new System.Drawing.Point(105, 56);
            this.templatesBox.Name = "templatesBox";
            this.templatesBox.Size = new System.Drawing.Size(335, 20);
            this.templatesBox.TabIndex = 1;
            this.templatesBox.TextChanged += new System.EventHandler(this.TemplatesBox_TextChanged);
            // 
            // templatesLabel
            // 
            this.templatesLabel.AutoSize = true;
            this.templatesLabel.Location = new System.Drawing.Point(17, 60);
            this.templatesLabel.Name = "templatesLabel";
            this.templatesLabel.Size = new System.Drawing.Size(84, 13);
            this.templatesLabel.TabIndex = 3;
            this.templatesLabel.Text = "GLvl Templates:";
            // 
            // templatesButton
            // 
            this.templatesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.templatesButton.FlatAppearance.BorderSize = 0;
            this.templatesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.templatesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templatesButton.Location = new System.Drawing.Point(446, 56);
            this.templatesButton.Name = "templatesButton";
            this.templatesButton.Size = new System.Drawing.Size(22, 20);
            this.templatesButton.TabIndex = 29;
            this.templatesButton.Text = "...";
            this.templatesButton.UseVisualStyleBackColor = false;
            this.templatesButton.Click += new System.EventHandler(this.TemplatesButton_Click);
            // 
            // filepathButton
            // 
            this.filepathButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.filepathButton.FlatAppearance.BorderSize = 0;
            this.filepathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filepathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filepathButton.Location = new System.Drawing.Point(446, 30);
            this.filepathButton.Name = "filepathButton";
            this.filepathButton.Size = new System.Drawing.Size(22, 20);
            this.filepathButton.TabIndex = 28;
            this.filepathButton.Text = "...";
            this.filepathButton.UseVisualStyleBackColor = false;
            this.filepathButton.Click += new System.EventHandler(this.FilepathButton_Click);
            // 
            // convertButton
            // 
            this.convertButton.BackColor = System.Drawing.Color.LightGreen;
            this.convertButton.Enabled = false;
            this.convertButton.FlatAppearance.BorderSize = 0;
            this.convertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.convertButton.Location = new System.Drawing.Point(393, 109);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(75, 23);
            this.convertButton.TabIndex = 30;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = false;
            this.convertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(478, 24);
            this.menuStrip.TabIndex = 31;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadToolStripMenuItem.Image")));
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.loadToolStripMenuItem.Text = "Load Configuration";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.saveToolStripMenuItem.Text = "Save Configuration";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // outputpathButton
            // 
            this.outputpathButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.outputpathButton.FlatAppearance.BorderSize = 0;
            this.outputpathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.outputpathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputpathButton.Location = new System.Drawing.Point(446, 82);
            this.outputpathButton.Name = "outputpathButton";
            this.outputpathButton.Size = new System.Drawing.Size(22, 20);
            this.outputpathButton.TabIndex = 34;
            this.outputpathButton.Text = "...";
            this.outputpathButton.UseVisualStyleBackColor = false;
            this.outputpathButton.Click += new System.EventHandler(this.OutputpathButton_Click);
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(35, 86);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(66, 13);
            this.outputLabel.TabIndex = 33;
            this.outputLabel.Text = "Output SET:";
            // 
            // outputpathBox
            // 
            this.outputpathBox.Location = new System.Drawing.Point(105, 82);
            this.outputpathBox.Name = "outputpathBox";
            this.outputpathBox.Size = new System.Drawing.Size(335, 20);
            this.outputpathBox.TabIndex = 32;
            this.outputpathBox.TextChanged += new System.EventHandler(this.OutputpathBox_TextChanged);
            // 
            // s06toGLVLCheckbox
            // 
            this.s06toGLVLCheckbox.AutoSize = true;
            this.s06toGLVLCheckbox.Location = new System.Drawing.Point(13, 112);
            this.s06toGLVLCheckbox.Name = "s06toGLVLCheckbox";
            this.s06toGLVLCheckbox.Size = new System.Drawing.Size(100, 17);
            this.s06toGLVLCheckbox.TabIndex = 35;
            this.s06toGLVLCheckbox.Text = "Convert to GLvl";
            this.s06toGLVLCheckbox.UseVisualStyleBackColor = true;
            this.s06toGLVLCheckbox.CheckedChanged += new System.EventHandler(this.S06toGLVLCheckbox_CheckedChanged);
            // 
            // filepathLabel
            // 
            this.filepathLabel.Location = new System.Drawing.Point(0, 30);
            this.filepathLabel.Name = "filepathLabel";
            this.filepathLabel.Size = new System.Drawing.Size(101, 23);
            this.filepathLabel.TabIndex = 53;
            this.filepathLabel.Text = "Generations SET:";
            this.filepathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 141);
            this.Controls.Add(this.filepathLabel);
            this.Controls.Add(this.s06toGLVLCheckbox);
            this.Controls.Add(this.outputpathButton);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.outputpathBox);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.templatesButton);
            this.Controls.Add(this.filepathButton);
            this.Controls.Add(this.templatesLabel);
            this.Controls.Add(this.templatesBox);
            this.Controls.Add(this.filepathBox);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GLvl Converter";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox filepathBox;
        private System.Windows.Forms.TextBox templatesBox;
        private System.Windows.Forms.Label templatesLabel;
        private System.Windows.Forms.Button templatesButton;
        private System.Windows.Forms.Button filepathButton;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button outputpathButton;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.TextBox outputpathBox;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.CheckBox s06toGLVLCheckbox;
        private System.Windows.Forms.Label filepathLabel;
    }
}

