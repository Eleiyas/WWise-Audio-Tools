namespace WWise_Audio_Tools.Classes.BankClasses
{
    public static class BankChunkMagics
    {
        public const uint BKHD_MAGIC = 0x44484B42;
        public const uint PLAT_MAGIC = 0x54414c50;
        public const uint INIT_MAGIC = 0x54494e49;
        public const uint DIDX_MAGIC = 0x58444944;
        public const uint ENVS_MAGIC = 0x53564e45;
        public const uint DATA_MAGIC = 0x41544144;
        public const uint HIRC_MAGIC = 0x43524948;
        public const uint STID_MAGIC = 0x44495453;
        public const uint STMG_MAGIC = 0x474d5453;
    }

    public enum HircType
    {
        None = 0,
        State = 1,
        Sound = 2,
        Action = 3,
        Event = 4,
        RanSequenceCounter = 5,
        SwitchCounter = 6,
        ActorMixer = 7,
        Bus = 8,
        LayerCounter = 9,
        Unknown_0 = 10,
        Unknown_1 = 11,
        Unknown_2 = 12,
        Unknown_3 = 13,
        Attenuation = 14,
        DialogueEvent = 15,
        FxShareSet = 16,
        FxCustom = 17,
        AuxBus = 18,
        LFOModulator = 19,
        EnvelopeModulator = 20,
        AudioDevice = 21,
        TimeModulator = 22,
    }

}
