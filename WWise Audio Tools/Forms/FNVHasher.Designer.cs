namespace WWise_Audio_Tools.Forms
{
    partial class FNVHasher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FNVHasher));
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            AboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            InputTextBox = new System.Windows.Forms.TextBox();
            OutputGroupBox = new System.Windows.Forms.GroupBox();
            OutputTextBox = new System.Windows.Forms.RichTextBox();
            FNVTypeGroupBox = new System.Windows.Forms.GroupBox();
            LegacyCheckBox = new System.Windows.Forms.CheckBox();
            FNV64RadioButton = new System.Windows.Forms.RadioButton();
            FNV32RadioButton = new System.Windows.Forms.RadioButton();
            InputGroupBox = new System.Windows.Forms.GroupBox();
            menuStrip1.SuspendLayout();
            OutputGroupBox.SuspendLayout();
            FNVTypeGroupBox.SuspendLayout();
            InputGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { HelpMenu, AboutMenu });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(1165, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
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
            // InputTextBox
            // 
            InputTextBox.Location = new System.Drawing.Point(6, 30);
            InputTextBox.Name = "InputTextBox";
            InputTextBox.Size = new System.Drawing.Size(1127, 31);
            InputTextBox.TabIndex = 2;
            InputTextBox.KeyDown += OnKeyDownHandler;
            // 
            // OutputGroupBox
            // 
            OutputGroupBox.Controls.Add(OutputTextBox);
            OutputGroupBox.Location = new System.Drawing.Point(11, 42);
            OutputGroupBox.Name = "OutputGroupBox";
            OutputGroupBox.Size = new System.Drawing.Size(1142, 539);
            OutputGroupBox.TabIndex = 5;
            OutputGroupBox.TabStop = false;
            OutputGroupBox.Text = "Output";
            // 
            // OutputTextBox
            // 
            OutputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            OutputTextBox.Location = new System.Drawing.Point(3, 27);
            OutputTextBox.Name = "OutputTextBox";
            OutputTextBox.ReadOnly = true;
            OutputTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            OutputTextBox.Size = new System.Drawing.Size(1136, 509);
            OutputTextBox.TabIndex = 0;
            OutputTextBox.Text = "";
            // 
            // FNVTypeGroupBox
            // 
            FNVTypeGroupBox.Controls.Add(LegacyCheckBox);
            FNVTypeGroupBox.Controls.Add(FNV64RadioButton);
            FNVTypeGroupBox.Controls.Add(FNV32RadioButton);
            FNVTypeGroupBox.Location = new System.Drawing.Point(11, 587);
            FNVTypeGroupBox.Name = "FNVTypeGroupBox";
            FNVTypeGroupBox.Size = new System.Drawing.Size(1142, 79);
            FNVTypeGroupBox.TabIndex = 6;
            FNVTypeGroupBox.TabStop = false;
            FNVTypeGroupBox.Text = "FNV Type";
            // 
            // LegacyCheckBox
            // 
            LegacyCheckBox.AutoSize = true;
            LegacyCheckBox.Location = new System.Drawing.Point(369, 35);
            LegacyCheckBox.Name = "LegacyCheckBox";
            LegacyCheckBox.Size = new System.Drawing.Size(92, 29);
            LegacyCheckBox.TabIndex = 2;
            LegacyCheckBox.Text = "Legacy";
            LegacyCheckBox.UseVisualStyleBackColor = true;
            LegacyCheckBox.CheckedChanged += LegacyCheckBox_CheckedChanged;
            // 
            // FNV64RadioButton
            // 
            FNV64RadioButton.AutoSize = true;
            FNV64RadioButton.Location = new System.Drawing.Point(104, 35);
            FNV64RadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            FNV64RadioButton.Name = "FNV64RadioButton";
            FNV64RadioButton.Size = new System.Drawing.Size(95, 29);
            FNV64RadioButton.TabIndex = 1;
            FNV64RadioButton.Text = "FNV 64";
            FNV64RadioButton.UseVisualStyleBackColor = true;
            // 
            // FNV32RadioButton
            // 
            FNV32RadioButton.AutoSize = true;
            FNV32RadioButton.Checked = true;
            FNV32RadioButton.Location = new System.Drawing.Point(7, 35);
            FNV32RadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            FNV32RadioButton.Name = "FNV32RadioButton";
            FNV32RadioButton.Size = new System.Drawing.Size(95, 29);
            FNV32RadioButton.TabIndex = 0;
            FNV32RadioButton.TabStop = true;
            FNV32RadioButton.Text = "FNV 32";
            FNV32RadioButton.UseVisualStyleBackColor = true;
            // 
            // InputGroupBox
            // 
            InputGroupBox.Controls.Add(InputTextBox);
            InputGroupBox.Location = new System.Drawing.Point(11, 672);
            InputGroupBox.Name = "InputGroupBox";
            InputGroupBox.Size = new System.Drawing.Size(1139, 76);
            InputGroupBox.TabIndex = 7;
            InputGroupBox.TabStop = false;
            InputGroupBox.Text = "Input";
            // 
            // FNVHasher
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1165, 760);
            Controls.Add(InputGroupBox);
            Controls.Add(FNVTypeGroupBox);
            Controls.Add(OutputGroupBox);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "FNVHasher";
            Text = "FNVHasher";
            Load += FNVHasher_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            OutputGroupBox.ResumeLayout(false);
            FNVTypeGroupBox.ResumeLayout(false);
            FNVTypeGroupBox.PerformLayout();
            InputGroupBox.ResumeLayout(false);
            InputGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.GroupBox OutputGroupBox;
        private System.Windows.Forms.RichTextBox OutputTextBox;
        private System.Windows.Forms.GroupBox FNVTypeGroupBox;
        private System.Windows.Forms.GroupBox InputGroupBox;
        private System.Windows.Forms.RadioButton FNV64RadioButton;
        private System.Windows.Forms.RadioButton FNV32RadioButton;
        private System.Windows.Forms.ToolStripMenuItem AboutMenu;
        private System.Windows.Forms.CheckBox LegacyCheckBox;
    }
}