using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Text;
using WWise_Audio_Tools.Classes.AppClasses;
using Newtonsoft.Json;

namespace WWise_Audio_Tools.Classes.PackageClasses
{
    public class Package
    {
        public PackageHeader Header = new PackageHeader();
        public LanguagesMap LanguagesMap = new LanguagesMap();
        public FileTable BanksTable = new FileTable();
        public FileTable StreamsTable = new FileTable();
        public FileTable ExternalsTable = new FileTable();

        private string? _filePath;
        private BinaryReader _reader;

        public Package(string filename)
        {
            _filePath = filename;

            _reader = new BinaryReader(new MemoryStream(File.ReadAllBytes(filename)));

            Read(_reader);
        }

        public Package(byte[] data)
        {
            _reader = new BinaryReader(new MemoryStream(data));

            Read(_reader);
        }

        public Package(BinaryReader reader)
        {
            _reader = reader;

            Read(reader);
        }

        public void Read(BinaryReader reader)
        {
            Header.Read(reader);
            LanguagesMap.Read(reader);

            BanksTable.Parent = this;
            StreamsTable.Parent = this;
            ExternalsTable.Parent = this;

            BanksTable.Read(reader);
            StreamsTable.Read(reader);
            ExternalsTable.Read(reader, true);
        }

        public string GetBankPath(FileTable.FileEntry entry)
        {
            var language = LanguagesMap.Languages[entry.LanguageId];
            return $"{language}/{entry.FileId}.bnk";
        }

        public string GetPath(FileTable.FileEntry entry)
        {
            var language = LanguagesMap.Languages[entry.LanguageId];
            return $"{language}/{entry.FileId}";
        }

        public string GetDirectory(FileTable.FileEntry entry)
        {
            var language = LanguagesMap.Languages[entry.LanguageId];
            return $"{language}/";
        }

        public byte[] GetBytes(FileTable.FileEntry entry)
        {
            var old = _reader.BaseStream.Position;

            _reader.BaseStream.Position = entry.StartingBlock;
            var data = _reader.ReadBytes((int)entry.FileSize);

            _reader.BaseStream.Position = old;

            return data;
        }

        public string Summary()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"=====================");
            builder.AppendLine($"PCK File Summary Info");
            if (_filePath is not null)
                builder.AppendLine($"      Filepath : {_filePath}");
            builder.AppendLine($"      Filesize : {_reader.BaseStream.Length / 1024 / 1024}mb");
            builder.AppendLine($"       Version : {Header.Version}");
            builder.AppendLine($"     Languages :");
            foreach (var id in LanguagesMap.Languages.Keys.ToImmutableSortedSet())
                builder.AppendLine($"         {id} : {LanguagesMap.Languages[id].ToUpper()}");
            builder.AppendLine($"    Bank Count : {BanksTable.Files.Count}");
            builder.AppendLine($"  Stream Count : {StreamsTable.Files.Count}");
            builder.AppendLine($"External Count : {ExternalsTable.Files.Count}");
            builder.AppendLine($"=====================");

            return builder.ToString();
        }
    }

    public class PackageHeader
    {
        public uint Signature;
        public uint HeaderSize;
        public uint Version;

        // Technically not considered a part of the header by the original code but I wanted to count it in
        public uint LangMapSize;
        public uint BanksTableSize;
        public uint SteamsTableSize;
        public uint ExternalsTableSize;

        public void Read(BinaryReader reader)
        {
            Signature = reader.ReadUInt32();
            HeaderSize = reader.ReadUInt32();
            Version = reader.ReadUInt32();

            LangMapSize = reader.ReadUInt32();
            BanksTableSize = reader.ReadUInt32();
            SteamsTableSize = reader.ReadUInt32();
            ExternalsTableSize = reader.ReadUInt32();

            if (Signature != PackageChunkMagics.AKPK_MAGIC)
                throw new InvalidDataException("File is not a valid .pck file! It is missing the AKPK header!");
        }
    }

    public class LanguagesMap
    {
        public Dictionary<uint, string> Languages = new Dictionary<uint, string>();

        private class StringEntry
        {
            public uint Offset;
            public uint Id;
        }

        public void Read(BinaryReader reader)
        {
            var start = reader.BaseStream.Position;
            var end = start;

            var count = reader.ReadInt32();

            var entries = new List<StringEntry>();

            for (var i = 0; i < count; i++)
                entries.Add(new StringEntry { Offset = reader.ReadUInt32(), Id = reader.ReadUInt32() });

            // Some wwise outputs use chars instead of wide chars so we need to check for this
            var str = reader.ReadBytes(2);
            reader.BaseStream.Position -= 2;
            var isWide = str[1] == 0 ? true : false;

            for (var i = 0; i < count; i++)
            {
                reader.BaseStream.Position = start + entries[i].Offset;

                if (isWide)
                    Languages.Add(entries[i].Id, reader.ReadSpacedStringToNull());
                else
                    Languages.Add(entries[i].Id, reader.ReadStringToNull());

                end = Math.Max(end, reader.BaseStream.Position);
            }

            reader.BaseStream.Position = end;
            reader.AlignStream();
        }
    }

    public class FileTable
    {
        public List<FileEntry> Files = new List<FileEntry>();

        [JsonIgnore] public Package Parent;

        public class FileEntry
        {
            public ulong FileId;
            public uint BlockSize;
            public uint FileSize;
            public uint StartingBlock; // File Offset
            public uint LanguageId;

            [JsonIgnore] public FileTable Parent;

            public FileEntry() { }

            public FileEntry(BinaryReader reader, bool is64 = false)
            {
                Read(reader, is64);
            }

            public void Read(BinaryReader reader, bool is64 = false)
            {
                if (is64)
                    FileId = reader.ReadUInt64();
                else
                    FileId = reader.ReadUInt32();
                BlockSize = reader.ReadUInt32();
                FileSize = reader.ReadUInt32();
                StartingBlock = reader.ReadUInt32() * BlockSize; //header offset multiplier, so this works with ZZZ
                LanguageId = reader.ReadUInt32();
            }

            public string GetLanguage()
            {
                return Parent.Parent.LanguagesMap.Languages[LanguageId];
            }
        }

        public void Read(BinaryReader reader, bool is64 = false)
        {
            var count = reader.ReadUInt32();

            for (var i = 0; i < count; i++)
            {
                var entry = new FileEntry(reader, is64);
                entry.Parent = this;
                Files.Add(entry);
            }
        }
    }

}
