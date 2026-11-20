namespace LeetCode;

public class LC0049_GroupAnagrams
{
    private const int AlphabetSize = 26;

    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        var groups = new Dictionary<string, List<string>>(strs.Length);
        Span<int> counts = stackalloc int[AlphabetSize];
        Span<char> keyBuffer = stackalloc char[AlphabetSize];

        foreach (var word in strs)
        {
            counts.Clear();

            foreach (char c in word)
            {
                counts[c - 'a']++;
            }

            for (int i = 0; i < AlphabetSize; i++)
            {
                keyBuffer[i] = (char)counts[i];
            }

            var key = new string(keyBuffer);

            if (!groups.TryGetValue(key, out var list))
            {
                list = new List<string>();
                groups[key] = list;
            }

            list.Add(word);
        }

        return new List<IList<string>>(groups.Values);
    }
}
