using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using static WWise_Audio_Tools.Classes.AppClasses.FNVHash;

namespace WWise_Audio_Tools.Forms
{
    public partial class FNVHasher : Form
    {
        private HashSet<ulong> knownHashes = new HashSet<ulong>();
        private HashSet<ulong> targetHashes = new HashSet<ulong>();

        public FNVHasher()
        {
            InitializeComponent();

            LoadKnownHashes(@"Libs\known_hashes.txt");
            LoadTargetHashes(@"Libs\target_hashes.txt");
        }

        private void HelpMenu_Click(object sender, EventArgs e)
        {
            string message = "Genshin Filename Layout:\n- [language]\\\n- [identifier]\\\n- vo_[character]\\ (this can also be \"CS\" or \"NPC\")\n- [format*].wem\\\n- _[number].wem\n\nLanguage List:\n- english(us)\\\n- chinese\\\n- japanese\\\n- korean\\\n\nIdentifier List:\n[Archon Quest] - vo_aq\\\n[Event Quest] - vo_eq\\\n[Legend Quest] - vo_lq\\\n[World Quest] - vo_wq\\\n[Hangout Event] - vo_coop\\\n[NPC Idles] - vo_ingame\\\n[Freetalk] - vo_freetalk\\\n[Tutorials] - vo_tips\\\n[Join Party] - vo_teamjoin\\\n[Costume Change] - vo_costume\\\n[Story Voice-Overs] - vo_friendship\\\n[Combat Voice-Overs] - vo_gameplay\\\n[Serenitea Pot Voice-Overs] - vo_hs\\\n[Boss Voice-Overs] - vo_monster\\\n[Interactables] - vo_gadget\\\n\n*Depending on the type of file you are wanting to de-hash, it will follow a different format.\nPlease ask Eleiyas or refer to the \"Genshin Sound Catalogue\" for more details.";
            string title = "Help Window";
            MessageBox.Show(message, title);
        }

        private void AboutMenu_Click(object sender, EventArgs e)
        {
            string message = "A small program used to manually generate FNV hashes by inputting suspected filenames.";
            string title = "Help Window";
            MessageBox.Show(message, title);
        }

        private void LoadTargetHashes(string filename)
        {
            targetHashes.Clear();

            var hashes = File.ReadAllLines(filename);

            foreach (var hash in hashes)
            {
                // Might want to notify about the failed parses
                if (ulong.TryParse(hash, NumberStyles.HexNumber, null, out var value))
                    targetHashes.Add(value);
            }
        }

        private void LoadKnownHashes(string filename)
        {
            knownHashes.Clear();

            var hashes = File.ReadAllLines(filename);

            foreach (var hash in hashes)
            {
                // Might want to notify about the failed parses
                if (ulong.TryParse(hash, NumberStyles.HexNumber, null, out var value))
                    knownHashes.Add(value);
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if FNV32 is selected
                if (FNV32RadioButton.Checked)
                {
                    TextBox InputTextBox = (TextBox)sender;
                    string UserText = InputTextBox.Text;

                    uint hash = Fnv32.ComputeLowerCase(UserText);

                    var inTarget = targetHashes.Contains(hash);
                    var inKnown = knownHashes.Contains(hash);

                    if (inTarget || inKnown)
                    {
                        if (inTarget)
                        {
                            OutputTextBox.SelectionColor = System.Drawing.Color.Green;
                            if (LegacyCheckBox.Checked)
                            {
                                OutputTextBox.AppendText("MATCH - " + hash.ToString("d") + "\t" + UserText + Environment.NewLine);
                            }
                            else
                            {
                                OutputTextBox.AppendText("MATCH - " + hash.ToString("x8") + "\t" + UserText + Environment.NewLine);
                            }
                        }

                        if (inKnown)
                        {
                            OutputTextBox.SelectionColor = System.Drawing.Color.Olive;
                            if (LegacyCheckBox.Checked)
                            {
                                OutputTextBox.AppendText("MATCH KNOWN - " + hash.ToString("d") + "\t" + UserText + Environment.NewLine);
                            }
                            else
                            {
                                OutputTextBox.AppendText("MATCH KNOWN - " + hash.ToString("x8") + "\t" + UserText + Environment.NewLine);
                            }
                        }
                    }
                    else
                    {
                        if (LegacyCheckBox.Checked)
                        {
                            OutputTextBox.AppendText(hash.ToString("d") + "\t" + UserText + Environment.NewLine);
                        }
                        else
                        {
                            OutputTextBox.AppendText(hash.ToString("x8") + "\t" + UserText + Environment.NewLine);
                        }
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }

                //if FNV64 is selected
                if (FNV64RadioButton.Checked)
                {
                    TextBox InputTextBox = (TextBox)sender;
                    string UserText = InputTextBox.Text;

                    ulong hash = Fnv64.ComputeLowerCase(UserText);

                    var inTarget = targetHashes.Contains(hash);
                    var inKnown = knownHashes.Contains(hash);

                    if (inTarget || inKnown)
                    {
                        if (inTarget)
                        {
                            OutputTextBox.SelectionColor = System.Drawing.Color.Green;
                            if (LegacyCheckBox.Checked)
                            {
                                OutputTextBox.AppendText("MATCH - " + hash.ToString("d") + "\t" + UserText + Environment.NewLine);
                            }
                            else
                            {
                                OutputTextBox.AppendText("MATCH - " + hash.ToString("x16") + "\t" + UserText + Environment.NewLine);
                            }
                        }

                        if (inKnown)
                        {
                            OutputTextBox.SelectionColor = System.Drawing.Color.Olive;
                            if (LegacyCheckBox.Checked)
                            {
                                OutputTextBox.AppendText("MATCH KNOWN - " + hash.ToString("d") + "\t" + UserText + Environment.NewLine);
                            }
                            else
                            {
                                OutputTextBox.AppendText("MATCH KNOWN - " + hash.ToString("x16") + "\t" + UserText + Environment.NewLine);
                            }
                        }
                    }
                    else
                    {
                        if (LegacyCheckBox.Checked)
                        {
                            OutputTextBox.AppendText(hash.ToString("d") + "\t" + UserText + Environment.NewLine);
                        }
                        else
                        {
                            OutputTextBox.AppendText(hash.ToString("x16") + "\t" + UserText + Environment.NewLine);
                        }
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }

        //Other UI Elements Below
        private void OutputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void OutputGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void FNVTypeGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void InputGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void FNVHasher_Load(object sender, EventArgs e)
        {

        }

        private void LegacyCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
