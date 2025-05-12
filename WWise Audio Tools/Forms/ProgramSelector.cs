using System;
using System.Windows.Forms;

namespace WWise_Audio_Tools.Forms
{
    public partial class ProgramSelector : Form
    {
        public ProgramSelector()
        {
            InitializeComponent();
        }

        private void CreditsMenu_Click(object sender, EventArgs e)
        {
            string message = "Huge thanks to:\n\n- AdituV: \nInitial breakthrough of extracting Genshin Impact audio files and the coding of the initial extraction tools.\n\n- Cecilio: \nCoding of newer, improved tools and helping to fix my messy code.\n\n- Dimbreath: \nHelping to fix broken features.\n\n- Dvingerh: \nA lot of code used was modified from the \"Genshin Audio Exporter\".\n\n- Echoblast: \nA lot of help in regards to modifying existing tools to do other functions.\n\n- Honey: \nGiving general pointers and advice as to how Genshin Impact's audio systems functioned.\n\n- Leo_Chan: \nHelping to fix broken features and make sense of the code.\n\n- Ninjamask: \nAssisting in audio extraction each patch and assisting with the upkeep of the 'Genshin Sound Catalogue' Google Sheet.";
            string title = "Credits";
            MessageBox.Show(message, title);
        }

        private void WWiseAudioExtractorButton_Click(object sender, EventArgs e)
        {
            WWiseAudioExtractor form = new WWiseAudioExtractor();
            form.Show();
        }

        private void FNVHasherButton_Click(object sender, EventArgs e)
        {
            FNVHasher form = new FNVHasher();
            form.Show();
        }

        private void VoiceItemsCollator_Click(object sender, EventArgs e)
        {
            VoiceItemsCollator form = new VoiceItemsCollator();
            form.Show();
        }

        private void FNVFileListGenerator_Click(object sender, EventArgs e)
        {
            FNVFileListGenerator form = new FNVFileListGenerator();
            form.Show();
        }

        private void ProgramSelector_Load(object sender, EventArgs e)
        {

        }
    }
}
