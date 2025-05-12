namespace WWise_Audio_Tools.Forms
{
    partial class VoiceItemsCollator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoiceItemsCollator));
            IOGroupBox = new System.Windows.Forms.GroupBox();
            OutputBrowseButton = new System.Windows.Forms.Button();
            InputBrowseButton = new System.Windows.Forms.Button();
            OutputTextBox = new System.Windows.Forms.TextBox();
            InputTextBox = new System.Windows.Forms.TextBox();
            RunButton = new System.Windows.Forms.Button();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            PrefixSelector = new System.Windows.Forms.ToolStripComboBox();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            IOGroupBox.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // IOGroupBox
            // 
            IOGroupBox.Controls.Add(OutputBrowseButton);
            IOGroupBox.Controls.Add(InputBrowseButton);
            IOGroupBox.Controls.Add(OutputTextBox);
            IOGroupBox.Controls.Add(InputTextBox);
            IOGroupBox.Location = new System.Drawing.Point(12, 51);
            IOGroupBox.Name = "IOGroupBox";
            IOGroupBox.Size = new System.Drawing.Size(776, 130);
            IOGroupBox.TabIndex = 0;
            IOGroupBox.TabStop = false;
            IOGroupBox.Text = "IOGroupBox";
            // 
            // OutputBrowseButton
            // 
            OutputBrowseButton.Location = new System.Drawing.Point(658, 82);
            OutputBrowseButton.Name = "OutputBrowseButton";
            OutputBrowseButton.Size = new System.Drawing.Size(112, 34);
            OutputBrowseButton.TabIndex = 3;
            OutputBrowseButton.Text = "Browse";
            OutputBrowseButton.UseVisualStyleBackColor = true;
            OutputBrowseButton.Click += OutputBrowseButton_Click;
            // 
            // InputBrowseButton
            // 
            InputBrowseButton.Location = new System.Drawing.Point(658, 34);
            InputBrowseButton.Name = "InputBrowseButton";
            InputBrowseButton.Size = new System.Drawing.Size(112, 33);
            InputBrowseButton.TabIndex = 2;
            InputBrowseButton.Text = "Browse";
            InputBrowseButton.UseVisualStyleBackColor = true;
            InputBrowseButton.Click += InputBrowseButton_Click;
            // 
            // OutputTextBox
            // 
            OutputTextBox.Location = new System.Drawing.Point(20, 82);
            OutputTextBox.Name = "OutputTextBox";
            OutputTextBox.Size = new System.Drawing.Size(622, 31);
            OutputTextBox.TabIndex = 1;
            OutputTextBox.Text = "Choose an Output directory.";
            OutputTextBox.TextChanged += OutputTextBox_TextChanged;
            // 
            // InputTextBox
            // 
            InputTextBox.Location = new System.Drawing.Point(17, 36);
            InputTextBox.Name = "InputTextBox";
            InputTextBox.Size = new System.Drawing.Size(625, 31);
            InputTextBox.TabIndex = 0;
            InputTextBox.Text = "Browse to \"BinOutput/Voice/Items\".";
            InputTextBox.TextChanged += InputTextBox_TextChanged;
            // 
            // RunButton
            // 
            RunButton.Location = new System.Drawing.Point(294, 187);
            RunButton.Name = "RunButton";
            RunButton.Size = new System.Drawing.Size(207, 79);
            RunButton.TabIndex = 1;
            RunButton.Text = "Run";
            RunButton.UseVisualStyleBackColor = true;
            RunButton.Click += RunButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { PrefixSelector, aboutToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(800, 37);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // PrefixSelector
            // 
            PrefixSelector.Items.AddRange(new object[] { "ALL", "english(us)", "chinese", "japanese", "korean" });
            PrefixSelector.Name = "PrefixSelector";
            PrefixSelector.Size = new System.Drawing.Size(121, 33);
            PrefixSelector.Tag = "Prefix";
            PrefixSelector.Text = "Prefix";
            PrefixSelector.ToolTipText = "Prefix";
            PrefixSelector.Click += PrefixSelector_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(78, 33);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // VoiceItemsCollator
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 279);
            Controls.Add(RunButton);
            Controls.Add(IOGroupBox);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "VoiceItemsCollator";
            Text = "VoiceItemsCollator";
            IOGroupBox.ResumeLayout(false);
            IOGroupBox.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox IOGroupBox;
        private System.Windows.Forms.Button OutputBrowseButton;
        private System.Windows.Forms.Button InputBrowseButton;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripComboBox PrefixSelector;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}