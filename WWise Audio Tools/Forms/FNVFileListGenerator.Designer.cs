namespace WWise_Audio_Tools.Forms
{
    partial class FNVFileListGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FNVFileListGenerator));
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            AboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            InputTextBox = new System.Windows.Forms.TextBox();
            OutputGroupBox = new System.Windows.Forms.GroupBox();
            LoggingTextBox = new System.Windows.Forms.TextBox();
            InputGroupBox = new System.Windows.Forms.GroupBox();
            OutputButton = new System.Windows.Forms.Button();
            OutputTextBox = new System.Windows.Forms.TextBox();
            InputButton = new System.Windows.Forms.Button();
            RunButton = new System.Windows.Forms.Button();
            menuStrip1.SuspendLayout();
            OutputGroupBox.SuspendLayout();
            InputGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { HelpMenu, AboutMenu });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(1071, 33);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "menuStrip1";
            // 
            // AboutMenu
            // 
            AboutMenu.Name = "AboutMenu";
            AboutMenu.Size = new System.Drawing.Size(78, 29);
            AboutMenu.Text = "About";
            AboutMenu.Click += AboutMenu_Click;
            // 
            // HelpMenu
            // 
            HelpMenu.Name = "HelpMenu";
            HelpMenu.Size = new System.Drawing.Size(65, 29);
            HelpMenu.Text = "Help";
            HelpMenu.Click += HelpMenu_Click;
            // 
            // InputTextBox
            // 
            InputTextBox.Location = new System.Drawing.Point(6, 30);
            InputTextBox.Name = "InputTextBox";
            InputTextBox.Size = new System.Drawing.Size(889, 31);
            InputTextBox.TabIndex = 2;
            InputTextBox.Text = "Select a valid list of filenames.";
            InputTextBox.TextChanged += InputTextBox_TextChanged;
            // 
            // OutputGroupBox
            // 
            OutputGroupBox.Controls.Add(LoggingTextBox);
            OutputGroupBox.Location = new System.Drawing.Point(12, 199);
            OutputGroupBox.Name = "OutputGroupBox";
            OutputGroupBox.Size = new System.Drawing.Size(1047, 614);
            OutputGroupBox.TabIndex = 9;
            OutputGroupBox.TabStop = false;
            OutputGroupBox.Text = "Output";
            // 
            // LoggingTextBox
            // 
            LoggingTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            LoggingTextBox.Location = new System.Drawing.Point(3, 27);
            LoggingTextBox.Multiline = true;
            LoggingTextBox.Name = "LoggingTextBox";
            LoggingTextBox.ReadOnly = true;
            LoggingTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            LoggingTextBox.Size = new System.Drawing.Size(1041, 584);
            LoggingTextBox.TabIndex = 0;
            LoggingTextBox.TextChanged += OutputTextBox_TextChanged;
            // 
            // InputGroupBox
            // 
            InputGroupBox.Controls.Add(OutputButton);
            InputGroupBox.Controls.Add(OutputTextBox);
            InputGroupBox.Controls.Add(InputButton);
            InputGroupBox.Controls.Add(InputTextBox);
            InputGroupBox.Location = new System.Drawing.Point(12, 46);
            InputGroupBox.Name = "InputGroupBox";
            InputGroupBox.Size = new System.Drawing.Size(1044, 128);
            InputGroupBox.TabIndex = 11;
            InputGroupBox.TabStop = false;
            InputGroupBox.Text = "Input";
            // 
            // OutputButton
            // 
            OutputButton.Location = new System.Drawing.Point(901, 74);
            OutputButton.Name = "OutputButton";
            OutputButton.Size = new System.Drawing.Size(126, 34);
            OutputButton.TabIndex = 5;
            OutputButton.Text = "Browse";
            OutputButton.UseVisualStyleBackColor = true;
            OutputButton.Click += OutputButton_Click;
            // 
            // OutputTextBox
            // 
            OutputTextBox.Location = new System.Drawing.Point(6, 74);
            OutputTextBox.Name = "OutputTextBox";
            OutputTextBox.Size = new System.Drawing.Size(889, 31);
            OutputTextBox.TabIndex = 4;
            OutputTextBox.Text = "Choose an Output directory.";
            OutputTextBox.TextChanged += OutputTextBox_TextChanged;
            // 
            // InputButton
            // 
            InputButton.Location = new System.Drawing.Point(901, 30);
            InputButton.Name = "InputButton";
            InputButton.Size = new System.Drawing.Size(126, 31);
            InputButton.TabIndex = 3;
            InputButton.Text = "Browse";
            InputButton.UseVisualStyleBackColor = true;
            InputButton.Click += InputButton_Click;
            // 
            // RunButton
            // 
            RunButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            RunButton.Location = new System.Drawing.Point(420, 841);
            RunButton.Name = "RunButton";
            RunButton.Size = new System.Drawing.Size(232, 67);
            RunButton.TabIndex = 12;
            RunButton.Text = "Run";
            RunButton.UseVisualStyleBackColor = true;
            RunButton.Click += RunButton_Click;
            // 
            // FNVFileListGenerator
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1071, 932);
            Controls.Add(RunButton);
            Controls.Add(menuStrip1);
            Controls.Add(OutputGroupBox);
            Controls.Add(InputGroupBox);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "FNVFileListGenerator";
            Text = "FNV FileList Generator";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            OutputGroupBox.ResumeLayout(false);
            OutputGroupBox.PerformLayout();
            InputGroupBox.ResumeLayout(false);
            InputGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AboutMenu;
        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.GroupBox OutputGroupBox;
        private System.Windows.Forms.TextBox LoggingTextBox;
        private System.Windows.Forms.GroupBox InputGroupBox;
        private System.Windows.Forms.Button InputButton;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Button OutputButton;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
    }
}