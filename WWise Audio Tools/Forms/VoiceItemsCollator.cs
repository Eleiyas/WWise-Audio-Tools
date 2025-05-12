using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WWise_Audio_Tools.Classes.AppClasses;
using Newtonsoft.Json;

namespace WWise_Audio_Tools.Forms
{
    public partial class VoiceItemsCollator : Form
    {

        private string InputDirectory = "";

        public VoiceItemsCollator()
        {
            InitializeComponent();
        }

        private void InputBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };
            DialogResult fbdResult = fbd.ShowDialog();
            if (fbdResult == DialogResult.OK)
            {
                InputDirectory = fbd.SelectedPath;
                InputTextBox.Text = fbd.SelectedPath;
            }
        }

        private void OutputBrowseButton_Click(object sender, EventArgs e)
        {
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog
                {
                    ShowNewFolderButton = true
                };
                DialogResult fbdResult = fbd.ShowDialog();
                if (fbdResult == DialogResult.OK)
                {
                    AppVariables.OutputDirectory = fbd.SelectedPath;
                    OutputTextBox.Text = fbd.SelectedPath;
                }
            }
        }

        private async void RunButton_Click(object sender, EventArgs e)
        {
            if (PrefixSelector.Text == "Prefix")
            {
                MessageBox.Show("Please select a prefix!", "Alert!");
                return;
            }

            string outputFileName = Path.Join(AppVariables.OutputDirectory, "VoiceItemsParsed.txt");

            string prefix = PrefixSelector.Text != "ALL" ? PrefixSelector.Text + "\\" : "";

            string[] files = Directory.GetFiles(InputDirectory);
            var result = new ConcurrentBag<string>();

            string[] keyNames = { "_sourceNames", "SourceNames", "sourceNames", "OFEEIPOMNKD", "EIKJKDICKMJ", "DHMACMBAEHG" };
            string[] fieldNames = { "sourceFileName", "CBGLAJNLFCB", "HLGOMILNFNK", "NCPBJNJNCEI" };

            await Task.WhenAll(files.Select(async fileName =>
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(await File.ReadAllTextAsync(fileName));

                    foreach (var file in data!.Values)
                    {
                        foreach (var key in keyNames)
                        {
                            if (file.ContainsKey(key))
                            {
                                var sourceNames = file[key] as IEnumerable<dynamic>;
                                if (sourceNames == null) continue;

                                foreach (var src in sourceNames)
                                {
                                    string? srcFileName = null;

                                    foreach (var field in fieldNames)
                                    {
                                        if (src.ContainsKey(field))
                                        {
                                            srcFileName = src[field]?.ToString()?.ToLower();
                                            if (srcFileName != null) break;
                                        }
                                    }

                                    if (srcFileName == null) continue;

                                    if (PrefixSelector.Text == "ALL")
                                    {
                                        result.Add($"english(us)\\{srcFileName}");
                                        result.Add($"chinese\\{srcFileName}");
                                        result.Add($"japanese\\{srcFileName}");
                                        result.Add($"korean\\{srcFileName}");
                                    }
                                    else
                                    {
                                        result.Add($"{prefix}{srcFileName}");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = $"An error occurred: {ex.Message}\n";
                    errorMessage += $"The file causing the error is: {fileName}";

                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));

            // Convert result to unique, sorted list and write to file
            var sortedResult = new HashSet<string>(result).OrderBy(r => r).ToList();
            await File.WriteAllLinesAsync(outputFileName, sortedResult);
        }

        //Other UI Elements
        private void OutputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PrefixSelector_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Please select a prefix before starting!\n\nThis program collates the filenames within 'VoiceItems' and saves them to a file.\n\nThe file generated can be used directly with the 'FNV FileList Generator' to find 'NO MATCH' files for ease of use.";
            string title = "About";
            MessageBox.Show(message, title);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}

