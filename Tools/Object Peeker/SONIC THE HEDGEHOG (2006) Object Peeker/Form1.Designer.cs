namespace SONIC_THE_HEDGEHOG__2006__Object_Peeker
{
    partial class Form1
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
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.setDirBox = new System.Windows.Forms.TextBox();
            this.setDirButton = new System.Windows.Forms.Button();
            this.targetObjectBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.setFilesList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.modeBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.paramBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(371, 5);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(417, 433);
            this.outputBox.TabIndex = 0;
            this.outputBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "SET Directory:";
            // 
            // setDirBox
            // 
            this.setDirBox.Location = new System.Drawing.Point(91, 6);
            this.setDirBox.Name = "setDirBox";
            this.setDirBox.Size = new System.Drawing.Size(246, 20);
            this.setDirBox.TabIndex = 2;
            this.setDirBox.TextChanged += new System.EventHandler(this.setDirBox_TextChanged);
            // 
            // setDirButton
            // 
            this.setDirButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setDirButton.Location = new System.Drawing.Point(343, 5);
            this.setDirButton.Name = "setDirButton";
            this.setDirButton.Size = new System.Drawing.Size(22, 22);
            this.setDirButton.TabIndex = 43;
            this.setDirButton.Text = "...";
            this.setDirButton.UseVisualStyleBackColor = true;
            this.setDirButton.Click += new System.EventHandler(this.setDirButton_Click);
            // 
            // targetObjectBox
            // 
            this.targetObjectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetObjectBox.FormattingEnabled = true;
            this.targetObjectBox.Items.AddRange(new object[] {
            "ambience_collision",
            "amigo_collision",
            "aqa_balancer",
            "aqa_door",
            "aqa_glass_blue",
            "aqa_glass_red",
            "aqa_lamp",
            "aqa_launcher",
            "aqa_magnet",
            "aqa_mercury_small",
            "aqa_pond",
            "aqa_wyvern_fall",
            "bell",
            "bldgexplode",
            "brickwall",
            "brokenstairs_left",
            "brokenstairs_right",
            "bungee",
            "cameraeventbox",
            "cameraeventcylinder",
            "cameraeventsphere",
            "candlestick",
            "chainjump",
            "changelight",
            "common_cage",
            "common_chaosemerald",
            "common_dashring",
            "common_guillotine",
            "common_hint",
            "common_hint_collision",
            "common_jumpboard",
            "common_key",
            "common_laser",
            "common_lensflare",
            "common_object_event",
            "common_path_obj",
            "common_psimarksphere",
            "common_rainbowring",
            "common_ringswitch",
            "common_stopplayercollision",
            "common_switch",
            "common_terrain",
            "common_thorn",
            "common_warphole",
            "common_water_collision",
            "common_windcollision_box",
            "crater",
            "cscglass",
            "cscglassbuildbomb",
            "csctornado",
            "csctornadochase",
            "darkness",
            "dashpanel",
            "disk",
            "download_obj",
            "dtd_billiard",
            "dtd_billiardcounter",
            "dtd_billiardswitch",
            "dtd_door",
            "dtd_movingfloor",
            "dtd_pillar",
            "dtd_pillar_eagle",
            "dtd_sandeffect",
            "dtd_sandwave",
            "dtd_switchcounter",
            "eagle",
            "eggman_train",
            "end_inputwarp",
            "end_outputwarp",
            "end_soleannaswitch",
            "end_timekeeper",
            "enemy",
            "enemyextra",
            "espstairs_left",
            "espstairs_right",
            "espswing",
            "eventbox",
            "eventsphere",
            "flamesequence",
            "flamesingle",
            "flc_door",
            "flc_flamecore",
            "flc_volcanicbomb",
            "freezedmantle",
            "freight_train",
            "fruit",
            "gate",
            "glidewire",
            "goalring",
            "gondola",
            "hangingrock",
            "impulsesphere",
            "inclinedbridge",
            "inclinedstonebridge",
            "ironspring",
            "itembox_next",
            "itemboxa",
            "itemboxg",
            "jumppanel",
            "kdv_decalog",
            "kdv_door",
            "kdv_rainbow",
            "kingdomcrest",
            "lightanimation",
            "lotus",
            "medal_of_royal_bronze",
            "medal_of_royal_silver",
            "memory_of_past",
            "normal_train",
            "objectphysics",
            "objectphysics_item",
            "objecttownscar",
            "particle",
            "particlesphere",
            "passring",
            "pendulum",
            "physicspath",
            "player_goal",
            "player_npc",
            "player_start",
            "player_start2",
            "pointlight",
            "pointsample",
            "pole",
            "positionSample",
            "present",
            "radarmapmark",
            "rct_belt",
            "rct_door",
            "rct_seesaw",
            "rct_seesaw_silver",
            "ring",
            "robustdoor",
            "rope",
            "savepoint",
            "score_collision_cylinder",
            "scrollbldg",
            "shopTV",
            "snowboardjump",
            "spring",
            "spring_twn",
            "switch_collector",
            "tamaire_collision_box",
            "tamaire_collision_cylinder",
            "tamaire_collision_sphere",
            "tarzan",
            "townsgoal",
            "townsman",
            "tpj_runninground",
            "trial_post",
            "turtle",
            "twn_door",
            "twn_flagdoor",
            "twn_gflag_stopplayercollision",
            "twn_hardrock",
            "updownreel",
            "vehicle",
            "venthole",
            "wap_brokensnowball",
            "wap_conifer",
            "wap_door",
            "wap_pathsnowball",
            "wap_searchlight",
            "warpgate",
            "widespring",
            "windroad",
            "windswitch",
            "wvo_battleship",
            "wvo_doorA",
            "wvo_doorB",
            "wvo_jumpsplinter",
            "wvo_orca",
            "wvo_revolvingnet",
            "wvo_waterslider"});
            this.targetObjectBox.Location = new System.Drawing.Point(91, 33);
            this.targetObjectBox.Name = "targetObjectBox";
            this.targetObjectBox.Size = new System.Drawing.Size(274, 21);
            this.targetObjectBox.TabIndex = 44;
            this.targetObjectBox.SelectedIndexChanged += new System.EventHandler(this.TargetObjectBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Target Object:";
            // 
            // searchButton
            // 
            this.searchButton.Enabled = false;
            this.searchButton.Location = new System.Drawing.Point(12, 415);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(353, 23);
            this.searchButton.TabIndex = 46;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // setFilesList
            // 
            this.setFilesList.FormattingEnabled = true;
            this.setFilesList.Location = new System.Drawing.Point(13, 86);
            this.setFilesList.Name = "setFilesList";
            this.setFilesList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.setFilesList.Size = new System.Drawing.Size(352, 316);
            this.setFilesList.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "List Mode:";
            // 
            // modeBox
            // 
            this.modeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeBox.FormattingEnabled = true;
            this.modeBox.Items.AddRange(new object[] {
            "All Parameters",
            "Unique Values of Specific Parameter"});
            this.modeBox.Location = new System.Drawing.Point(74, 60);
            this.modeBox.Name = "modeBox";
            this.modeBox.Size = new System.Drawing.Size(200, 21);
            this.modeBox.TabIndex = 48;
            this.modeBox.SelectedIndexChanged += new System.EventHandler(this.ModeBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(280, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Param No.";
            // 
            // paramBox
            // 
            this.paramBox.Location = new System.Drawing.Point(343, 60);
            this.paramBox.Name = "paramBox";
            this.paramBox.Size = new System.Drawing.Size(22, 20);
            this.paramBox.TabIndex = 51;
            this.paramBox.Text = "0";
            this.paramBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.paramBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.modeBox);
            this.Controls.Add(this.setFilesList);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.targetObjectBox);
            this.Controls.Add(this.setDirButton);
            this.Controls.Add(this.setDirBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "SONIC THE HEDGEHOG (2006) Object Peeker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox setDirBox;
        private System.Windows.Forms.Button setDirButton;
        private System.Windows.Forms.ComboBox targetObjectBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ListBox setFilesList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox modeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox paramBox;
    }
}

