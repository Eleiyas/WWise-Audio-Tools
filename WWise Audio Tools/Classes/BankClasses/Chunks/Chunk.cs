using System;
using System.Collections.Generic;
using System.IO;
using WWise_Audio_Tools.Classes.AppClasses;
using Newtonsoft.Json;

namespace WWise_Audio_Tools.Classes.BankClasses.Chunks
{
    public class Chunk
    {
        [JsonIgnore]
        public uint Signature;
        [JsonIgnore]
        public uint ChunkSize;

        public virtual void Read(BinaryReader reader)
        {
            Signature = reader.ReadUInt32();
            ChunkSize = reader.ReadUInt32();
        }
    }

    public class InitChunk : Chunk
    {
        public Dictionary<uint, string> Plugins = new Dictionary<uint, string>();

        public override void Read(BinaryReader reader)
        {
            base.Read(reader);

            var pluginCount = reader.ReadUInt32();

            for (var i = 0; i < pluginCount; i++)
            {
                var pluginId = reader.ReadUInt32();
                var nameSize = reader.ReadUInt32();
                var name = reader.ReadStringToNull();

                Plugins.Add(pluginId, name);
            }
        }
    }

    public class CustomPlatformChunk : Chunk
    {
        public string Platform;

        public override void Read(BinaryReader reader)
        {
            base.Read(reader);

            reader.ReadUInt32(); // String length
            Platform = reader.ReadStringToNull();
        }
    }

    public class EnvSettingsChunk : Chunk
    {
        public class Curve
        {
            public bool Enabled;
            public byte Scaling; // Enum exists : AkCurveScaling
            public ushort PointCount;

            public Curve(BinaryReader reader)
            {
                Read(reader);
            }

            public void Read(BinaryReader reader)
            {
                Enabled = reader.ReadBoolean();
                Scaling = reader.ReadByte();
                PointCount = reader.ReadUInt16();
                var pointData = reader.ReadBytes(PointCount); // TODO: Need to parse curve
            }
        }

        public Dictionary<uint, Dictionary<uint, Curve>> Curves = new Dictionary<uint, Dictionary<uint, Curve>>();

        public override void Read(BinaryReader reader)
        {
            base.Read(reader);

            for (uint x = 0; x < 2; x++)
            {
                Curves.Add(x, new Dictionary<uint, Curve>());

                for (uint y = 0; y < 3; y++)
                {
                    var curve = new Curve(reader);
                    Curves[x].Add(y, curve);
                }
            }
        }
    }

    public class GlobalSettingsChunk : Chunk
    {
        public class StateGroup
        {
            public class Transition
            {
                public uint StateId1;
                public uint StateId2;
                public int TransitionTime;

                public Transition(BinaryReader reader)
                {
                    Read(reader);
                }

                public void Read(BinaryReader reader)
                {
                    StateId1 = reader.ReadUInt32();
                    StateId2 = reader.ReadUInt32();
                    TransitionTime = reader.ReadInt32();
                }
            }

            public uint Id;
            public int TransitionTime;
            public List<Transition> Transitions = new List<Transition>();

            public StateGroup(BinaryReader reader)
            {
                Read(reader);
            }

            public void Read(BinaryReader reader)
            {
                Id = reader.ReadUInt32();
                TransitionTime = reader.ReadInt32();

                var count = reader.ReadUInt32();

                for (var i = 0; i < count; i++)
                {
                    var transition = new Transition(reader);
                    Transitions.Add(transition);
                }
            }
        }

        public class SwitchGroup
        {
            public uint Id;

            public SwitchGroup(BinaryReader reader)
            {
                Read(reader);
            }

            public void Read(BinaryReader reader)
            {
                Id = reader.ReadUInt32();

                // TODO : Parse RTPC
                reader.ReadUInt32(); // rtpcId
                reader.ReadByte(); // rtpcType
                var numGraphPts = reader.ReadUInt32();
                reader.ReadBytes((int)numGraphPts * 12); // graphPts
            }
        }

        public class AcousticTexture
        {
            public uint Id;
            public float AbsorptionOffset;
            public float AbsorptionLow;
            public float AbsorptionMidLow;
            public float AbsorptionMidHigh;
            public float AbsorptionHigh;
            public float Scattering;

            public AcousticTexture(BinaryReader reader)
            {
                Read(reader);
            }

            public void Read(BinaryReader reader)
            {
                Id = reader.ReadUInt32();
                AbsorptionOffset = reader.ReadSingle();
                AbsorptionLow = reader.ReadSingle();
                AbsorptionMidLow = reader.ReadSingle();
                AbsorptionMidHigh = reader.ReadSingle();
                AbsorptionHigh = reader.ReadSingle();
                Scattering = reader.ReadSingle();
            }
        }

