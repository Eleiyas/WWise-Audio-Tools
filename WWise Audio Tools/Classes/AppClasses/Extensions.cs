using System;
using System.IO;
using System.Linq;

namespace WWise_Audio_Tools.Classes.AppClasses
{
    public static class Extensions
    {
        public static string DetermineFileExtension(this byte[] array)
        {

            if (array.Length < 4)
            {
                return null;
            }

            var magic = BitConverter.ToUInt32(array.Take(4).Reverse().ToArray());

            switch (magic)
            {
                case 0x424B4844:
                    return ".bnk";
                case 0x414B504B:
                    return ".pck";
                case 0x52494646:
                    return ".wem";
            }

            return ".bin";
        }

        public static string ReadStringToNull(this BinaryReader reader)
        {
            var value = "";

            char chr;
            while ((chr = reader.ReadChar()) != 0)
            {
                value += chr;
            }

            return value;
        }

        public static string ReadSpacedStringToNull(this BinaryReader reader)
        {
            var value = "";

            ushort chr;
            while ((chr = reader.ReadUInt16()) != 0)
            {
                value += (char)chr;
            }

            return value;
        }

        public static uint PeekUInt32(this BinaryReader reader)
        {
            var value = reader.ReadUInt32();
            reader.BaseStream.Position -= 4;
            return value;
        }

        // Taken from github.com/Perfare/AssetStudio
        public static void AlignStream(this BinaryReader reader)
        {
            reader.AlignStream(4);
        }

        public static void AlignStream(this BinaryReader reader, int alignment)
        {
            var pos = reader.BaseStream.Position;
            var mod = pos % alignment;
            if (mod != 0)
            {
                reader.BaseStream.Position += alignment - mod;
            }
        }
    }
}
