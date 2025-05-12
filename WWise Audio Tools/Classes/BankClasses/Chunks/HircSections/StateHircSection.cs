using System.IO;

namespace WWise_Audio_Tools.Classes.BankClasses.Chunks.HircSections
{
    public class StateHircSection : IdedHircSection
    {
        public override void Read(BinaryReader reader)
        {
            base.Read(reader);

            // CAkState::SetInitialValues
        }
    }
}
