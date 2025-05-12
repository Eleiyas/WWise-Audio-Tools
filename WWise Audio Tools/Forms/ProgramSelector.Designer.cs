namespace WWise_Audio_Tools.Forms
{
    partial class ProgramSelector
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
            System.Windows.Forms.TextBox AuthorTextBox;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramSelector));
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            CreditsMenu = new System.Windows.Forms.ToolStripMenuItem();
            WWiseAudioExtractorButton = new System.Windows.Forms.Button();
            FNVHasherButton = new System.Windows.Forms.Button();
            FNVFileListGenerator = new System.Windows.Forms.Button();
            VoiceItemsCollator = new System.Windows.Forms.Button();
            AuthorTextBox = new System.Windows.Forms.TextBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // AuthorTextBox
            // 
            AuthorTextBox.Enabled = false;
            AuthorTextBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            AuthorTextBox.Location = new System.Drawing.Point(282, 319);
            AuthorTextBox.Name = "AuthorTextBox";
            AuthorTextBox.ReadOnly = true;
            AuthorTextBox.ShortcutsEnabled = false;
            AuthorTextBox.Size = new System.Drawing.Size(150, 31);
            AuthorTextBox.TabIndex = 5;
            AuthorTextBox.TabStop = false;
            AuthorTextBox.Text = "Coded by Eleiyas";
            AuthorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { CreditsMenu });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(432, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // CreditsMenu
            // 
            CreditsMenu.Name = "CreditsMenu";
            CreditsMenu.Size = new System.Drawing.Size(83, 29);
            CreditsMenu.Text = "Credits";
            CreditsMenu.Click += CreditsMenu_Click;
            // 
            // WWiseAudioExtractorButton
            // 
            WWiseAudioExtractorButton.Location = new System.Drawing.Point(98, 55);
            WWiseAudioExtractorButton.Name = "WWiseAudioExtractorButton";
            WWiseAudioExtractorButton.Size = new System.Drawing.Size(220, 60);
            WWiseAudioExtractorButton.TabIndex = 1;
            WWiseAudioExtractorButton.Text = "WWise Audio Extractor";
            WWiseAudioExtractorButton.UseVisualStyleBackColor = true;
            WWiseAudioExtractorButton.Click += WWiseAudioExtractorButton_Click;
            // 
            // FNVHasherButton
            // 
            FNVHasherButton.Location = new System.Drawing.Point(98, 121);
            FNVHasherButton.Name = "FNVHasherButton";
            FNVHasherButton.Size = new System.Drawing.Size(220, 60);
            FNVHasherButton.TabIndex = 2;
            FNVHasherButton.Text = "FNV Hasher";
            FNVHasherButton.UseVisualStyleBackColor = true;
            FNVHasherButton.Click += FNVHasherButton_Click;
            // 
            // FNVFileListGenerator
            // 
            FNVFileListGenerator.Location = new System.Drawing.Point(98, 253);
            FNVFileListGenerator.Name = "FNVFileListGenerator";
            FNVFileListGenerator.Size = new System.Drawing.Size(220, 60);
            FNVFileListGenerator.TabIndex = 3;
            FNVFileListGenerator.Text = "FNV FileList Generator";
            FNVFileListGenerator.UseVisualStyleBackColor = true;
            FNVFileListGenerator.Click += FNVFileListGenerator_Click;
            // 
            // VoiceItemsCollator
            // 
            VoiceItemsCollator.Location = new System.Drawing.Point(98, 187);
            VoiceItemsCollator.Name = "VoiceItemsCollator";
            VoiceItemsCollator.Size = new System.Drawing.Size(220, 60);
            VoiceItemsCollator.TabIndex = 4;
            VoiceItemsCollator.Text = "VoiceItems Collator";
            VoiceItemsCollator.UseVisualStyleBackColor = true;
            VoiceItemsCollator.Click += VoiceItemsCollator_Click;
            // 
            // ProgramSelector
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(432, 347);
            Controls.Add(AuthorTextBox);
            Controls.Add(VoiceItemsCollator);
            Controls.Add(FNVFileListGenerator);
            Controls.Add(FNVHasherButton);
            Controls.Add(WWiseAudioExtractorButton);
            Controls.Add(menuStrip1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "ProgramSelector";
            Text = "WWise Audio Tools";
            Load += ProgramSelector_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CreditsMenu;
        private System.Windows.Forms.Button WWiseAudioExtractorButton;
        private System.Windows.Forms.Button FNVHasherButton;
        private System.Windows.Forms.Button FNVFileListGenerator;
        private System.Windows.Forms.Button VoiceItemsCollator;
        private System.Windows.Forms.TextBox AuthorTextBox;
    }
}