        public float VolumeThreshold;
        public ushort MaxNumVoicesLimit;
        public ushort MaxNumDangerousVirtVoicesLimit;
        public List<StateGroup> StateGroups = new List<StateGroup>();
        public List<SwitchGroup> SwitchGroups = new List<SwitchGroup>();
        public List<AcousticTexture> AcousticTextures = new List<AcousticTexture>();

        public override void Read(BinaryReader reader)
        {
            base.Read(reader);

            VolumeThreshold = reader.ReadSingle();
            MaxNumVoicesLimit = reader.ReadUInt16();
            MaxNumDangerousVirtVoicesLimit = reader.ReadUInt16();

            var stateGroupCount = reader.ReadUInt32();
            for (var i = 0; i < stateGroupCount; i++)
            {
                var stateGroup = new StateGroup(reader);
                StateGroups.Add(stateGroup);
            }

            var switchGroupCount = reader.ReadUInt32();
            for (var i = 0; i < switchGroupCount; i++)
            {
                var switchGroup = new SwitchGroup(reader);
                SwitchGroups.Add(switchGroup);
            }

            // TODO : Parse RTPC
            var rptcParamCount = reader.ReadUInt32();
            for (var i = 0; i < rptcParamCount; i++)
            {
                var rtpcId = reader.ReadUInt32();
                var defaultParamBalue = reader.ReadSingle();
                var rampingType = reader.ReadUInt32(); // Enum exists for this: AkTransitionRampingType
                var rampUp = reader.ReadSingle();
                var rampDown = reader.ReadSingle();
                var addBuiltInParamBinding = reader.ReadBoolean();
            }

            var acousticTextureCount = reader.ReadUInt32();
            for (var i = 0; i < acousticTextureCount; i++)
            {
                var texture = new AcousticTexture(reader);
                AcousticTextures.Add(texture);
            }
        }
    }

    public class DataChunk : Chunk
    {
        private BinaryReader data;

        public override void Read(BinaryReader reader)
        {
            base.Read(reader);

            data = new BinaryReader(new MemoryStream(reader.ReadBytes((int)ChunkSize)));
        }

        public byte[] GetFile(DataIndexChunk.FileEntry entry)
        {
            data.BaseStream.Position = entry.Offset;
            return data.ReadBytes((int)entry.Length);
        }
    }

    /*public class DataIndexChunk : Chunk
    {
        public class FileEntry
        {
            public uint Id;
            public uint Offset;
            public uint Length;

            public FileEntry(BinaryReader reader)
            {
                Read(reader);
            }

            public void Read(BinaryReader reader)
            {
                Id = reader.ReadUInt32();
                Offset = reader.ReadUInt32();
                Length = reader.ReadUInt32();
            }
        }

        public Dictionary<uint, FileEntry> Files = new Dictionary<uint, FileEntry>();

        public override void Read(BinaryReader reader)
        {
            base.Read(reader);

            var count = ChunkSize / 12;

            for (var i = 0; i < count; i++)
            {
                var entry = new FileEntry(reader);
                Files.Add(entry.Id, entry);
            }
        }
    }*/

    public class DataIndexChunk : Chunk
    {
        public class FileEntry
        {
            public uint Id;
            public uint Offset;
            public uint Length;

            public FileEntry(BinaryReader reader)
            {
                Read(reader);
            }

            public void Read(BinaryReader reader)
            {
                Id = reader.ReadUInt32();
                Offset = reader.ReadUInt32();
                Length = reader.ReadUInt32();
            }
        }

        public Dictionary<uint, List<FileEntry>> Files = new Dictionary<uint, List<FileEntry>>();

        public override void Read(BinaryReader reader)
        {
            base.Read(reader);

            var count = ChunkSize / 12;

            for (var i = 0; i < count; i++)
            {
                var entry = new FileEntry(reader);
                if (!Files.ContainsKey(entry.Id))
                {
                    Files[entry.Id] = new List<FileEntry>();
                }
                Files[entry.Id].Add(entry);
            }
        }
    }

    public class HircChunk : Chunk
    {
        public override void Read(BinaryReader reader)
        {
            base.Read(reader);

            var sectionCount = reader.ReadUInt32();

            for (var i = 0; i < sectionCount; i++)
            {
                var hircType = reader.ReadByte();
                var sectionSize = reader.ReadUInt32();
                var old = reader.BaseStream.Position;

                Console.WriteLine($"HIRC Section {hircType} {(HircType)hircType}");

                reader.BaseStream.Position += sectionSize;
            }
        }
    }


}
