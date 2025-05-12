using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFMpegCore;
using FFMpegCore.Pipes;
using WWise_Audio_Tools.Classes.AppClasses;
using WWise_Audio_Tools.Classes.BankClasses;
using WWise_Audio_Tools.Classes.BankClasses.Chunks;
using WWise_Audio_Tools.Classes.PackageClasses;

namespace WWise_Audio_Tools
{
    public partial class WWiseAudioExtractor : Form
    {
        private bool doUpdateFormatSettings = false;
        private bool isBusy = false;
        private bool isAborted = false;

        public Dictionary<string, string> KnownFilenames = new Dictionary<string, string>();
        public Dictionary<string, string> KnownEvents = new Dictionary<string, string>();

        public class ProcessedFile
        {
            public string FilePath { get; set; }
            public string FileHash { get; set; }
        }

        public enum OutputFormat
        {
            Wem,
            Wav,
            Ogg
        }

        public WWiseAudioExtractor()
        {
            InitializeComponent();

            OutputDirectoryTextBox.Text = Properties.Settings.Default.OutputDirectory;

            WEMExportRadioButton.Checked = Properties.Settings.Default.ExportWem;
            WAVExportRadioButton.Checked = Properties.Settings.Default.ExportWav;
            OGGExportRadioButton.Checked = Properties.Settings.Default.ExportOgg;

            doUpdateFormatSettings = true;
            UpdateCanExportStatus();
        }

