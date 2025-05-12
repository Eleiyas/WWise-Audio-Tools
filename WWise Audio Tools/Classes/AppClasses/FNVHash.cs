namespace WWise_Audio_Tools.Classes.AppClasses
{
    public static class FNVHash
    {

        public struct Hash32
        {
            public delegate uint HashType();
            public delegate uint SizeType();

            public static uint Bits() { return 32; }
            public static uint Prime() { return 16777619; }
            public const uint s_offsetBasis = 2166136261U;
        };

        public struct Hash30
        {
            public static new uint Bits() { return 30; }
        };

        public struct Hash64
        {
            public delegate ulong HashType();
            public delegate ulong SizeType();

            public static uint Bits() { return 64; }
            public static ulong Prime() { return 1099511628211UL; }
            public const ulong s_offsetBasis = 14695981039346656037UL;
        };

        public class Fnv32
        {
            public static uint Compute(string data)
            {
                var hash = 0x811c9dc5;

                for (var i = 0; i < data.Length; i++)
                    hash = (0x1000193 * hash) ^ data[i];

                return hash;
            }

            public static uint ComputeLowerCase(string data)
            {
                data = data.ToLower();

                return Compute(data);
            }
        }

        public class Fnv64
        {
            public static ulong Compute(string data)
            {
                var hash = 0xCBF29CE484222325;

                for (var i = 0; i < data.Length; i++)
                    hash = (0x100000001B3 * hash) ^ data[i];

                return hash;
            }

            public static ulong ComputeLowerCase(string data)
            {
                data = data.ToLower();

                return Compute(data);
            }
        }
    }
}


