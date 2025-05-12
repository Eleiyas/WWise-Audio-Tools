using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Newtonsoft.Json;
using WWiseAudioExtractor.Reference.AkBank;
using WWiseAudioExtractor;
using WWiseAudioExtractor.Classes.AppClasses;



namespace WWiseAudioExtractor.Reference;

public class CAkBankMgr
{

    /*public void WriteStatus(string text, bool prefix = true)
    {
        StatusTextBox.AppendText($"{((text.Length > 0 && prefix) ? "> " + text : "  " + text)}" + Environment.NewLine);
    }

    public static AkBankHeader ProcessBankHeader(BinaryReader reader)
    {
        var signature = reader.ReadUInt32();
        var headerLength = reader.ReadUInt32();

        if (signature != 0x44484B42)
            throw new InvalidDataException("File input doesn't have the Bank header magic!");

        return new AkBankHeader
        {
            dwBankGeneratorVersion = reader.ReadUInt32(),
            dwSoundBankID = reader.ReadUInt32(),
            dwLanguageID = reader.ReadUInt32(),
            uAlignment = reader.ReadUInt16(),
            bDeviceAllocated = reader.ReadUInt16(),
            dwProjectID = reader.ReadUInt32(),
        };
    }

    public static void LoadBank(BinaryReader reader)
    {
        var header = ProcessBankHeader(reader);
        
        WriteStatus($"[BKHD] {JsonConvert.SerializeObject(header)}");

        while (true)
        {
            if (reader.BaseStream.Position + 1 >= reader.BaseStream.Length)
                break;

            var chunkIdent = reader.ReadUInt32();
            var chunkLength = reader.ReadUInt32();

            switch (chunkIdent)
            {
                case 0x54414c50: // PLAT
                    WriteStatus($"Unhandled PLAT {chunkLength}");
                    reader.ReadBytes((int)chunkLength);
                    break;
                case 0x54494e49: // INIT
                    WriteStatus($"Unhandled INIT {chunkLength}");
                    reader.ReadBytes((int)chunkLength);
                    break;
                case 0x58444944: // DIDX
                    LoadMediaIndex(reader, chunkLength);
                    break;
                case 0x53564e45: // ENVS
                    WriteStatus($"Unhandled ENVS {chunkLength}");
                    reader.ReadBytes((int)chunkLength);
                    break;
                case 0x41544144: // DATA
                    WriteStatus($"Unhandled DATA {chunkLength}");
                    reader.ReadBytes((int)chunkLength);
                    break;
                case 0x43524948: // HIRC
                    var old = reader.BaseStream.Position;
                    ProcessHircChunk(reader, header.dwSoundBankID);
                    reader.BaseStream.Position = old + chunkLength;
                    break;
                case 0x44495453: // STID
                    WriteStatus($"Unhandled STID {chunkLength}");
                    reader.ReadBytes((int)chunkLength);
                    break;
                case 0x474d5453: // STMG
                    WriteStatus($"Unhandled STMG {chunkLength}");
                    reader.ReadBytes((int)chunkLength);
                    break;
                default:
                    WriteStatus("Unknown Bank chunk for this Reader version, it will be ignored");
                    WriteStatus($"{chunkIdent.ToString("X8")} {chunkLength}");
                    reader.ReadBytes((int)chunkLength);
                    break;
            }
        }
    }
    
    public static Custom_MediaIndex LoadMediaIndex(BinaryReader reader, uint in_uIndexChunkSize)
    {
        var index = new Custom_MediaIndex
        {
            Data = reader.ReadBytes((int)in_uIndexChunkSize),
            Count = in_uIndexChunkSize / 12,
        };

        var r = new BinaryReader(new MemoryStream(index.Data));

        for (var i = 0; i < index.Count; i++)
        {
            WriteStatus($"0 {r.ReadUInt32()}");
            WriteStatus($"1 {r.ReadUInt32()}");
            WriteStatus($"2 {r.ReadUInt32()}");
        }

        return index;
    }

    public static void ProcessHircChunk(BinaryReader reader, uint in_dwBankID)
    {
        var count = reader.ReadUInt32();
        
        WriteStatus($"[HIRC] Count {count}");

        for (var i = 0; i < count; i++)
        {
            var section = new AKBKSubHircSection
            {
                eHircType = reader.ReadByte(),
                dwSectionSize = reader.ReadUInt32(),
            };
            
            WriteStatus($"[HIRC] Sub Section {section.eHircType} {section.dwSectionSize}");

            switch (section.eHircType)
            {
                case 1: // ReadState
                    reader.ReadBytes((int)section.dwSectionSize);
                    break;
                case 2: // ReadSourceParent<CAkSound>
                {
                    WriteStatus("[HIRC] [02] ReadSourceParent<CAkSound> TODO");
                    var old = reader.BaseStream.Position;
                    
                    var objectId = reader.ReadUInt32();
                    
                    WriteStatus($"[HIRC] [02] Object ID {objectId}");

                    { // CAkBankMgr::LoadSource
                        var m_PluginID = reader.ReadUInt32();
                        var v6 = reader.ReadByte();
                        var sourceID = reader.ReadUInt32(); // m_MediaInfo.sourceId
                        var uFileId = sourceID; // m_MediaInfo.uFileId
                        var uInMemoryMediaSize = reader.ReadUInt32(); // m_MediaInfo.uInMemoryMediaSize
                        var v5 = reader.ReadByte();
                        
                        WriteStatus(m_PluginID);
                        WriteStatus(v6);
                        WriteStatus(sourceID);
                        WriteStatus(uFileId);
                        WriteStatus(uInMemoryMediaSize);
                        WriteStatus(v5);

                        if ((m_PluginID & 0xf) == 2)
                        {
                            var m_uSize = reader.ReadUInt32();
                            var m_pParam = reader.ReadBytes((int)m_uSize);
                        }
                    }

                    { // CAkParameterNodeBase::SetNodeBaseParams
                        { // CAkParameterNode::SetInitialFxParams
                            var v6 = reader.ReadByte(); // Related to m_overriddenParams
                            
                            var fxCount = reader.ReadByte();
                            for (var j = 0; j < fxCount; j++)
                            {
                                var in_uFXIndex = reader.ReadChar();
                                var in_uID = reader.ReadUInt32();
                                var in_bShareSet = reader.ReadByte();
                                var v5 = reader.ReadByte(); // if true, it runs CAkParameterNodeBase::SetFx
                            }
                        }

                        { // CAkParameterNode::SetInitialMetadataParams
                            var v5 = reader.ReadByte(); // Related to m_overriddenParams

                            var metaCount = reader.ReadByte();
                            for (var j = 0; j < metaCount; j++)
                            {
                                var in_uFXIndex = reader.ReadByte();
                                var in_uID = reader.ReadUInt32();
                                var in_bShareSet = reader.ReadByte();
                            }
                        }

                        var _bf_dc = reader.ReadByte(); // some sort of weird flag logic | (8 * (AK::ReadBankData<unsigned char>(io_rpData, io_rulDataSize) & 1)) | *(this + 220) & 0xF7;
                        
                        var in_ID = reader.ReadUInt32();
                        
                        WriteStatus($"[HIRC] [02] 2nd Object ID {in_ID}");
                        
                        var v12 = reader.ReadUInt32();
                        var v5_ = reader.ReadByte();

                        { // CAkParameterNode::SetInitialParams
                            { // m_props
                                var propBundleCount = reader.ReadByte();
                                WriteStatus($"[HIRC] [02] m_props Count {propBundleCount}");

                                var propBundleKeys = new List<byte>(propBundleCount);
                                var propBundleValues = new List<uint>(propBundleCount);
                                var propBundle = new Dictionary<byte, uint>(propBundleCount);

                                for (var j = 0; j < propBundleCount; j++)
                                    propBundleKeys.Add(reader.ReadByte());
                                for (var j = 0; j < propBundleCount; j++)
                                    propBundleValues.Add(reader.ReadUInt32());
                                for (var j = 0; j < propBundleCount; j++)
                                    propBundle.Add(propBundleKeys[j], propBundleValues[j]);

                                WriteStatus(JsonConvert.SerializeObject(propBundle));
                            }

                            { // m_ranges : not sure if it should be a KeyValuePair
                                var propBundleCount = reader.ReadByte();
                                WriteStatus($"[HIRC] [02] m_ranges Count {propBundleCount}");

                                var propBundleKeys = new List<byte>(propBundleCount);
                                var propBundleValues = new List<KeyValuePair<uint, uint>>(propBundleCount);
                                var propBundle = new Dictionary<byte, KeyValuePair<uint, uint>>(propBundleCount);

                                for (var j = 0; j < propBundleCount; j++)
                                    propBundleKeys.Add(reader.ReadByte());
                                for (var j = 0; j < propBundleCount; j++)
                                    propBundleValues.Add(new KeyValuePair<uint, uint>(reader.ReadUInt32(), reader.ReadUInt32()));
                                for (var j = 0; j < propBundleCount; j++)
                                    propBundle.Add(propBundleKeys[j], propBundleValues[j]);

                                WriteStatus(JsonConvert.SerializeObject(propBundle));
                            }
                        }
                        
                        WriteStatus("[HIRC] [02] TODO : Continue with the rest of the source data");

                        { // CAkParameterNodeBase::SetPositioningParams
                            // var v6 = reader.ReadByte();
                            //
                            // if ((v6 & 1) != 0 && ((v6 >> 1) & 1) != 0)
                            // {
                            //     var v5 = reader.ReadByte();
                            // }
                        }

                        { // CAkSound::SetAuxParams
                            
                        }

                        { // CAkSound::SetAdvSettingsParams
                            
                        }

                        { // CAkSound::ReadStateChunk
                            
                        }

                        { // SetInitialRTPC<CAkParameterNodeBase>
                            
                        }
                    }
                    
                    if (reader.BaseStream.Position != section.dwSectionSize + old)
                    {
                        WriteStatus($"[HIRC] [02] Section read length mismatch! {section.dwSectionSize + old - reader.BaseStream.Position}");
                        reader.BaseStream.Position = old;
                        Directory.CreateDirectory($"./dumps/CAkSound/");
                        File.WriteAllBytes($"./dumps/CAkSound/{objectId}.bin", reader.ReadBytes((int)section.dwSectionSize));
                    }
                    
                    reader.BaseStream.Position = section.dwSectionSize + old;
                    break;
                }
                case 3: // ReadAction
                {
                    WriteStatus("[HIRC] [03] ReadAction");
                    var old = reader.BaseStream.Position;
                    
                    var objectId = reader.ReadUInt32();

                    var eActionType = reader.ReadUInt16();

                    WriteStatus($"[HIRC] [03] Object ID {objectId}");
                    WriteStatus($"[HIRC] [03] eActionType {eActionType}");
                    
                    { // CAkAction::SetInitialValues
                        { // ElementId
                            var id = reader.ReadUInt32();
                            var bIsBus = reader.ReadByte() != 0;
                            
                            WriteStatus($"[HIRC] [03] ElementId.Id {id}");
                            WriteStatus($"[HIRC] [03] ElementId.bIsBus {bIsBus}");
                        }
                        
                        { // m_props
                            var propBundleCount = reader.ReadByte();
                            WriteStatus($"[HIRC] [03] m_props Count {propBundleCount}");

                            var propBundleKeys = new List<byte>(propBundleCount);
                            var propBundleValues = new List<uint>(propBundleCount);
                            var propBundle = new Dictionary<byte, uint>(propBundleCount);

                            for (var j = 0; j < propBundleCount; j++)
                                propBundleKeys.Add(reader.ReadByte());
                            for (var j = 0; j < propBundleCount; j++)
                                propBundleValues.Add(reader.ReadUInt32());
                            for (var j = 0; j < propBundleCount; j++)
                                propBundle.Add(propBundleKeys[j], propBundleValues[j]);

                            WriteStatus(JsonConvert.SerializeObject(propBundle));
                        }

                        { // m_ranges : not sure if it should be a KeyValuePair
                            var propBundleCount = reader.ReadByte();
                            WriteStatus($"[HIRC] [03] m_ranges Count {propBundleCount}");

                            var propBundleKeys = new List<byte>(propBundleCount);
                            var propBundleValues = new List<KeyValuePair<uint, uint>>(propBundleCount);
                            var propBundle = new Dictionary<byte, KeyValuePair<uint, uint>>(propBundleCount);

                            for (var j = 0; j < propBundleCount; j++)
                                propBundleKeys.Add(reader.ReadByte());
                            for (var j = 0; j < propBundleCount; j++)
                                propBundleValues.Add(new KeyValuePair<uint, uint>(reader.ReadUInt32(), reader.ReadUInt32()));
                            for (var j = 0; j < propBundleCount; j++)
                                propBundle.Add(propBundleKeys[j], propBundleValues[j]);

                            WriteStatus(JsonConvert.SerializeObject(propBundle));
                        }

                        { // SetActionParams
                            WriteStatus("[HIRC] [03] [TODO] Work on SetActionParams");

                            switch (eActionType & 0xff00)
                            {
                                case 0x100:
                                { // CAkActionStop::Create
                                    var _bf_36 = reader.ReadByte();

                                    { // CAkActionStop::SetActionSpecificParams
                                        var bitFlag = reader.ReadByte();
                                        
                                        // Bit 0 - ApplyToStateTransitions
                                        // Bit 1 - ApplyToDynamicSequence
                                    }

                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                                case 0x200:
                                { // CAkActionPause::Create
                                    var _bf_36 = reader.ReadByte();

                                    { // CAkActionPause::SetActionSpecificParams
                                        var bitFlag = reader.ReadByte();
                                        
                                        // Bit 0 - IncludePendingResume
                                        // Bit 1 - ApplyToStateTransitions
                                        // Bit 2 - ApplyToDynamicSequence
                                    }

                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                                case 0x300:
                                { // CAkActionResume::Create
                                    var _bf_36 = reader.ReadByte();

                                    { // CAkActionResume::SetActionSpecificParams
                                        var bitFlag = reader.ReadByte();
                                        
                                        // Bit 0 - IsMasterResume
                                        // Bit 1 - ApplyToStateTransitions
                                        // Bit 2 - ApplyToDynamicSequence
                                    }

                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                                case 0x400:
                                { // CAkActionPlay::Create
                                    var _bf_36 = reader.ReadByte();
                                    var bankId = reader.ReadUInt32();
                                    WriteStatus($"[HIRC] [03] [0400] PlayAction BankID: {bankId} _bf_36 {_bf_36}");
                                    break;
                                }
                                case 0x500:
                                    WriteStatus("[HIRC] [03] Unknown Action Type! 0x500");
                                    break;
                                case 0x600: // Same as 0x700
                                case 0x700:
                                { // CAkActionMute::Create
                                    var _bf_36 = reader.ReadByte();
                                    
                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                                case 0x800: // Same as 0x900
                                case 0x900:
                                { // CAkActionSetAkProp::Create AkPropID_Pitch
                                    var _bf_36 = reader.ReadByte();

                                    { // CAkActionSetAkProp::SetActionSpecificParams
                                        var m_eValueMeaning = reader.ReadByte();
                                        var in_MidValue = reader.ReadSingle();
                                        var in_min = reader.ReadSingle();
                                        var in_max = reader.ReadSingle();
                                    }

                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                                case 0xA00:
                                { // CAkActionSetAkProp::Create AkPropID_Volume
                                    var _bf_36 = reader.ReadByte();

                                    { // CAkActionSetAkProp::SetActionSpecificParams
                                        var m_eValueMeaning = reader.ReadByte();
                                        var in_MidValue = reader.ReadSingle();
                                        var in_min = reader.ReadSingle();
                                        var in_max = reader.ReadSingle();
                                    }

                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                                case 0xB00:
                                { // CAkActionSetAkProp::Create AkPropID_Volume
                                    var _bf_36 = reader.ReadByte();

                                    { // CAkActionSetAkProp::SetActionSpecificParams
                                        var m_eValueMeaning = reader.ReadByte();
                                        var in_MidValue = reader.ReadSingle();
                                        var in_min = reader.ReadSingle();
                                        var in_max = reader.ReadSingle();
                                    }

                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                                case 0xC00: // Same as 0xD00
                                case 0xD00:
                                { // CAkActionSetAkProp::Create AkPropID_BusVolume
                                    var _bf_36 = reader.ReadByte();

                                    { // CAkActionSetAkProp::SetActionSpecificParams
                                        var m_eValueMeaning = reader.ReadByte();
                                        var in_MidValue = reader.ReadSingle();
                                        var in_min = reader.ReadSingle();
                                        var in_max = reader.ReadSingle();
                                    }

                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                                case 0xE00: // Same as 0xF00
                                case 0xF00:
                                { // CAkActionSetAkProp::Create AkPropID_LPF
                                    var _bf_36 = reader.ReadByte();

                                    { // CAkActionSetAkProp::SetActionSpecificParams
                                        var m_eValueMeaning = reader.ReadByte();
                                        var in_MidValue = reader.ReadSingle();
                                        var in_min = reader.ReadSingle();
                                        var in_max = reader.ReadSingle();
                                    }

                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                                case 0x1000: // Same as 0x1100
                                case 0x1100:
                                { // CAkActionUseState::Create
                                    break;
                                }
                                case 0x1200:
                                    WriteStatus("[HIRC] [03] Unknown Action Type! 0x1200");
                                    break;
                                case 0x1300:
                                case 0x1400:
                                { // CAkActionSetGameParameter::Create
                                    var _bf_36 = reader.ReadByte();

                                    { // CAkActionSetGameParameter::SetActionSpecificParams
                                        var bypassGameParameterInternalTransition = reader.ReadByte();
                                        var m_eValueMeaning = reader.ReadByte();
                                        var in_MidValue = reader.ReadSingle();
                                        var in_min = reader.ReadSingle();
                                        var in_max = reader.ReadSingle();
                                    }

                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                                case 0x1500:
                                case 0x1600:
                                    WriteStatus("[HIRC] [03] TODO : CAkActionEvent::Create");
                                    break; // CAkActionEvent::Create
                                case 0x1700:
                                    WriteStatus("[HIRC] [03] TODO : CAkActionEvent::Create");
                                    break; // CAkActionEvent::Create
                                case 0x1800:
                                    WriteStatus("[HIRC] [03] Unknown Action Type! 0x1800");
                                    break;
                                case 0x1900:
                                    WriteStatus("[HIRC] [03] TODO : CAkActionSetSwitch::Create");
                                    break; // CAkActionSetSwitch::Create
                                case 0x1A00: // Same as 0x1B00
                                case 0x1B00:
                                    WriteStatus("[HIRC] [03] TODO : CAkActionBypassFX::Create");
                                    break; // CAkActionBypassFX::Create
                                case 0x1C00:
                                    WriteStatus("[HIRC] [03] TODO : CAkActionBreak::Create");
                                    break; // CAkActionBreak::Create
                                case 0x1D00:
                                    WriteStatus("[HIRC] [03] TODO : CAkActionTrigger::Create");
                                    break; // CAkActionTrigger::Create
                                case 0x1E00:
                                    WriteStatus("[HIRC] [03] TODO : CAkActionSeek::Create");
                                    break; // CAkActionSeek::Create
                                case 0x1F00:
                                    WriteStatus("[HIRC] [03] TODO : CAkActionRelease::Create");
                                    break; // CAkActionRelease::Create
                                case 0x2000:
                                { // CAkActionSetAkProp::Create AkPropID_HPF
                                    var _bf_36 = reader.ReadByte();

                                    { // CAkActionSetAkProp::SetActionSpecificParams
                                        var m_eValueMeaning = reader.ReadByte();
                                        var in_MidValue = reader.ReadSingle();
                                        var in_min = reader.ReadSingle();
                                        var in_max = reader.ReadSingle();
                                    }

                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                                case 0x2100:
                                { // CAkActionPlayEvent::Create
                                    break;
                                }
                                case 0x2200:
                                { // CAkActionResetPlaylist::Create
                                    var _bf_36 = reader.ReadByte();

                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                                case 0x3000:
                                { // CAkActionSetAkProp::Create AkPropID_HPF
                                    var _bf_36 = reader.ReadByte();

                                    { // CAkActionSetAkProp::SetActionSpecificParams
                                        var m_eValueMeaning = reader.ReadByte();
                                        var in_MidValue = reader.ReadSingle();
                                        var in_min = reader.ReadSingle();
                                        var in_max = reader.ReadSingle();
                                    }

                                    { // CAkActionExcept::SetExceptParams
                                        var paramCount = reader.ReadUInt32();

                                        for (var j = 0; j < paramCount; j++)
                                        {
                                            // WwiseObjectIDext
                                            var id = reader.ReadUInt32();
                                            var bIsBus = reader.ReadByte();
                                        }
                                    }
                                    
                                    break;
                                }
                            }
                        }
                    }
                    
                    if (reader.BaseStream.Position != section.dwSectionSize + old)
                    {
                        WriteStatus($"[HIRC] [03] Section read length mismatch! {section.dwSectionSize + old - reader.BaseStream.Position}");
                        reader.BaseStream.Position = old;
                        Directory.CreateDirectory($"./dumps/CAkAction/");
                        File.WriteAllBytes($"./dumps/CAkAction/{objectId}.bin", reader.ReadBytes((int)section.dwSectionSize));
                    }
                    
                    reader.BaseStream.Position = section.dwSectionSize + old;
                    break;
                }
                case 4: // ReadEvent
                {
                    WriteStatus("[HIRC] [04] ReadEvent");
                    var old = reader.BaseStream.Position;

                    var objectId = reader.ReadUInt32();

                    WriteStatus($"[HIRC] [04] Object ID {objectId}");

                    { // CAkEvent::SetInitialValues
                        var actionCount = (uint)reader.Read7BitEncodedInt();

                        var actions = new List<uint>();
                        for (var j = 0; j < actionCount; j++)
                            actions.Add(reader.ReadUInt32());
                        
                        WriteStatus($"[HIRC] [04] Actions {JsonConvert.SerializeObject(actions)}");
                    }

                    if (reader.BaseStream.Position != section.dwSectionSize + old)
                    {
                        WriteStatus($"[HIRC] [04] Section read length mismatch! {section.dwSectionSize + old - reader.BaseStream.Position}");
                        reader.BaseStream.Position = old;
                        Directory.CreateDirectory($"./dumps/CAkEvent/");
                        File.WriteAllBytes($"./dumps/CAkEvent/{objectId}.bin", reader.ReadBytes((int)section.dwSectionSize));
                    }
                    
                    reader.BaseStream.Position = section.dwSectionSize + old;
                    break;
                }
                case 5: // StdBankRead<CAkRanSeqCntr,CAkParameterNodeBase>
                case 6: // StdBankRead<CAkSwitchCntr,CAkParameterNodeBase>
                case 7: // StdBankRead<CAkActorMixer,CAkParameterNodeBase>
                case 8: // ReadBus
                case 9: // StdBankRead<CAkLayerCntr,CAkParameterNodeBase>
                case 10: // TODO
                case 11: // TODO
                case 12: // TODO
                case 13: // TODO
                    reader.ReadBytes((int)section.dwSectionSize);
                    break;
                case 14: // StdBankRead<CAkAttenuation,CAkAttenuation>
                case 15: // StdBankRead<CAkDialogueEvent,CAkDialogueEvent>
                case 16: // StdBankRead<CAkFxShareSet,CAkFxShareSet>
                case 17: // StdBankRead<CAkFxCustom,CAkFxCustom>
                case 18: // StdBankRead<CAkAuxBus,CAkParameterNodeBase>
                case 19: // StdBankRead<CAkLFOModulator,CAkModulator>
                case 20: // StdBankRead<CAkEnvelopeModulator,CAkModulator>
                case 21: // StdBankRead<CAkAudioDevice,CAkAudioDevice>
                    reader.ReadBytes((int)section.dwSectionSize);
                    break;
                case 22: // StdBankRead<CAkTimeModulator,CAkModulator>
                {
                    WriteStatus("[HIRC] [22] StdBankRead<CAkTimeModulator,CAkModulator>");
                    var old = reader.BaseStream.Position;

                    var objectId = reader.ReadUInt32();

                    WriteStatus($"[HIRC] [22] Object ID {objectId}");

                    {
                        // m_props
                        var propBundleCount = reader.ReadByte();
                        WriteStatus($"[HIRC] [22] m_props Count {propBundleCount}");

                        var propBundleKeys = new List<byte>(propBundleCount);
                        var propBundleValues = new List<uint>(propBundleCount);
                        var propBundle = new Dictionary<byte, uint>(propBundleCount);

                        for (var j = 0; j < propBundleCount; j++)
                            propBundleKeys.Add(reader.ReadByte());
                        for (var j = 0; j < propBundleCount; j++)
                            propBundleValues.Add(reader.ReadUInt32());
                        for (var j = 0; j < propBundleCount; j++)
                            propBundle.Add(propBundleKeys[j], propBundleValues[j]);

                        WriteStatus(JsonConvert.SerializeObject(propBundle));
                    }

                    {
                        // m_ranges : not sure if it should be a KeyValuePair
                        var propBundleCount = reader.ReadByte();
                        WriteStatus($"[HIRC] [22] m_ranges Count {propBundleCount}");

                        var propBundleKeys = new List<byte>(propBundleCount);
                        var propBundleValues = new List<KeyValuePair<uint, uint>>(propBundleCount);
                        var propBundle = new Dictionary<byte, KeyValuePair<uint, uint>>(propBundleCount);

                        for (var j = 0; j < propBundleCount; j++)
                            propBundleKeys.Add(reader.ReadByte());
                        for (var j = 0; j < propBundleCount; j++)
                            propBundleValues.Add(new KeyValuePair<uint, uint>(reader.ReadUInt32(), reader.ReadUInt32()));
                        for (var j = 0; j < propBundleCount; j++)
                            propBundle.Add(propBundleKeys[j], propBundleValues[j]);

                        WriteStatus(JsonConvert.SerializeObject(propBundle));
                    }

                    { // SetInitialRTPC<CAkModulator>
                        var rtpcCount = reader.ReadUInt16();
                        
                        WriteStatus($"[HIRC] [22] RTPC Count {rtpcCount}");

                        for (var j = 0; j < rtpcCount; j++)
                        {
                            var rtpc = new RTPC
                            {
                                ID = reader.ReadInt32(),
                                Type = reader.ReadByte(),
                                Accum = reader.ReadByte(),
                                ParamID = (uint)reader.Read7BitEncodedInt(),
                                CurveID = reader.ReadUInt32(),
                                eScaling = reader.ReadByte(),
                                ulConversionArraySize = reader.ReadUInt16(),
                            };

                            rtpc.pArrayGraphPoints = new List<float>(rtpc.ulConversionArraySize);

                            for (var k = 0; k < rtpc.ulConversionArraySize * 3; k++)
                            {
                                rtpc.pArrayGraphPoints.Add(reader.ReadSingle());
                            }
                            
                            WriteStatus(JsonConvert.SerializeObject(rtpc));
                        }
                    }
                    
                    if (reader.BaseStream.Position != section.dwSectionSize + old)
                        WriteStatus($"[HIRC] [22] Section read length mismatch! {section.dwSectionSize + old - reader.BaseStream.Position}");
                    
                    reader.BaseStream.Position = section.dwSectionSize + old;
                    break;
                }
                default:
                    reader.ReadBytes((int)section.dwSectionSize);
                    break;
            }
        }
    }*/
}

public class Custom_MediaIndex
{
    public byte[] Data;
    public uint Count;
}

// IDK What to name this, should probably check if an actual class exists
public class RTPC
{
    public int ID;
    public byte Type;
    public byte Accum;
    public uint ParamID;
    public uint CurveID;
    public byte eScaling;
    public List<float> pArrayGraphPoints;
    public ushort ulConversionArraySize;
}
