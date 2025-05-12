using System;
using System.Windows.Forms;
using FFMpegCore;
using WWise_Audio_Tools.Forms;

namespace WWise_Audio_Tools
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // need to specify the ffmpeg binary folder.
            GlobalFFOptions.Configure(new FFOptions { BinaryFolder = "Tools/ffmpeg-master-latest-win64-gpl-shared/bin", TemporaryFilesFolder = "Processing" });
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProgramSelector());
        }
    }
}
