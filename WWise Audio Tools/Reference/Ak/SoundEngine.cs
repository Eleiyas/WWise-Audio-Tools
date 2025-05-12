using System.IO;

namespace WWiseAudioExtractor.Reference.Ak;

public class SoundEngine
{
    public static uint GetBankIDFromString(string in_pszString)
    {
        return GetIDFromString(Path.GetFileNameWithoutExtension(in_pszString));
    }
    
    public static uint GetIDFromString(string in_pszString)
    {
        if (in_pszString.Length == 0)
            return 0;
        return Fnv32.ComputeLowerCase(in_pszString);
    }
}
