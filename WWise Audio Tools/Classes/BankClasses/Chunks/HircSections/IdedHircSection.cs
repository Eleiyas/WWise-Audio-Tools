using System.IO;

namespace WWise_Audio_Tools.Classes.BankClasses.Chunks.HircSections
{
    public class IdedHircSection : HircSection
    {
        public uint Id;

        public override void Read(BinaryReader reader)
        {
            base.Read(reader);

            Id = reader.ReadUInt32();
        }
    }
}
