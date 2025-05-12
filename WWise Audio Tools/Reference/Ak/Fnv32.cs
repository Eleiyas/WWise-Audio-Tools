namespace WWiseAudioExtractor.Reference.Ak;

public static class Fnv32
{
    public static uint ComputeLowerCase(string in_pData)
    {
        in_pData = in_pData.ToLower();
        
        var hash = 0x811c9dc5;

        for (var i = 0; i < in_pData.Length; i++)
            hash = (0x1000193 * hash) ^ in_pData[i];

        return hash;
    }
}