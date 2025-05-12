using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WWise_Audio_Tools.Classes.AppClasses;
using static WWise_Audio_Tools.Classes.AppClasses.FNVHash;

namespace WWise_Audio_Tools.Forms
{
    public partial class FNVFileListGenerator : Form
    {

        private HashSet<ulong> knownHashes = new HashSet<ulong>();
        private HashSet<ulong> targetHashes = new HashSet<ulong>();

        private List<string> fileContents = new List<string>();
        //public Dictionary<string, string> KnownEvents = new Dictionary<string, string>();

        public FNVFileListGenerator()
        {
            InitializeComponent();

            LoadKnownHashes(@"Libs\known_hashes.txt");
            LoadTargetHashes(@"Libs\target_hashes.txt");
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

        public void WriteStatus(string text, bool prefix = true)
        {
            LoggingTextBox.AppendText($"{((text.Length > 0 && prefix) ? "> " + text : "  " + text)}" + Environment.NewLine);
        }

        private void AboutMenu_Click(object sender, EventArgs e)
        {
            string message = "Small program using the same technique as the 'FNV Hash Generator', used to parse a large list of filenames and output to a text file.";
            string title = "About";
            MessageBox.Show(message, title);
        }

        private void HelpMenu_Click(object sender, EventArgs e)
        {
            string message = "Current implementation will only log and output 'NO MATCH' files for ease of use (and because it's slow as heck otherwise).\n\nRun the 'VoiceItems Collator' to generate a valid list of filenames.";
            string title = "About";
            MessageBox.Show(message, title);
        }

        private void InputButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                Multiselect = false,
                Filter = "Parsed Filenames|*.txt"
            };
            DialogResult fbdResult = ofd.ShowDialog();
            if (fbdResult == DialogResult.OK)
            {
                string sFileName = ofd.FileName;
                InputTextBox.Text = sFileName;

                var fileArray = File.ReadAllLines(sFileName);
                fileContents = fileArray.ToList();
                fileContents.Sort();
            }
            WriteStatus("Successfully loaded parsed filenames.");
        }

        private void OutputButton_Click(object sender, EventArgs e)
        {
            {
                FolderBrowserDialog fbd = new()
                {
                    ShowNewFolderButton = true
                };
                DialogResult fbdResult = fbd.ShowDialog();
                if (fbdResult == DialogResult.OK)
                {
                    AppVariables.OutputDirectory = fbd.SelectedPath;
                    OutputTextBox.Text = fbd.SelectedPath;

                    WriteStatus($"Output set as: {AppVariables.OutputDirectory}");
                }
            }
        }

        private async void RunButton_Click(object sender, EventArgs e)
        {
            RunButton.Enabled = false;
            await Task.Run(() => ProcessFile());
            RunButton.Enabled = true;
        }

        private void ProcessFile()
        {
            var fileOutputList = new List<string>();

            foreach (var line in fileContents)
            {
                ulong hash = Fnv64.ComputeLowerCase(line);

                if (!knownHashes.Contains(hash))
                {
                    string matchStatus = hash.ToString("x16") + "\t" + line;

                    lock (fileOutputList)
                    {
                        fileOutputList.Add(matchStatus);
                    }
                }
                if (targetHashes.Contains(hash))
                {
                    string matchStatus = $"MATCH: {hash:x16}\t{line}";

                    WriteStatus(matchStatus);
                }
              /*  if (!targetHashes.Contains(hash) && knownHashes.Contains(hash))
                {
                    string matchStatus = $"MATCH KNOWN: {hash:x16}\t{line}";

                    WriteStatus(matchStatus);
                } */
            }

            File.WriteAllLines(Path.Join(AppVariables.OutputDirectory, "GeneratedOutput.txt"), fileOutputList);

            Invoke(new Action(() =>
            {
                WriteStatus("Processing completed.");
            }));
        }

        //Other UI elements below
        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoggingTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void OutputTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
