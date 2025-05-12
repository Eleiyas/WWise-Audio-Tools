using System.IO;
using WWise_Audio_Tools.Classes.AppClasses;
using WWise_Audio_Tools.Classes.BankClasses.Chunks;
using WWise_Audio_Tools.Classes.PackageClasses;
using Newtonsoft.Json;

namespace WWise_Audio_Tools.Classes.BankClasses
{
    public class Bank
    {
        public BankHeader Header = new BankHeader();

        // Chunks, this assumes there can only be one chunk of each type
        public InitChunk? INITChunk;
        public CustomPlatformChunk? PLATChunk;
        public GlobalSettingsChunk? STMGChunk;
        public DataIndexChunk? DIDXChunk;
        public DataChunk? DATAChunk;
        public HircChunk? HIRCChunk;
        public EnvSettingsChunk? ENVSChunk;

        private string? _filePath;
        private BinaryReader _reader;

        [JsonIgnore] public string Language;
        [JsonIgnore] public Package Package;

        public Bank(string filename)
        {
            _filePath = filename;
            _reader = new BinaryReader(new MemoryStream(File.ReadAllBytes(filename)));
            Read(_reader);
        }

        public Bank(byte[] data)
        {
            _reader = new BinaryReader(new MemoryStream(data));
            Read(_reader);
        }

        public Bank(BinaryReader reader)
        {
            _reader = reader;
            Read(reader);
        }

        public void Read(BinaryReader reader)
        {
            Header.Parent = this;

            Header.Read(reader);

            while (true)
            {
                if (reader.BaseStream.Position + 1 >= reader.BaseStream.Length)
                    break;

                ParseChunk(reader);
            }
        }

        public void ParseChunk(BinaryReader reader)
        {
            var signature = reader.PeekUInt32();
            var old = reader.BaseStream.Position + 8;

            switch (signature)
            {
                case BankChunkMagics.DIDX_MAGIC:
                    {
                        DIDXChunk = new DataIndexChunk();
                        DIDXChunk.Read(reader);
                        reader.BaseStream.Position = old + DIDXChunk.ChunkSize;
                        break;
                    }
                case BankChunkMagics.DATA_MAGIC:
                    {
                        DATAChunk = new DataChunk();
                        DATAChunk.Read(reader);
                        reader.BaseStream.Position = old + DATAChunk.ChunkSize;
                        break;
                    }
                case BankChunkMagics.INIT_MAGIC:
                    {
                        INITChunk = new InitChunk();
                        INITChunk.Read(reader);
                        reader.BaseStream.Position = old + INITChunk.ChunkSize;
                        break;
                    }
                case BankChunkMagics.STMG_MAGIC:
                    {
                        STMGChunk = new GlobalSettingsChunk();
                        STMGChunk.Read(reader);
                        reader.BaseStream.Position = old + STMGChunk.ChunkSize;
                        break;
                    }
                case BankChunkMagics.ENVS_MAGIC:
                    {
                        ENVSChunk = new EnvSettingsChunk();
                        ENVSChunk.Read(reader);
                        reader.BaseStream.Position = old + ENVSChunk.ChunkSize;
                        break;
                    }
                case BankChunkMagics.PLAT_MAGIC:
                    {
                        PLATChunk = new CustomPlatformChunk();
                        PLATChunk.Read(reader);
                        reader.BaseStream.Position = old + PLATChunk.ChunkSize;
                        break;
                    }
                case BankChunkMagics.HIRC_MAGIC:
                    {
                        HIRCChunk = new HircChunk();
                        HIRCChunk.Read(reader);
                        reader.BaseStream.Position = old + HIRCChunk.ChunkSize;
                        break;
                    }
                default:
                    {
                        var chunk = new Chunk();
                        chunk.Read(reader);
                        reader.BaseStream.Position = old + chunk.ChunkSize;
                        break;
                    }
            }
        }
    }

    public class BankHeader
    {
        public uint Signature;
        public uint HeaderSize;
        public uint Version;

        public uint SoundBankId;
        public uint LanguageId;
        public ushort Alignment;
        public ushort DeviceAllocated;
        public uint ProjectId;

        [JsonIgnore] public Bank Parent;

        public void Read(BinaryReader reader)
        {
            Signature = reader.ReadUInt32();
            HeaderSize = reader.ReadUInt32();

            var old = reader.BaseStream.Position;

            Version = reader.ReadUInt32();

            SoundBankId = reader.ReadUInt32();
            LanguageId = reader.ReadUInt32();
            Alignment = reader.ReadUInt16();
            DeviceAllocated = reader.ReadUInt16();
            ProjectId = reader.ReadUInt32();

            if (Signature != BankChunkMagics.BKHD_MAGIC)
                throw new InvalidDataException("File is not a valid .bnk file! It is missing the BKHD header!");

            reader.BaseStream.Position = old + HeaderSize;
        }
    }
}