        private void vGMStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory("Tools");
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += VGM_DownloadProgressChanged;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new Uri("https://github.com/vgmstream/vgmstream-releases/releases/download/nightly/vgmstream-win64.zip"),
                    // Param2 = Path to save
                    @"Tools\vgmstream-win.zip"
                );
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadVGMCallback);
            }
        }

        private void fFMPegToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Directory.CreateDirectory("Tools");
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += FFMPEG_DownloadProgressChanged;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new Uri("https://github.com/BtbN/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-win64-gpl-shared.zip"),
                    // Param2 = Path to save
                    @"Tools\ffmpeg-master.zip"
                );
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFFMPEGCallback);
            }
        }

        private static void DownloadVGMCallback(object sender, AsyncCompletedEventArgs e)
        {
            var StatusMessage = new WWiseAudioExtractor();

            string VGMZipPath = @"Tools\vgmstream-win.zip";
            string VGMExtractPath = @"Tools\vgmstream-win";

            if (!Directory.Exists(VGMExtractPath))
            {
                Directory.CreateDirectory(VGMExtractPath);
            }

            var VGMfiles = new DirectoryInfo(VGMExtractPath).GetFiles("*.*");

            ZipFile.ExtractToDirectory(VGMZipPath, VGMExtractPath, true);

            File.Delete(VGMZipPath);

            if (e.Cancelled)
            {
                StatusMessage.StatusTextBox.AppendText("File download cancelled." + Environment.NewLine);
            }

            if (e.Error != null)
            {
                StatusMessage.StatusTextBox.SelectionColor = Color.Red;
                StatusMessage.StatusTextBox.AppendText(e.Error.ToString() + Environment.NewLine);
            }

        }
        private static void DownloadFFMPEGCallback(object sender, AsyncCompletedEventArgs e)
        {
            var StatusMessage = new WWiseAudioExtractor();

            string FFMPEGZipPath = @"Tools\ffmpeg-master.zip";
            string FFMPEGExtractPath = @"Tools\";

            if (!Directory.Exists(FFMPEGExtractPath))
            {
                Directory.CreateDirectory(FFMPEGExtractPath);
            }

            var FFMPEGfiles = new DirectoryInfo(FFMPEGExtractPath).GetFiles("*.*");

            ZipFile.ExtractToDirectory(FFMPEGZipPath, FFMPEGExtractPath, true);

            File.Delete(FFMPEGZipPath);

            if (e.Cancelled)
            {
                StatusMessage.StatusTextBox.AppendText("File download cancelled." + Environment.NewLine);
            }

            if (e.Error != null)
            {
                StatusMessage.StatusTextBox.SelectionColor = Color.Red;
                StatusMessage.StatusTextBox.AppendText(e.Error.ToString() + Environment.NewLine);
            }

        }

        int vgmLastPercentage = 0;

        void VGM_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (vgmLastPercentage == e.ProgressPercentage)
                return;

            vgmLastPercentage = e.ProgressPercentage;
            CurrentProgressBar.Value = e.ProgressPercentage;

            if (e.ProgressPercentage < 100)
            {
                StatusTextBox.AppendText($"Downloading latest VGMStream version: {e.ProgressPercentage}% completed." + Environment.NewLine);
            }
            if (e.ProgressPercentage == 100)
            {
                StatusTextBox.SelectionColor = Color.Green;
                StatusTextBox.AppendText($"Download Completed." + Environment.NewLine);
            }
        }

        int ffmpegLastPercentage = 0;

        void FFMPEG_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (ffmpegLastPercentage == e.ProgressPercentage)
                return;

            ffmpegLastPercentage = e.ProgressPercentage;
            CurrentProgressBar.Value = e.ProgressPercentage;

            if (e.ProgressPercentage < 100)
            {
                StatusTextBox.AppendText($"Downloading latest FFMPEG version: {e.ProgressPercentage}% completed." + Environment.NewLine);
            }
            if (e.ProgressPercentage == 100)
            {
                StatusTextBox.SelectionColor = Color.Green;
                StatusTextBox.AppendText($"Download Completed." + Environment.NewLine);
            }
        }

        private void InputDirectoryBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "PCK Files|*.pck|BNK Files|*.bnk|WEM Files|*.wem"
            };
            DialogResult fbdResult = ofd.ShowDialog();
            if (fbdResult == DialogResult.OK)
            {
                AppVariables.InputFiles.Clear();
                AppVariables.InputFiles = ofd.FileNames.ToList();
                foreach (var filePath in AppVariables.InputFiles)
                {
                    string fileName = Path.GetFileName(filePath);
                    string folderName = Path.GetFileName(Path.GetDirectoryName(filePath));
                    string outputLine = folderName + "\\" + fileName;

                    if (filePath.EndsWith(".pck"))
                    {
                        InputDirectoryTextBox.Text = $"Selected {AppVariables.InputFiles.Count} WWise file{(AppVariables.InputFiles.Count > 1 ? "s" : "")} to process.";
                        StatusTextBox.SelectionColor = Color.Green;
                        StatusTextBox.AppendText($"Successfully loaded {outputLine}" + Environment.NewLine);
                    }
                    if (filePath.EndsWith(".bnk"))
                    {
                        InputDirectoryTextBox.Text = $"Selected {AppVariables.InputFiles.Count} WWise file{(AppVariables.InputFiles.Count > 1 ? "s" : "")} to process.";
                        StatusTextBox.SelectionColor = Color.Green;
                        StatusTextBox.AppendText($"Successfully loaded {outputLine}" + Environment.NewLine);
                    }
                    if (filePath.EndsWith(".wem"))
                    {
                        InputDirectoryTextBox.Text = $"Selected {AppVariables.InputFiles.Count} WWise file{(AppVariables.InputFiles.Count > 1 ? "s" : "")} to process.";
                        StatusTextBox.SelectionColor = Color.Green;
                        StatusTextBox.AppendText($"Successfully loaded {outputLine}" + Environment.NewLine);
                    }
                }
            }
            UpdateCanExportStatus();
        }

        private void OutputDirectoryBrowse_Click(object sender, EventArgs e)
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
                    OutputDirectoryTextBox.Text = fbd.SelectedPath;

                    AppVariables.OutputDirectoryWem = Path.Combine(AppVariables.OutputDirectory, "Wem");
                    AppVariables.OutputDirectoryWav = Path.Combine(AppVariables.OutputDirectory, "Wav");
                    AppVariables.OutputDirectoryOgg = Path.Combine(AppVariables.OutputDirectory, "Ogg");

                    StatusTextBox.AppendText($"{AppVariables.OutputDirectory}" + Environment.NewLine);
                }
                UpdateCanExportStatus();
            }

        }

        private void KnownFilenamesBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Known_Filenames TSV|*.tsv"
            };
            DialogResult fbdResult = ofd.ShowDialog();
            if (fbdResult == DialogResult.OK)
            {
                string sFileName = ofd.FileName;
                KnownFilenamesTextBox.Text = sFileName;

                var lines = File.ReadAllLines(sFileName);

                foreach (var line in lines)
                {
                    if (line.Trim() == "")
                        continue;

                    var entry = line.Split("\t");

                    KnownFilenames[entry[0]] = entry[1];
                }
            }
            StatusTextBox.SelectionColor = Color.Green;
            StatusTextBox.AppendText("Successfully loaded Known_Filenames.tsv" + Environment.NewLine);
            UpdateCanExportStatus();
        }

        private void KnownEventsBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Known_Events TSV|*.tsv"
            };
            DialogResult fbdResult = ofd.ShowDialog();
            if (fbdResult == DialogResult.OK)
            {
                string sFileName = ofd.FileName;
                KnownEventsTextBox.Text = sFileName;

                var lines = File.ReadAllLines(sFileName);

                foreach (var line in lines)
                {
                    if (line.Trim() == "")
                        continue;

                    var entry = line.Split("\t");

                    KnownEvents[entry[0]] = entry[1];
                }
            }
            StatusTextBox.SelectionColor = Color.Green;
            StatusTextBox.AppendText("Successfully loaded Known_Events.tsv" + Environment.NewLine);
            UpdateCanExportStatus();
        }

        private void UpdateAudioFormatStatus(object sender, EventArgs e)
        {
            if (doUpdateFormatSettings)
                UpdateCanExportStatus();
        }

        private void UpdateCanExportStatus()
        {
            bool canExport = true;
            AppVariables.ExportWem = WEMExportRadioButton.Checked;
            AppVariables.ExportWav = WAVExportRadioButton.Checked;
            AppVariables.ExportOgg = OGGExportRadioButton.Checked;

            if (!AppVariables.ExportWem && !AppVariables.ExportWav && !AppVariables.ExportOgg)
                canExport = false;
            if (AppVariables.InputFiles.Count == 0 || !Directory.Exists(OutputDirectoryTextBox.Text))
                canExport = false;
            if (!Directory.Exists(@"Tools\vgmstream-win"))
            {
                canExport = false;
                StatusTextBox.SelectionColor = Color.Red;
                StatusTextBox.AppendText("Please download VGMStream." + Environment.NewLine);
            }
            if (!Directory.Exists(@"Tools\ffmpeg-master-latest-win64-gpl-shared"))
            {
                canExport = false;
                StatusTextBox.SelectionColor = Color.Red;
                StatusTextBox.AppendText("Please download FFMPEG." + Environment.NewLine);
            }
            if (canExport)
            {
                Properties.Settings.Default.ExportWem = AppVariables.ExportWem;
                Properties.Settings.Default.ExportWav = AppVariables.ExportWav;
                Properties.Settings.Default.ExportOgg = AppVariables.ExportOgg;

                Properties.Settings.Default.KnownFilenamesPath = AppVariables.KnownFilenamesPath;
                Properties.Settings.Default.KnownEventsPath = AppVariables.KnownEventsPath;

                AppVariables.OutputDirectory = OutputDirectoryTextBox.Text;
                Properties.Settings.Default.OutputDirectory = OutputDirectoryTextBox.Text;
                ExportButton.Enabled = canExport;

                StatusTextBox.SelectionColor = Color.Green;
                StatusTextBox.AppendText("Ready to Export" + Environment.NewLine);
            }
            else
                ExportButton.Enabled = false;
        }

        private async void ExportButton_Click(object sender, EventArgs e)
        {
            if (!isBusy)
            {
                isBusy = true;
                ExportButton.Text = "Abort";
                ParametersGroupBox.Enabled = false;

                TotalProgressBar.Style = ProgressBarStyle.Continuous;
                CurrentProgressBar.Style = ProgressBarStyle.Continuous;

                TotalProgressBar.Value = 0;
                CurrentProgressBar.Value = 0;

                int index = 0;
                int overallIndex = 0;

                Directory.CreateDirectory(AppVariables.OutputDirectoryWem);

                if (!isAborted)
                {
                    CurrentProgressBar.Maximum = AppVariables.InputFiles.Count;
                    TotalProgressBar.Maximum = AppVariables.InputFiles.Count;

                    StatusTextBox.SelectionColor = Color.Purple;
                    StatusTextBox.AppendText($"Exporting {AppVariables.InputFiles.Count} files" + Environment.NewLine);

                    for (int i = 0; i < AppVariables.InputFiles.Count; i++)
                    {
                        index++;
                        overallIndex++;
                        CurrentProgressBar.Value = index;
                        TotalProgressBar.Value = overallIndex;

                        string pckFile = AppVariables.InputFiles[i];

                        string fileName = Path.GetFileName(AppVariables.InputFiles[i]);
                        string folderName = Path.GetFileName(Path.GetDirectoryName(AppVariables.InputFiles[i]));
                        string pckFileString = folderName + "\\" + fileName;

                        // Process pck
                        ProcessFile(pckFile, AppVariables.OutputDirectoryWem);
                        StatusTextBox.AppendText($"Exported: {pckFileString}" + Environment.NewLine);
                    }

                    if (AppVariables.ExportWem && !SplitOutputCheckBox.Checked && !NoLangCheckBox.Checked)
                    {
                        GenerateMD5Checksums();
                    }

                    if (AppVariables.ExportWav)
                    {
                        Directory.CreateDirectory(AppVariables.OutputDirectoryWav);

                        GenerateMD5Checksums();

                        ProcessDirectory(AppVariables.OutputDirectoryWem, true);
                    }

                    if (AppVariables.ExportOgg)
                    {
                        Directory.CreateDirectory(AppVariables.OutputDirectoryWav);
                        Directory.CreateDirectory(AppVariables.OutputDirectoryOgg);

                        GenerateMD5Checksums();

                        ProcessDirectory(AppVariables.OutputDirectoryWem, true);
                    }
                    Cleanup();
                    await ProcessOutputAsync();
                }
            }
            else
            {
                isAborted = true;
                ExportButton.Text = "Aborting...";
                ExportButton.Enabled = false;
                return;
            }

            StatusTextBox.Focus();
            OnExportEnded(isAborted);
        }

        private async Task ProcessOutputAsync()
        {
            await Task.Run(() =>
            {
                ProcessOutput();
            });

            StatusTextBox.SelectionColor = Color.Green;
            StatusTextBox.AppendText("Processing Output Completed.");
        }

        private void ProcessOutput()
        {
            string directoryName = Path.GetFileName(AppVariables.OutputDirectory);
            string processedFilesFilePath = Path.Combine("Logging\\", directoryName + "-OutputFiles.txt");

            ConcurrentBag<string> processedFiles = new ConcurrentBag<string>();
            ConcurrentBag<string> newFiles = new ConcurrentBag<string>();

            bool changesMade = false;

            if (File.Exists(processedFilesFilePath))
            {
                foreach (string line in File.ReadAllLines(processedFilesFilePath))
                {
                    processedFiles.Add(line);
                }
            }

            Parallel.ForEach(Directory.GetFiles(AppVariables.OutputDirectory, "*", SearchOption.AllDirectories), file =>
            {
                string fileName = Path.GetFileName(file);
                string folderPath = Path.GetDirectoryName(file);
                string folderName = folderPath.Substring(AppVariables.OutputDirectory.Length);
                string[] folders = folderName.Split(Path.DirectorySeparatorChar);
                string concatenatedFolders = string.Join("\\", folders);
                string outputLine = concatenatedFolders + "\\" + fileName;

                if (!processedFiles.Contains(outputLine))
                {
                    processedFiles.Add(outputLine);
                    newFiles.Add(outputLine);
                    StatusTextBox.Invoke((MethodInvoker)delegate
                    {
                        StatusTextBox.SelectionColor = Color.Green;
                        StatusTextBox.AppendText("> " + "New file detected: " + outputLine + Environment.NewLine);
                    });
                    changesMade = true;
                }
            });

            if (changesMade)
            {
                var sortedResult = processedFiles.OrderBy(r => r).ToList();
                File.WriteAllLines(processedFilesFilePath, sortedResult);
                StatusTextBox.Invoke((MethodInvoker)delegate
                {
                    StatusTextBox.SelectionColor = Color.Purple;
                    StatusTextBox.AppendText($"{newFiles.Count} new audio files have been exported." + Environment.NewLine);
                });
            }

            if (SpreadsheetOutputCheckBox.Checked)
            {
                List<string> OutputFiles = new List<string>();

                foreach (string file in Directory.GetFiles(AppVariables.OutputDirectoryWem, "*", SearchOption.AllDirectories))
                {
                    if (!OutputFiles.Contains(file))
                    {
                        OutputFiles.Add(file);
                    }
                }

                string outputFilePath = Path.Combine("Logging\\", "SpreadsheetOutput.txt");

                List<string> outputLines = new List<string>();
                foreach (string file in OutputFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string folderName = Path.GetFileName(Path.GetDirectoryName(file));
                    string outputLine = folderName + "\t" + fileName;
                    outputLines.Add(outputLine);
                }

                File.WriteAllLines(outputFilePath, outputLines);
                StatusTextBox.Invoke((MethodInvoker)delegate
                {
                    StatusTextBox.Text += "> " + "Output files saved to " + outputFilePath + Environment.NewLine;
                });
            }
        }

        private void GenerateMD5Checksums()
        {
            string directoryName = Path.GetFileName(AppVariables.OutputDirectory);
            string processedFilesFilePath = Path.Combine("Logging\\", directoryName + "-WEM_Checksums.csv");

            Dictionary<string, (string, string)> processedFileHashes = new Dictionary<string, (string, string)>();

            bool changesMade = false;

            if (!Directory.Exists("Logging\\"))
            {
                Directory.CreateDirectory("Logging\\");
            }

            if (File.Exists(processedFilesFilePath))
            {
                processedFileHashes = File.ReadLines(processedFilesFilePath)
                    .Select(line => line.Split(','))
                    .ToDictionary(parts => parts[0], parts => (parts[1], parts[2]));
            }

            foreach (string file in Directory.GetFiles(AppVariables.OutputDirectoryWem, "*", SearchOption.AllDirectories))
            {
                string fileName = Path.GetFileName(file);
                string folderPath = Path.GetDirectoryName(file);
                string folderName = folderPath.Substring(AppVariables.OutputDirectoryWem.Length);
                string[] folders = folderName.Split(Path.DirectorySeparatorChar);
                string concatenatedFolders = string.Join("\\", folders);
                string fileHash = GetFileHash(file);
                string outputLine = concatenatedFolders + "\\" + fileName;

                if (processedFileHashes.TryGetValue(outputLine, out var processedFileHashAndDate))
                {
                    if (fileHash != processedFileHashAndDate.Item1)
                    {
                        StatusTextBox.SelectionColor = Color.Orange;
                        StatusTextBox.AppendText("> " + "MD5-Checksum Changed: " + outputLine + Environment.NewLine);
                        processedFileHashes[outputLine] = (fileHash, DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                        changesMade = true;
                    }
                    //This small piece of code should make it so existing files with identical checksums get deleted prior to conversion.
                    else
                    {
                        File.Delete(file);
                        StatusTextBox.SelectionColor = Color.Red;
                        StatusTextBox.AppendText("> " + "File Deleted (MD5-Checksum Matched): " + outputLine + Environment.NewLine);
                    }
                }
                else
                {
                    StatusTextBox.SelectionColor = Color.Green;
                    StatusTextBox.AppendText("> " + "New MD5-Checksum Generated: " + outputLine + Environment.NewLine);
                    processedFileHashes[outputLine] = (fileHash, DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    changesMade = true;
                }
            }
            if (changesMade)
            {
                var processedFileLines = processedFileHashes.Select(kv => kv.Key + "," + kv.Value.Item1 + "," + kv.Value.Item2);

                var sortedLines = processedFileLines
                    .OrderBy(line =>
                    {
                        var parts = line.Split(',');
                        var dateStr = parts[2].Trim();
                        var date = DateTime.ParseExact(dateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        return date;
                    })
                    .ThenBy(line =>
                    {
                        var parts = line.Split(',');
                        return parts[0];
                    });

                File.WriteAllLines(processedFilesFilePath, sortedLines);
            }
        }

        string GetFileHash(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] checksumBytes = md5.ComputeHash(stream);
                    return BitConverter.ToString(checksumBytes).Replace("-", "").ToLower();
                }
            }
        }

        private void Cleanup()
        {
            if (AppVariables.ExportWav)
            {
                foreach (string dirPath in Directory.GetDirectories(AppVariables.OutputDirectoryWem))
                {
                    var dirString = dirPath.Replace(AppVariables.OutputDirectory, "");

                    Directory.Delete(dirPath, true);

                    StatusTextBox.SelectionColor = Color.Red;
                    StatusTextBox.AppendText($"Deleting: {dirString}" + Environment.NewLine);
                }
            }
            if (AppVariables.ExportOgg)
            {
                foreach (string dirPath in Directory.GetDirectories(AppVariables.OutputDirectoryWem))
                {
                    var dirString = dirPath.Replace(AppVariables.OutputDirectory, "");

                    Directory.Delete(dirPath, true);

                    StatusTextBox.SelectionColor = Color.Red;
                    StatusTextBox.AppendText($"Deleting: {dirString}" + Environment.NewLine);
                }
                foreach (string dirPath in Directory.GetDirectories(AppVariables.OutputDirectoryWav))
                {
                    var dirString = dirPath.Replace(AppVariables.OutputDirectory, "");

                    Directory.Delete(dirPath, true);

                    StatusTextBox.SelectionColor = Color.Red;
                    StatusTextBox.AppendText($"Deleting: {dirString}" + Environment.NewLine);
                }
            }
            DialogResult result = MessageBox.Show("Delete all input files?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (var filePath in AppVariables.InputFiles)
                {
                    string fileName = Path.GetFileName(filePath);
                    string folderName = Path.GetFileName(Path.GetDirectoryName(filePath));
                    string outputLine = folderName + "\\" + fileName;

                    File.Delete(filePath);
                    StatusTextBox.SelectionColor = Color.Red;
                    StatusTextBox.AppendText($"Deleting: {outputLine}" + Environment.NewLine);
                }
            }
            else
            {
                return;
            }
        }

        public List<string> ProcessDirectory(string path, bool process)
        {
            var fileList = new List<string>();

            foreach (var dirPath in Directory.GetDirectories(path))
            {
                var dirString = path.Replace(AppVariables.OutputDirectory, "");

                StatusTextBox.AppendText($"Processing Directory: {dirString}" + Environment.NewLine);
            }

            if (!Directory.Exists(path)) return fileList;
            var directories = Directory.GetDirectories(path);
            if (directories.Length > 0)
            {
                foreach (var directory in directories)
                {
                    if (process && !Directory.Exists(directory)) Directory.CreateDirectory(directory);
                    fileList.AddRange(ProcessDirectory(directory, process));
                }
            }
            var files = Directory.GetFiles(path);
            if (files.Length > 0)
            {
                foreach (var file in files)
                {
                    ProcessFile(file, process);
                    if (process) File.Delete(file);
                    fileList.Add(file);
                }
            }
            return fileList;
        }

        public void ProcessFile(string file, bool process)
        {
            try
            {
                var fileString = file.Replace(AppVariables.OutputDirectory, "");

                StatusTextBox.AppendText($"Processed: {fileString}" + Environment.NewLine);
                if (AppVariables.ExportWav && process && !isAborted)
                {
                    var outputFile = file.Replace(AppVariables.OutputDirectoryWem, AppVariables.OutputDirectoryWav).Replace(".wem", ".wav");

                    var outputFileString = outputFile.Replace(AppVariables.OutputDirectory, "");

                    ProcessFile(file, outputFile);
                    if (CurrentProgressBar.Value != CurrentProgressBar.Maximum) CurrentProgressBar.Value++;
                }

                if (AppVariables.ExportOgg && process && !isAborted)
                {
                    var wavOutputFile = file.Replace(AppVariables.OutputDirectoryWem, AppVariables.OutputDirectoryWav).Replace(".wem", ".wav");
                    var oggOutputFile = wavOutputFile.Replace(AppVariables.OutputDirectoryWav, AppVariables.OutputDirectoryOgg).Replace(".wav", ".ogg");

                    var wavOutputString = wavOutputFile.Replace(AppVariables.OutputDirectory, "");
                    var oggOutputString = oggOutputFile.Replace(AppVariables.OutputDirectory, "");

                    // Convert .wem -> to .wav.
                    ProcessFile(file, wavOutputFile);
                    if (CurrentProgressBar.Value != CurrentProgressBar.Maximum) CurrentProgressBar.Value++;

                    // This would be inside ProcessFile() the issue is that both .wem and .wav have the same magic (RIFF) on the header.
                    // Hence all it would do is reconvert it to wav.
                    var data = File.ReadAllBytes(wavOutputFile);
                    ProcessWav(data, oggOutputFile);

                    if (CurrentProgressBar.Value != CurrentProgressBar.Maximum) CurrentProgressBar.Value++;
                }
            }
            catch (FileNotFoundException ex)
            {
                StatusTextBox.SelectionColor = Color.Red;
                StatusTextBox.AppendText($"File not found: {ex.Message}" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                StatusTextBox.SelectionColor = Color.Red;
                StatusTextBox.AppendText($"An error occurred: {ex.Message}" + Environment.NewLine);
            }
        }

        private void OnExportEnded(bool aborted)
        {
            ExportButton.Enabled = true;
            ExportButton.Text = "Export";
            ParametersGroupBox.Enabled = true;
            isAborted = false;
            isBusy = false;
        }

        public void ProcessFile(string filepath, string outputPath = null)
        {
            var data = File.ReadAllBytes(filepath);

            switch (data.DetermineFileExtension())
            {
                case ".pck":
                    ProcessPck(data, filepath);
                    break;
                case ".bnk":
                    ProcessBnk(data, filepath);
                    break;
                case ".wem":
                    ProcessWem(data, outputPath);
                    break;
                default:
                    break;
            }
        }

        public void ProcessPck(byte[] data, string filepath)
        {
            var pck = new Package(data);

            foreach (var entry in pck.BanksTable.Files)
            {
                var bnkData = pck.GetBytes(entry);
                ProcessPckBnk(bnkData, entry, filepath);
            }
            foreach (var entry in pck.ExternalsTable.Files)
            {
                var extData = pck.GetBytes(entry);
                ProcessPckExternal(extData, entry, filepath);
            }
            foreach (var entry in pck.StreamsTable.Files)
            {
                var streamData = pck.GetBytes(entry);
                ProcessPckStream(streamData, entry, filepath);
            }
        }

        public void ProcessBnk(byte[] data, string filepath)
        {
            var bnk = new Bank(data);

            if (bnk.DIDXChunk is not null && bnk.DATAChunk is not null)
            {
                foreach (var fileEntryList in bnk.DIDXChunk.Files.Values)
                {
                    foreach (var file in fileEntryList)
                    {
                        var wemData = bnk.DATAChunk.GetFile(file);
                        ProcessPckBnkWem(wemData, file, bnk.Header, filepath);
                    }
                }
            }
        }

        public void ProcessWem(byte[] data, string path)
        {
            byte[] convertedData = ConvertData(data, out var convertedFormat, OutputFormat.Wav, OutputFormat.Wem);
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? "");
            File.WriteAllBytes(path, convertedData);
        }

        public void ProcessWav(byte[] data, string path)
        {
            byte[] convertedData = ConvertData(data, out var convertedFormat, OutputFormat.Ogg, OutputFormat.Wav);
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? "");
            File.WriteAllBytes(path, convertedData);
        }

        public void ProcessPckBnk(byte[] data, FileTable.FileEntry entry, string filepath)
        {
            var bnk = new Bank(data);
            bnk.Package = entry.Parent.Parent;
            bnk.Language = entry.GetLanguage();

            if (bnk.DIDXChunk is not null && bnk.DATAChunk is not null)
            {
                foreach (var fileEntryList in bnk.DIDXChunk.Files.Values)
                {
                    foreach (var file in fileEntryList)
                    {
                        var wemData = bnk.DATAChunk.GetFile(file);
                        ProcessPckBnkWem(wemData, file, bnk.Header, filepath);
                    }
                }
            }
        }

        public void ProcessPckBnkWem(byte[] data, DataIndexChunk.FileEntry entry, BankHeader header, string filepath)
        {
            var convertedData = ConvertData(data, out var format);
            var path = GetPckBnkWemOutputPath(entry, header, format);

            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? "");
            File.WriteAllBytes(path, convertedData);
        }

        public void ProcessPckExternal(byte[] data, FileTable.FileEntry entry, string filepath)
        {
            var convertedData = ConvertData(data, out var format);
            var path = GetPckExternalOutputPath(filepath, entry, format);

            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? "");
            File.WriteAllBytes(path, convertedData);
        }

        public void ProcessPckStream(byte[] data, FileTable.FileEntry entry, string filepath)
        {
            var convertedData = ConvertData(data, out var format);
            var path = GetPckStreamOutputPath(entry, format);

            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? "");
            File.WriteAllBytes(path, convertedData);
        }

        public string GetPckExternalName(ulong fileId, out bool hasLanguage)
        {
            var name = fileId.ToString("x16");

            hasLanguage = KnownFilenames.TryGetValue(name, out var full);

            if (hasLanguage)
                return Path.Join(Path.GetDirectoryName(full), Path.GetFileNameWithoutExtension(full));

            return name;
        }

        public string GetPckStreamName(ulong fileId, out bool hasLanguage)
        {
            hasLanguage = false;

            if (LegacyCheckBox.Checked)
                return fileId.ToString("d");

            return fileId.ToString("x8");
        }

        //Made some edits, so that PCK/BNK files get named properly based on KnownEvents
        public string GetPckBnkWemName(ulong fileId, out bool hasLanguage)
        {
            var name = fileId.ToString("x8");
            if (LegacyCheckBox.Checked)
            {
                name = fileId.ToString("d");

                hasLanguage = KnownEvents.TryGetValue(name, out var full);

                if (hasLanguage)
                    return Path.Join(Path.GetDirectoryName(full), Path.GetFileNameWithoutExtension(full));
            }
            else
            {
                hasLanguage = false;
            }
            return name;
        }

        public byte[] ConvertData(byte[] data, out OutputFormat convertedFormat, OutputFormat? targetFormat = null, OutputFormat? sourceFormat = null)
        {
            if (sourceFormat is null)
                sourceFormat = OutputFormat.Wem;

            if (targetFormat is null)
                targetFormat = OutputFormat.Wem;

            if (sourceFormat != targetFormat && sourceFormat == OutputFormat.Wem)
            {
                if (!Directory.Exists("Processing")) Directory.CreateDirectory("Processing");

                File.WriteAllBytes("./Processing/tmp.wem", data);
                var output = RunShellCommand("Tools\\vgmstream-win\\vgmstream-cli -o ./Processing/tmp.wav ./Processing/tmp.wem");
                data = File.ReadAllBytes("./Processing/tmp.wav");
                File.Delete("./Processing/tmp.wem");
                File.Delete("./Processing/tmp.wav");
                sourceFormat = OutputFormat.Wav;
            }

            if (sourceFormat != targetFormat && sourceFormat == OutputFormat.Wav)
            {
                var codec = "libvorbis";

                switch (targetFormat)
                {
                    case OutputFormat.Ogg:
                        codec = "libvorbis";
                        break;
                    default:
                        StatusTextBox.SelectionColor = Color.Red;
                        StatusTextBox.AppendText($"[WARN] Invalid target format {targetFormat}" + Environment.NewLine);
                        break;
                }

                var input = new MemoryStream(data);
                var output = new MemoryStream();
                FFMpegArguments
                    .FromPipeInput(new StreamPipeSource(input))
                    .OutputToPipe(new StreamPipeSink(output), options => options
                        .WithAudioCodec(codec)
                        .WithCustomArgument("-q 10.0")
                        .WithCustomArgument("-map_metadata -1")
                        .ForceFormat("ogg"))
                    .ProcessSynchronously();
                input.Close();
                data = output.ToArray();
                output.Close();

                sourceFormat = targetFormat;
            }

            convertedFormat = sourceFormat.Value;
            return data;
        }

        public static string RunShellCommand(string command)
        {
            var shellPath = $@"{Environment.SystemDirectory}\cmd.exe";
            var shellArg = $"/C {command}";

            var startInfo = new ProcessStartInfo(shellPath, shellArg);
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;

            var process = new Process();
            process.StartInfo = startInfo;

            process.Start();

            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            process.Dispose();

            return result;
        }

        public string GetPckExternalOutputPath(string filepath, FileTable.FileEntry entry, OutputFormat format)
        {
            var outputPath = AppVariables.OutputDirectoryWem;

            if (SplitOutputCheckBox.Checked)
            {
                var pckName = Path.GetFileNameWithoutExtension(filepath);
                outputPath = Path.Join(outputPath, pckName);

            }

            var name = GetPckExternalName(entry.FileId, out var hasLanguage);

            if (!hasLanguage && (!NoLangCheckBox.Checked || entry.Parent.Parent.LanguagesMap.Languages.Count > 1))
            {
                var language = entry.GetLanguage();
                outputPath = Path.Join(outputPath, language);
            }
            return Path.Join(outputPath, name + GetFileExtension(format));
        }

        public string GetPckStreamOutputPath(FileTable.FileEntry entry, OutputFormat format)
        {
            var outputPath = AppVariables.OutputDirectoryWem;

            if (SplitOutputCheckBox.Checked)
            {
                foreach (var filepath in AppVariables.InputFiles)
                {
                    var pckName = Path.GetFileNameWithoutExtension(filepath);
                    outputPath = Path.Join(outputPath, pckName);
                }
            }

            var name = GetPckStreamName(entry.FileId, out var hasLanguage);

            if (!hasLanguage && (!NoLangCheckBox.Checked || entry.Parent.Parent.LanguagesMap.Languages.Count > 1))
            {
                var language = entry.GetLanguage();
                outputPath = Path.Join(outputPath, language);
            }

            return Path.Join(outputPath, name + GetFileExtension(format));
        }

        public string GetPckBnkWemOutputPath(DataIndexChunk.FileEntry entry, BankHeader header, OutputFormat format)
        {
            var outputPath = AppVariables.OutputDirectoryWem;

            if (SplitOutputCheckBox.Checked)
            {
                foreach (var filepath in AppVariables.InputFiles)
                {
                    var pckName = Path.GetFileNameWithoutExtension(filepath);
                    outputPath = Path.Join(outputPath, pckName);
                }
            }

            if (BankedOutputCheckBox.Checked)
            {
                outputPath = Path.Join(outputPath, header.SoundBankId.ToString());
            }

            var name = GetPckBnkWemName(entry.Id, out var hasLanguage);

            // An incredibly janky way of making sure languages are always selected.
            if (!hasLanguage && (!NoLangCheckBox.Checked || header.Parent.Package.LanguagesMap.Languages.Count > 1))
            {
                var language = header.Parent.Language;
                outputPath = Path.Join(outputPath, language);
            }
            /*if (!hasLanguage || NoLangCheckBox.Checked)
            {
                var language = header.Parent.Language;
                outputPath = Path.Join(outputPath, language);
            }*/

            return Path.Join(outputPath, name + GetFileExtension(format));
        }

        public string GetFileExtension(OutputFormat format)
        {
            switch (format)
            {
                case OutputFormat.Ogg:
                    return ".ogg";
                case OutputFormat.Wav:
                    return ".wav";
                case OutputFormat.Wem:
                    return ".wem";
                default:
                    return ".unknown_format.bin";
            }
        }

        //Other UI Elements Below
        private void WWiseAudioExtractorUI_Load(object sender, EventArgs e)
        {
            this.Invalidate();
            this.Refresh();
        }

        private void InputDirectoryTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void OutputDirectoryTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void KnownEventsTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void KnownFilenamesTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void WEMExportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (WEMExportRadioButton.Checked)
            {
                StatusTextBox.AppendText("Selected 'Export to WEM'." + Environment.NewLine);
                StatusTextBox.AppendText($"{AppVariables.OutputDirectoryWem}" + Environment.NewLine);
            }
            else
            {
                StatusTextBox.AppendText("Deselected 'Export to WEM'." + Environment.NewLine);
            }
            UpdateCanExportStatus();
        }

        private void WAVExportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (WAVExportRadioButton.Checked)
            {
                StatusTextBox.AppendText("Selected 'Export to WAV'." + Environment.NewLine);
                StatusTextBox.AppendText($"{AppVariables.OutputDirectoryWav}" + Environment.NewLine);
            }
            else
            {
                StatusTextBox.AppendText("Deselected 'Export to WAV'." + Environment.NewLine);
            }
            UpdateCanExportStatus();
        }

        private void OGGExportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (OGGExportRadioButton.Checked)
            {
                StatusTextBox.AppendText("Selected 'Export to OGG'." + Environment.NewLine);
                StatusTextBox.AppendText($"{AppVariables.OutputDirectoryOgg}" + Environment.NewLine);
            }
            else
            {
                StatusTextBox.AppendText("Deselected 'Export to OGG'." + Environment.NewLine);
            }
            UpdateCanExportStatus();
        }

        // Decides whether to put the output depending on pck or bnk
        // No split:
        // /output/14245325.wem
        // Split:
        // /output/Banks/14245325.wem
        private void SplitOutputCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SplitOutputCheckBox.Checked)
            {
                StatusTextBox.AppendText("Selected 'Split Output'." + Environment.NewLine);
            }
            else
            {
                StatusTextBox.AppendText("Deselected 'Split Output'." + Environment.NewLine);
            }
        }

        // Decides on whether to split using embedded banks, useless without split
        // No split and banked:
        // /output/14245325.wem
        // Split and banked:
        // /output/Banks/5241236/14245325.wem
        private void BankedOutputCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (BankedOutputCheckBox.Checked)
            {
                StatusTextBox.AppendText("Selected 'Banked Output'." + Environment.NewLine);
            }
            else
            {
                StatusTextBox.AppendText("Deselected 'Banked Output'." + Environment.NewLine);
            }
        }

        // Changes how the pck stream and pck bnk wems are named from using the
        // default "x8" format to just "d"
        private void LegacyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (LegacyCheckBox.Checked)
            {
                StatusTextBox.AppendText("Selected 'Legacy Output'." + Environment.NewLine);
            }
            else
            {
                StatusTextBox.AppendText("Deselected 'Legacy Output'." + Environment.NewLine);
            }
        }

        // Defines if we should add a language folder if there is only one language
        // or not
        private void NoLangCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NoLangCheckBox.Checked)
            {
                StatusTextBox.AppendText("Selected 'NoLang'." + Environment.NewLine);
            }
            else
            {
                StatusTextBox.AppendText("Deselected 'NoLang'." + Environment.NewLine);
            }
        }

        // Will generate a file to be used in the 'Genshin Sound Catalogue' Spreadsheet
        private void SpreadsheetOutputCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SpreadsheetOutputCheckBox.Checked)
            {
                StatusTextBox.AppendText("Selected 'Spreadsheet Output'." + Environment.NewLine);
            }
            else
            {
                StatusTextBox.AppendText("Deselected 'Spreadsheet Output'." + Environment.NewLine);
            }

        }

        private void StatusTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CurrentProgressBar_Click(object sender, EventArgs e)
        {

        }

        private void TotalProgressBar_Click(object sender, EventArgs e)
        {

        }

        private void ParametersGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void ProcessingGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void HelpMenu_Click(object sender, EventArgs e)
        {
            string message = "Select input files of either type '.pck' or '.bnk', choose an output directory and whether or not you wish to use a generated 'known_filenames.tsv'.\nOutput format and settings depend on use-case.\n\nExample:\nSelecting 'WEM' and 'Split Output' will extract the WEM files into folders corresponding to the name of the input '.pck' file.";
            string title = "About";
            MessageBox.Show(message, title);
        }

        private void AboutMenu_Click(object sender, EventArgs e)
        {
            string message = "Extraction tool for WWise audio packages (.pck) and banks (.bnk).\n\nCan output to WEM, split WEM, WAV or OGG.";
            string title = "About";
            MessageBox.Show(message, title);
        }
    }
}
