using System.Collections.Generic;

namespace WWise_Audio_Tools.Classes.AppClasses
{
    public static class AppVariables
    {
        private static string outputDirectory = "";

        private static string knownFilenamesPath = "";
        private static string knownEventsPath = "";

        private static bool exportWem = false, exportWav = false, exportOgg = false, exportSpreadsheet = false;

        private static bool overwriteExisting = true;

        private static List<string> inputFiles = new List<string>();
        private static List<string> wemFiles = new List<string>();
        private static List<string> wavFiles = new List<string>();

        public static string OutputDirectory { get; set; }
        public static string OutputDirectoryWem { get; set; }
        public static string OutputDirectoryWav { get; set; }
        public static string OutputDirectoryOgg { get; set; }

        public static string KnownFilenamesPath { get => knownFilenamesPath; set => knownFilenamesPath = value; }
        public static string KnownEventsPath { get => knownEventsPath; set => knownEventsPath = value; }

        public static bool ExportWem { get => exportWem; set => exportWem = value; }
        public static bool ExportWav { get => exportWav; set => exportWav = value; }
        public static bool ExportOgg { get => exportOgg; set => exportOgg = value; }

        public static bool OverwriteExisting { get => overwriteExisting; set => overwriteExisting = value; }

        public static List<string> InputFiles { get => inputFiles; set => inputFiles = value; }

        public static List<string> WemFiles { get => wemFiles; set => wemFiles = value; }
        public static List<string> WavFiles { get => wavFiles; set => wavFiles = value; }
    }
}
