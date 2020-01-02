namespace MP3_Randomizer_2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cmsTextbox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPath = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMp3Files = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRenamed = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.grpQuickSettings = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbShuffleMode = new System.Windows.Forms.ComboBox();
            this.btnCheckUpdates = new System.Windows.Forms.Button();
            this.btnRearrangeNow = new System.Windows.Forms.Button();
            this.btnResetDisable = new System.Windows.Forms.Button();
            this.chkautoReOrder = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblCurrentGame = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnForceReload = new System.Windows.Forms.Button();
            this.rightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewFileDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prgProgress = new System.Windows.Forms.ProgressBar();
            this.cmsTextbox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.grpQuickSettings.SuspendLayout();
            this.panel4.SuspendLayout();
            this.rightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.ContextMenuStrip = this.cmsTextbox;
            this.txtPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(0, 0);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(728, 26);
            this.txtPath.TabIndex = 0;
            // 
            // cmsTextbox
            // 
            this.cmsTextbox.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsTextbox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.cmsTextbox.Name = "cmsTextbox";
            this.cmsTextbox.Size = new System.Drawing.Size(165, 34);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(164, 30);
            this.testToolStripMenuItem.Text = "Clear path";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // btnPath
            // 
            this.btnPath.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPath.Location = new System.Drawing.Point(604, 0);
            this.btnPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(124, 34);
            this.btnPath.TabIndex = 2;
            this.btnPath.Text = "&Select path";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPath);
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 34);
            this.panel1.TabIndex = 3;
            // 
            // lblMp3Files
            // 
            this.lblMp3Files.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblMp3Files.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblMp3Files.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMp3Files.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMp3Files.Location = new System.Drawing.Point(0, 34);
            this.lblMp3Files.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMp3Files.Name = "lblMp3Files";
            this.lblMp3Files.Size = new System.Drawing.Size(141, 375);
            this.lblMp3Files.TabIndex = 4;
            this.lblMp3Files.Text = "0";
            this.lblMp3Files.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(4, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 185);
            this.label2.TabIndex = 5;
            this.label2.Text = "*.MP3 or *.LNK files were found";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lblRenamed);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(141, 34);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(156, 375);
            this.panel3.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(4, 69);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 185);
            this.label4.TabIndex = 10;
            this.label4.Text = "*.MP3 or *.LNK files were renamed";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenamed
            // 
            this.lblRenamed.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblRenamed.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblRenamed.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRenamed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenamed.Location = new System.Drawing.Point(0, 0);
            this.lblRenamed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenamed.Name = "lblRenamed";
            this.lblRenamed.Size = new System.Drawing.Size(150, 375);
            this.lblRenamed.TabIndex = 9;
            this.lblRenamed.Text = "0";
            this.lblRenamed.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblRenamed.Click += new System.EventHandler(this.lblRenamed_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblStatus);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(297, 34);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(431, 375);
            this.panel2.TabIndex = 10;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(0, 42);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(379, 122);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "If you see this, the application probably froze.\r\nRestart to fix this.";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOk.Location = new System.Drawing.Point(379, 42);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(52, 122);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.grpQuickSettings);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 164);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(431, 211);
            this.panel5.TabIndex = 1;
            // 
            // grpQuickSettings
            // 
            this.grpQuickSettings.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grpQuickSettings.Controls.Add(this.label1);
            this.grpQuickSettings.Controls.Add(this.cmbShuffleMode);
            this.grpQuickSettings.Controls.Add(this.btnCheckUpdates);
            this.grpQuickSettings.Controls.Add(this.btnRearrangeNow);
            this.grpQuickSettings.Controls.Add(this.btnResetDisable);
            this.grpQuickSettings.Controls.Add(this.chkautoReOrder);
            this.grpQuickSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpQuickSettings.Location = new System.Drawing.Point(0, 0);
            this.grpQuickSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpQuickSettings.Name = "grpQuickSettings";
            this.grpQuickSettings.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpQuickSettings.Size = new System.Drawing.Size(431, 211);
            this.grpQuickSettings.TabIndex = 0;
            this.grpQuickSettings.TabStop = false;
            this.grpQuickSettings.Text = "Quick settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Shuffle mode";
            // 
            // cmbShuffleMode
            // 
            this.cmbShuffleMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbShuffleMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShuffleMode.FormattingEnabled = true;
            this.cmbShuffleMode.Items.AddRange(new object[] {
            "Randomize all songs",
            "Pick a random song to start playing from",
            "Randomize the first song only",
            "Randomize everything but the first song"});
            this.cmbShuffleMode.Location = new System.Drawing.Point(123, 55);
            this.cmbShuffleMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbShuffleMode.Name = "cmbShuffleMode";
            this.cmbShuffleMode.Size = new System.Drawing.Size(297, 28);
            this.cmbShuffleMode.TabIndex = 4;
            this.cmbShuffleMode.SelectedIndexChanged += new System.EventHandler(this.cmbShuffleMode_SelectedIndexChanged);
            // 
            // btnCheckUpdates
            // 
            this.btnCheckUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckUpdates.Location = new System.Drawing.Point(10, 169);
            this.btnCheckUpdates.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCheckUpdates.Name = "btnCheckUpdates";
            this.btnCheckUpdates.Size = new System.Drawing.Size(417, 35);
            this.btnCheckUpdates.TabIndex = 3;
            this.btnCheckUpdates.Text = "&Check for updates...";
            this.btnCheckUpdates.UseVisualStyleBackColor = true;
            this.btnCheckUpdates.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnRearrangeNow
            // 
            this.btnRearrangeNow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRearrangeNow.Location = new System.Drawing.Point(10, 92);
            this.btnRearrangeNow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRearrangeNow.Name = "btnRearrangeNow";
            this.btnRearrangeNow.Size = new System.Drawing.Size(417, 35);
            this.btnRearrangeNow.TabIndex = 2;
            this.btnRearrangeNow.Text = "&Rearrange songs right now";
            this.btnRearrangeNow.UseVisualStyleBackColor = true;
            this.btnRearrangeNow.Click += new System.EventHandler(this.btnRearrangeNow_Click);
            // 
            // btnResetDisable
            // 
            this.btnResetDisable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetDisable.Location = new System.Drawing.Point(10, 131);
            this.btnResetDisable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnResetDisable.Name = "btnResetDisable";
            this.btnResetDisable.Size = new System.Drawing.Size(417, 35);
            this.btnResetDisable.TabIndex = 1;
            this.btnResetDisable.Text = "Recover &songs";
            this.btnResetDisable.UseVisualStyleBackColor = true;
            this.btnResetDisable.Click += new System.EventHandler(this.btnResetDisable_Click);
            // 
            // chkautoReOrder
            // 
            this.chkautoReOrder.AutoSize = true;
            this.chkautoReOrder.Location = new System.Drawing.Point(10, 31);
            this.chkautoReOrder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkautoReOrder.Name = "chkautoReOrder";
            this.chkautoReOrder.Size = new System.Drawing.Size(334, 24);
            this.chkautoReOrder.TabIndex = 0;
            this.chkautoReOrder.Text = "Check for game and re-order &automatically";
            this.chkautoReOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkautoReOrder.UseVisualStyleBackColor = true;
            this.chkautoReOrder.CheckedChanged += new System.EventHandler(this.chkautoReOrder_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblCurrentGame);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(431, 42);
            this.panel4.TabIndex = 0;
            // 
            // lblCurrentGame
            // 
            this.lblCurrentGame.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblCurrentGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentGame.ForeColor = System.Drawing.Color.Orange;
            this.lblCurrentGame.Location = new System.Drawing.Point(0, 0);
            this.lblCurrentGame.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentGame.Name = "lblCurrentGame";
            this.lblCurrentGame.Size = new System.Drawing.Size(431, 42);
            this.lblCurrentGame.TabIndex = 0;
            this.lblCurrentGame.Text = "Detecting...";
            this.lblCurrentGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(3, 365);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(265, 40);
            this.label5.TabIndex = 13;
            this.label5.Text = "Backup your songs before using this\r\nRight click for more options\r\n";
            // 
            // btnForceReload
            // 
            this.btnForceReload.Location = new System.Drawing.Point(8, 229);
            this.btnForceReload.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnForceReload.Name = "btnForceReload";
            this.btnForceReload.Size = new System.Drawing.Size(112, 35);
            this.btnForceReload.TabIndex = 14;
            this.btnForceReload.Text = "&Force reload";
            this.btnForceReload.UseVisualStyleBackColor = true;
            this.btnForceReload.Visible = false;
            this.btnForceReload.Click += new System.EventHandler(this.btnForceReload_Click);
            // 
            // rightClick
            // 
            this.rightClick.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.rightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewFileDetailsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.rightClick.Name = "rightClick";
            this.rightClick.Size = new System.Drawing.Size(206, 64);
            this.rightClick.Opening += new System.ComponentModel.CancelEventHandler(this.rightClick_Opening);
            // 
            // viewFileDetailsToolStripMenuItem
            // 
            this.viewFileDetailsToolStripMenuItem.Name = "viewFileDetailsToolStripMenuItem";
            this.viewFileDetailsToolStripMenuItem.Size = new System.Drawing.Size(205, 30);
            this.viewFileDetailsToolStripMenuItem.Text = "View &file details";
            this.viewFileDetailsToolStripMenuItem.Click += new System.EventHandler(this.viewFileDetailsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(205, 30);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // prgProgress
            // 
            this.prgProgress.Location = new System.Drawing.Point(34, 291);
            this.prgProgress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.prgProgress.Name = "prgProgress";
            this.prgProgress.Size = new System.Drawing.Size(219, 29);
            this.prgProgress.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 409);
            this.ContextMenuStrip = this.rightClick;
            this.Controls.Add(this.prgProgress);
            this.Controls.Add(this.btnForceReload);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMp3Files);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GTA: MP3 Randomizer 2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.cmsTextbox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.grpQuickSettings.ResumeLayout(false);
            this.grpQuickSettings.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.rightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMp3Files;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblRenamed;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblCurrentGame;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox grpQuickSettings;
        private System.Windows.Forms.Button btnCheckUpdates;
        private System.Windows.Forms.Button btnRearrangeNow;
        private System.Windows.Forms.Button btnResetDisable;
        private System.Windows.Forms.CheckBox chkautoReOrder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbShuffleMode;
        private System.Windows.Forms.ContextMenuStrip cmsTextbox;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnForceReload;
        private System.Windows.Forms.ContextMenuStrip rightClick;
        private System.Windows.Forms.ToolStripMenuItem viewFileDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ProgressBar prgProgress;
        private System.Windows.Forms.Button btnOk;
    }
}

