using System.IO;

namespace WWise_Audio_Tools.Classes.BankClasses.Chunks.HircSections
{
    public class HircSection
    {
        public HircType Type = HircType.None;
        public uint SectionSize;

        public virtual void Read(BinaryReader reader)
        {
            Type = (HircType)reader.ReadByte();
            SectionSize = reader.ReadUInt32();
        }
    }
}
