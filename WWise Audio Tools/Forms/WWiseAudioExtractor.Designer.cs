namespace WWise_Audio_Tools
{
    partial class WWiseAudioExtractor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WWiseAudioExtractor));
            ParametersGroupBox = new System.Windows.Forms.GroupBox();
            SpreadsheetOutputCheckBox = new System.Windows.Forms.CheckBox();
            NoLangCheckBox = new System.Windows.Forms.CheckBox();
            LegacyCheckBox = new System.Windows.Forms.CheckBox();
            KnownEventsBrowse = new System.Windows.Forms.Button();
            KnownEventsTextBox = new System.Windows.Forms.TextBox();
            KnownEventsLabel = new System.Windows.Forms.Label();
            BankedOutputCheckBox = new System.Windows.Forms.CheckBox();
            SplitOutputCheckBox = new System.Windows.Forms.CheckBox();
            ExportOptionsLabel = new System.Windows.Forms.Label();
            WEMExportRadioButton = new System.Windows.Forms.RadioButton();
            OGGExportRadioButton = new System.Windows.Forms.RadioButton();
            WAVExportRadioButton = new System.Windows.Forms.RadioButton();
            FormatOptionsLabel = new System.Windows.Forms.Label();
            InputLabel = new System.Windows.Forms.Label();
            KnownFilenamesLabel = new System.Windows.Forms.Label();
            KnownFilenamesBrowse = new System.Windows.Forms.Button();
            OutputLabel = new System.Windows.Forms.Label();
            InputDirectoryTextBox = new System.Windows.Forms.TextBox();
            OutputDirectoryBrowse = new System.Windows.Forms.Button();
            InputDirectoryBrowse = new System.Windows.Forms.Button();
            KnownFilenamesTextBox = new System.Windows.Forms.TextBox();
            OutputDirectoryTextBox = new System.Windows.Forms.TextBox();
            ProcessingGroupBox = new System.Windows.Forms.GroupBox();
            StatusTextBox = new System.Windows.Forms.RichTextBox();
            CurrentProgressBar = new System.Windows.Forms.ProgressBar();
            TotalProgressBar = new System.Windows.Forms.ProgressBar();
            ExportButton = new System.Windows.Forms.Button();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            DownloadMenu = new System.Windows.Forms.ToolStripMenuItem();
            vGMStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            fFMPegToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            AboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            EleiyasLogoPictureBox = new System.Windows.Forms.PictureBox();
            SpaceFillTextBox = new System.Windows.Forms.TextBox();
            ParametersGroupBox.SuspendLayout();
            ProcessingGroupBox.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)EleiyasLogoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // ParametersGroupBox
            // 
            ParametersGroupBox.Controls.Add(SpreadsheetOutputCheckBox);
            ParametersGroupBox.Controls.Add(NoLangCheckBox);
            ParametersGroupBox.Controls.Add(LegacyCheckBox);
            ParametersGroupBox.Controls.Add(KnownEventsBrowse);
            ParametersGroupBox.Controls.Add(KnownEventsTextBox);
            ParametersGroupBox.Controls.Add(KnownEventsLabel);
            ParametersGroupBox.Controls.Add(BankedOutputCheckBox);
            ParametersGroupBox.Controls.Add(SplitOutputCheckBox);
            ParametersGroupBox.Controls.Add(ExportOptionsLabel);
            ParametersGroupBox.Controls.Add(WEMExportRadioButton);
            ParametersGroupBox.Controls.Add(OGGExportRadioButton);
            ParametersGroupBox.Controls.Add(WAVExportRadioButton);
            ParametersGroupBox.Controls.Add(FormatOptionsLabel);
            ParametersGroupBox.Controls.Add(InputLabel);
            ParametersGroupBox.Controls.Add(KnownFilenamesLabel);
            ParametersGroupBox.Controls.Add(KnownFilenamesBrowse);
            ParametersGroupBox.Controls.Add(OutputLabel);
            ParametersGroupBox.Controls.Add(InputDirectoryTextBox);
            ParametersGroupBox.Controls.Add(OutputDirectoryBrowse);
            ParametersGroupBox.Controls.Add(InputDirectoryBrowse);
            ParametersGroupBox.Controls.Add(KnownFilenamesTextBox);
            ParametersGroupBox.Controls.Add(OutputDirectoryTextBox);
            ParametersGroupBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            ParametersGroupBox.Location = new System.Drawing.Point(7, 50);
            ParametersGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            ParametersGroupBox.Name = "ParametersGroupBox";
            ParametersGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            ParametersGroupBox.Size = new System.Drawing.Size(1131, 362);
            ParametersGroupBox.TabIndex = 0;
            ParametersGroupBox.TabStop = false;
            ParametersGroupBox.Text = "Parameters";
            ParametersGroupBox.Enter += ParametersGroupBox_Enter;
            // 
            // SpreadsheetOutputCheckBox
            // 
            SpreadsheetOutputCheckBox.AutoSize = true;
            SpreadsheetOutputCheckBox.Location = new System.Drawing.Point(674, 307);
            SpreadsheetOutputCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            SpreadsheetOutputCheckBox.Name = "SpreadsheetOutputCheckBox";
            SpreadsheetOutputCheckBox.Size = new System.Drawing.Size(175, 25);
            SpreadsheetOutputCheckBox.TabIndex = 22;
            SpreadsheetOutputCheckBox.Text = "Spreadsheet Output";
            SpreadsheetOutputCheckBox.UseVisualStyleBackColor = true;
            SpreadsheetOutputCheckBox.CheckedChanged += SpreadsheetOutputCheckBox_CheckedChanged;
            // 
            // NoLangCheckBox
            // 
            NoLangCheckBox.AutoSize = true;
            NoLangCheckBox.Location = new System.Drawing.Point(560, 307);
            NoLangCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            NoLangCheckBox.Name = "NoLangCheckBox";
            NoLangCheckBox.Size = new System.Drawing.Size(91, 25);
            NoLangCheckBox.TabIndex = 20;
            NoLangCheckBox.Text = "NoLang";
            NoLangCheckBox.UseVisualStyleBackColor = true;
            NoLangCheckBox.CheckedChanged += NoLangCheckBox_CheckedChanged;
            // 
            // LegacyCheckBox
            // 
            LegacyCheckBox.AutoSize = true;
            LegacyCheckBox.Location = new System.Drawing.Point(457, 308);
            LegacyCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            LegacyCheckBox.Name = "LegacyCheckBox";
            LegacyCheckBox.Size = new System.Drawing.Size(84, 25);
            LegacyCheckBox.TabIndex = 19;
            LegacyCheckBox.Text = "Legacy";
            LegacyCheckBox.UseVisualStyleBackColor = true;
            LegacyCheckBox.CheckedChanged += LegacyCheckBox_CheckedChanged;
            // 
            // KnownEventsBrowse
            // 
            KnownEventsBrowse.Location = new System.Drawing.Point(1006, 187);
            KnownEventsBrowse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            KnownEventsBrowse.Name = "KnownEventsBrowse";
            KnownEventsBrowse.Size = new System.Drawing.Size(119, 33);
            KnownEventsBrowse.TabIndex = 18;
            KnownEventsBrowse.Text = "Browse";
            KnownEventsBrowse.UseVisualStyleBackColor = true;
            KnownEventsBrowse.Click += KnownEventsBrowse_Click;
            // 
            // KnownEventsTextBox
            // 
            KnownEventsTextBox.Location = new System.Drawing.Point(185, 187);
            KnownEventsTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            KnownEventsTextBox.Name = "KnownEventsTextBox";
            KnownEventsTextBox.ReadOnly = true;
            KnownEventsTextBox.Size = new System.Drawing.Size(781, 29);
            KnownEventsTextBox.TabIndex = 17;
            KnownEventsTextBox.Text = "No Known_Events TSV Selected.";
            KnownEventsTextBox.TextChanged += KnownEventsTextBox_TextChanged;
            // 
            // KnownEventsLabel
            // 
            KnownEventsLabel.AutoSize = true;
            KnownEventsLabel.Location = new System.Drawing.Point(6, 190);
            KnownEventsLabel.Name = "KnownEventsLabel";
            KnownEventsLabel.Size = new System.Drawing.Size(113, 21);
            KnownEventsLabel.TabIndex = 16;
            KnownEventsLabel.Text = "Known_Events:";
            // 
            // BankedOutputCheckBox
            // 
            BankedOutputCheckBox.AutoSize = true;
            BankedOutputCheckBox.Location = new System.Drawing.Point(311, 307);
            BankedOutputCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            BankedOutputCheckBox.Name = "BankedOutputCheckBox";
            BankedOutputCheckBox.Size = new System.Drawing.Size(140, 25);
            BankedOutputCheckBox.TabIndex = 15;
            BankedOutputCheckBox.Text = "Banked Output";
            BankedOutputCheckBox.UseVisualStyleBackColor = true;
            BankedOutputCheckBox.CheckedChanged += BankedOutputCheckBox_CheckedChanged;
            // 
            // SplitOutputCheckBox
            // 
            SplitOutputCheckBox.AutoSize = true;
            SplitOutputCheckBox.Location = new System.Drawing.Point(185, 307);
            SplitOutputCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            SplitOutputCheckBox.Name = "SplitOutputCheckBox";
            SplitOutputCheckBox.Size = new System.Drawing.Size(120, 25);
            SplitOutputCheckBox.TabIndex = 14;
            SplitOutputCheckBox.Text = "Split Output";
            SplitOutputCheckBox.UseVisualStyleBackColor = true;
            SplitOutputCheckBox.CheckedChanged += SplitOutputCheckBox_CheckedChanged;
            // 
            // ExportOptionsLabel
            // 
            ExportOptionsLabel.AutoSize = true;
            ExportOptionsLabel.Location = new System.Drawing.Point(6, 309);
            ExportOptionsLabel.Name = "ExportOptionsLabel";
            ExportOptionsLabel.Size = new System.Drawing.Size(116, 21);
            ExportOptionsLabel.TabIndex = 13;
            ExportOptionsLabel.Text = "Export Options:";
            // 
            // WEMExportRadioButton
            // 
            WEMExportRadioButton.AutoSize = true;
            WEMExportRadioButton.Location = new System.Drawing.Point(185, 261);
            WEMExportRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            WEMExportRadioButton.Name = "WEMExportRadioButton";
            WEMExportRadioButton.Size = new System.Drawing.Size(72, 25);
            WEMExportRadioButton.TabIndex = 12;
            WEMExportRadioButton.Text = "WEM";
            WEMExportRadioButton.UseVisualStyleBackColor = true;
            WEMExportRadioButton.CheckedChanged += WEMExportRadioButton_CheckedChanged;
            // 
            // OGGExportRadioButton
            // 
            OGGExportRadioButton.AutoSize = true;
            OGGExportRadioButton.Location = new System.Drawing.Point(337, 261);
            OGGExportRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            OGGExportRadioButton.Name = "OGGExportRadioButton";
            OGGExportRadioButton.Size = new System.Drawing.Size(69, 25);
            OGGExportRadioButton.TabIndex = 11;
            OGGExportRadioButton.Text = "OGG";
            OGGExportRadioButton.UseVisualStyleBackColor = true;
            OGGExportRadioButton.CheckedChanged += OGGExportRadioButton_CheckedChanged;
            // 
            // WAVExportRadioButton
            // 
            WAVExportRadioButton.AutoSize = true;
            WAVExportRadioButton.Location = new System.Drawing.Point(263, 261);
            WAVExportRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            WAVExportRadioButton.Name = "WAVExportRadioButton";
            WAVExportRadioButton.Size = new System.Drawing.Size(68, 25);
            WAVExportRadioButton.TabIndex = 10;
            WAVExportRadioButton.Text = "WAV";
            WAVExportRadioButton.UseVisualStyleBackColor = true;
            WAVExportRadioButton.CheckedChanged += WAVExportRadioButton_CheckedChanged;
            // 
            // FormatOptionsLabel
            // 
            FormatOptionsLabel.AutoSize = true;
            FormatOptionsLabel.Location = new System.Drawing.Point(6, 263);
            FormatOptionsLabel.Name = "FormatOptionsLabel";
            FormatOptionsLabel.Size = new System.Drawing.Size(122, 21);
            FormatOptionsLabel.TabIndex = 9;
            FormatOptionsLabel.Text = "Format Options:";
            // 
            // InputLabel
            // 
            InputLabel.AutoSize = true;
            InputLabel.Location = new System.Drawing.Point(6, 39);
            InputLabel.Name = "InputLabel";
            InputLabel.Size = new System.Drawing.Size(84, 21);
            InputLabel.TabIndex = 3;
            InputLabel.Text = "Input Files:";
            // 
            // KnownFilenamesLabel
            // 
            KnownFilenamesLabel.AutoSize = true;
            KnownFilenamesLabel.Location = new System.Drawing.Point(6, 137);
            KnownFilenamesLabel.Name = "KnownFilenamesLabel";
            KnownFilenamesLabel.Size = new System.Drawing.Size(138, 21);
            KnownFilenamesLabel.TabIndex = 5;
            KnownFilenamesLabel.Text = "Known_Filenames:";
            // 
            // KnownFilenamesBrowse
            // 
            KnownFilenamesBrowse.Location = new System.Drawing.Point(1006, 133);
            KnownFilenamesBrowse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            KnownFilenamesBrowse.Name = "KnownFilenamesBrowse";
            KnownFilenamesBrowse.Size = new System.Drawing.Size(119, 33);
            KnownFilenamesBrowse.TabIndex = 8;
            KnownFilenamesBrowse.Text = "Browse";
            KnownFilenamesBrowse.UseVisualStyleBackColor = true;
            KnownFilenamesBrowse.Click += KnownFilenamesBrowse_Click;
            // 
            // OutputLabel
            // 
            OutputLabel.AutoSize = true;
            OutputLabel.Location = new System.Drawing.Point(6, 84);
            OutputLabel.Name = "OutputLabel";
            OutputLabel.Size = new System.Drawing.Size(130, 21);
            OutputLabel.TabIndex = 4;
            OutputLabel.Text = "Output Directory:";
            // 
            // InputDirectoryTextBox
            // 
            InputDirectoryTextBox.Location = new System.Drawing.Point(185, 34);
            InputDirectoryTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            InputDirectoryTextBox.Name = "InputDirectoryTextBox";
            InputDirectoryTextBox.ReadOnly = true;
            InputDirectoryTextBox.Size = new System.Drawing.Size(781, 29);
            InputDirectoryTextBox.TabIndex = 0;
            InputDirectoryTextBox.Text = "No Input Files Selected.";
            InputDirectoryTextBox.TextChanged += InputDirectoryTextBox_TextChanged;
            // 
            // OutputDirectoryBrowse
            // 
            OutputDirectoryBrowse.Location = new System.Drawing.Point(1006, 84);
            OutputDirectoryBrowse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            OutputDirectoryBrowse.Name = "OutputDirectoryBrowse";
            OutputDirectoryBrowse.Size = new System.Drawing.Size(119, 33);
            OutputDirectoryBrowse.TabIndex = 7;
            OutputDirectoryBrowse.Text = "Browse";
            OutputDirectoryBrowse.UseVisualStyleBackColor = true;
            OutputDirectoryBrowse.Click += OutputDirectoryBrowse_Click;
            // 
            // InputDirectoryBrowse
            // 
            InputDirectoryBrowse.Location = new System.Drawing.Point(1006, 34);
            InputDirectoryBrowse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            InputDirectoryBrowse.Name = "InputDirectoryBrowse";
            InputDirectoryBrowse.Size = new System.Drawing.Size(119, 33);
            InputDirectoryBrowse.TabIndex = 6;
            InputDirectoryBrowse.Text = "Browse";
            InputDirectoryBrowse.UseVisualStyleBackColor = true;
            InputDirectoryBrowse.Click += InputDirectoryBrowse_Click;
            // 
            // KnownFilenamesTextBox
            // 
            KnownFilenamesTextBox.Location = new System.Drawing.Point(185, 133);
            KnownFilenamesTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            KnownFilenamesTextBox.Name = "KnownFilenamesTextBox";
            KnownFilenamesTextBox.ReadOnly = true;
            KnownFilenamesTextBox.Size = new System.Drawing.Size(781, 29);
            KnownFilenamesTextBox.TabIndex = 2;
            KnownFilenamesTextBox.Text = "No Known_Filenames TSV Selected.";
            KnownFilenamesTextBox.TextChanged += KnownFilenamesTextBox_TextChanged;
            // 
            // OutputDirectoryTextBox
            // 
            OutputDirectoryTextBox.Location = new System.Drawing.Point(185, 84);
            OutputDirectoryTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            OutputDirectoryTextBox.Name = "OutputDirectoryTextBox";
            OutputDirectoryTextBox.ReadOnly = true;
            OutputDirectoryTextBox.Size = new System.Drawing.Size(781, 29);
            OutputDirectoryTextBox.TabIndex = 1;
            OutputDirectoryTextBox.Text = "No Output Directory Selected.";
            OutputDirectoryTextBox.TextChanged += OutputDirectoryTextBox_TextChanged;
            // 
            // ProcessingGroupBox
            // 
            ProcessingGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ProcessingGroupBox.Controls.Add(StatusTextBox);
            ProcessingGroupBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            ProcessingGroupBox.Location = new System.Drawing.Point(13, 433);
            ProcessingGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            ProcessingGroupBox.Name = "ProcessingGroupBox";
            ProcessingGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            ProcessingGroupBox.Size = new System.Drawing.Size(1371, 567);
            ProcessingGroupBox.TabIndex = 10;
            ProcessingGroupBox.TabStop = false;
            ProcessingGroupBox.Text = "Processing";
            ProcessingGroupBox.Enter += ProcessingGroupBox_Enter;
            // 
            // StatusTextBox
            // 
            StatusTextBox.BackColor = System.Drawing.SystemColors.Control;
            StatusTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            StatusTextBox.Font = new System.Drawing.Font("Lucida Console", 8F);
            StatusTextBox.HideSelection = false;
            StatusTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            StatusTextBox.Location = new System.Drawing.Point(3, 24);
            StatusTextBox.Margin = new System.Windows.Forms.Padding(4);
            StatusTextBox.Name = "StatusTextBox";
            StatusTextBox.ReadOnly = true;
            StatusTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            StatusTextBox.Size = new System.Drawing.Size(1365, 541);
            StatusTextBox.TabIndex = 0;
            StatusTextBox.TabStop = false;
            StatusTextBox.Text = "";
            StatusTextBox.TextChanged += StatusTextBox_TextChanged;
            // 
            // CurrentProgressBar
            // 
            CurrentProgressBar.Location = new System.Drawing.Point(12, 1023);
            CurrentProgressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            CurrentProgressBar.Name = "CurrentProgressBar";
            CurrentProgressBar.Size = new System.Drawing.Size(1242, 38);
            CurrentProgressBar.TabIndex = 11;
            CurrentProgressBar.Click += CurrentProgressBar_Click;
            // 
            // TotalProgressBar
            // 
            TotalProgressBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TotalProgressBar.Location = new System.Drawing.Point(12, 1077);
            TotalProgressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            TotalProgressBar.Name = "TotalProgressBar";
            TotalProgressBar.Size = new System.Drawing.Size(1242, 38);
            TotalProgressBar.TabIndex = 12;
            TotalProgressBar.Click += TotalProgressBar_Click;
            // 
            // ExportButton
            // 
            ExportButton.Font = new System.Drawing.Font("Segoe UI", 8F);
            ExportButton.Location = new System.Drawing.Point(1260, 1023);
            ExportButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new System.Drawing.Size(126, 92);
            ExportButton.TabIndex = 13;
            ExportButton.Text = "Export";
            ExportButton.UseVisualStyleBackColor = true;
            ExportButton.Click += ExportButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { DownloadMenu, HelpMenu, AboutMenu });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(1394, 33);
            menuStrip1.TabIndex = 14;
            menuStrip1.Text = "menuStrip1";
            // 
            // DownloadMenu
            // 
            DownloadMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { vGMStreamToolStripMenuItem, fFMPegToolStripMenuItem });
            DownloadMenu.Name = "DownloadMenu";
            DownloadMenu.Size = new System.Drawing.Size(118, 29);
            DownloadMenu.Text = "Downloads";
            // 
            // vGMStreamToolStripMenuItem
            // 
            vGMStreamToolStripMenuItem.Name = "vGMStreamToolStripMenuItem";
            vGMStreamToolStripMenuItem.Size = new System.Drawing.Size(208, 34);
            vGMStreamToolStripMenuItem.Text = "VGMStream";
            vGMStreamToolStripMenuItem.Click += vGMStreamToolStripMenuItem_Click;
            // 
            // fFMPegToolStripMenuItem
            // 
            fFMPegToolStripMenuItem.Name = "fFMPegToolStripMenuItem";
            fFMPegToolStripMenuItem.Size = new System.Drawing.Size(208, 34);
            fFMPegToolStripMenuItem.Text = "FFMPeg";
            fFMPegToolStripMenuItem.Click += fFMPegToolStripMenuItem_Click;
            // 
            // HelpMenu
            // 
            HelpMenu.Name = "HelpMenu";
            HelpMenu.Size = new System.Drawing.Size(65, 29);
            HelpMenu.Text = "Help";
            HelpMenu.Click += HelpMenu_Click;
            // 
            // AboutMenu
            // 
            AboutMenu.Name = "AboutMenu";
            AboutMenu.Size = new System.Drawing.Size(78, 29);
            AboutMenu.Text = "About";
            AboutMenu.Click += AboutMenu_Click;
            // 
            // EleiyasLogoPictureBox
            // 
            EleiyasLogoPictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            EleiyasLogoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            EleiyasLogoPictureBox.Image = (System.Drawing.Image)resources.GetObject("EleiyasLogoPictureBox.Image");
            EleiyasLogoPictureBox.Location = new System.Drawing.Point(1157, 61);
            EleiyasLogoPictureBox.Name = "EleiyasLogoPictureBox";
            EleiyasLogoPictureBox.Size = new System.Drawing.Size(224, 351);
            EleiyasLogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            EleiyasLogoPictureBox.TabIndex = 15;
            EleiyasLogoPictureBox.TabStop = false;
            // 
            // SpaceFillTextBox
            // 
            SpaceFillTextBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            SpaceFillTextBox.Font = new System.Drawing.Font("Visitor TT1 BRK", 9.999999F);
            SpaceFillTextBox.Location = new System.Drawing.Point(1167, 370);
            SpaceFillTextBox.Multiline = true;
            SpaceFillTextBox.Name = "SpaceFillTextBox";
            SpaceFillTextBox.ReadOnly = true;
            SpaceFillTextBox.Size = new System.Drawing.Size(201, 29);
            SpaceFillTextBox.TabIndex = 16;
            SpaceFillTextBox.Text = "Coded by Eleiyas";
            SpaceFillTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // WWiseAudioExtractor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(1394, 1126);
            Controls.Add(SpaceFillTextBox);
            Controls.Add(EleiyasLogoPictureBox);
            Controls.Add(TotalProgressBar);
            Controls.Add(ExportButton);
            Controls.Add(CurrentProgressBar);
            Controls.Add(ProcessingGroupBox);
            Controls.Add(ParametersGroupBox);
            Controls.Add(menuStrip1);
            Font = new System.Drawing.Font("Segoe UI", 8F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "WWiseAudioExtractor";
            Text = "WWise Audio Extractor";
            Load += WWiseAudioExtractorUI_Load;
            ParametersGroupBox.ResumeLayout(false);
            ParametersGroupBox.PerformLayout();
            ProcessingGroupBox.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)EleiyasLogoPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox ParametersGroupBox;

        private System.Windows.Forms.Label InputLabel;
        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.Label KnownFilenamesLabel;
        private System.Windows.Forms.Label KnownEventsLabel;

        private System.Windows.Forms.TextBox InputDirectoryTextBox;
        private System.Windows.Forms.TextBox OutputDirectoryTextBox;
        private System.Windows.Forms.TextBox KnownFilenamesTextBox;
        private System.Windows.Forms.TextBox KnownEventsTextBox;

        private System.Windows.Forms.Button InputDirectoryBrowse;
        private System.Windows.Forms.Button OutputDirectoryBrowse;
        private System.Windows.Forms.Button KnownFilenamesBrowse;
        private System.Windows.Forms.Button KnownEventsBrowse;

        private System.Windows.Forms.GroupBox ProcessingGroupBox;

        private System.Windows.Forms.Label FormatOptionsLabel;
        private System.Windows.Forms.Label ExportOptionsLabel;

        private System.Windows.Forms.RadioButton WEMExportRadioButton;
        private System.Windows.Forms.RadioButton WAVExportRadioButton;
        private System.Windows.Forms.RadioButton OGGExportRadioButton;

        private System.Windows.Forms.CheckBox BankedOutputCheckBox;
        private System.Windows.Forms.CheckBox SplitOutputCheckBox;
        private System.Windows.Forms.CheckBox LegacyCheckBox;
        private System.Windows.Forms.CheckBox NoLangCheckBox;
        private System.Windows.Forms.CheckBox SpreadsheetOutputCheckBox;

        internal System.Windows.Forms.RichTextBox StatusTextBox;
        private System.Windows.Forms.ProgressBar CurrentProgressBar;
        private System.Windows.Forms.ProgressBar TotalProgressBar;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DownloadMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripMenuItem vGMStreamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fFMPegToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenu;
        private System.Windows.Forms.PictureBox EleiyasLogoPictureBox;
        private System.Windows.Forms.TextBox SpaceFillTextBox;
    }
}

