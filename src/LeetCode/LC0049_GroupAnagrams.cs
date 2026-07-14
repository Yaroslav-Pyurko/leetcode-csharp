namespace LeetCode
{
    /// <summary>
    /// aka Japan Dictionary ex SKB Kontur
    /// </summary>
    public class LC0049_GroupAnagrams
    {
        public List<List<string>> GroupAnagrams(string[] words)
        {
            var groups = new Dictionary<int[], List<string>>(new IntCustomArrayComparer());

            foreach (var word in words)
            {
                const int alphabetSize = 256;
                int[] alphabetCount = new int[alphabetSize];

                foreach (char c in word)
                {
                    if (c < alphabetSize) 
                        alphabetCount[c]++;
                    else
                        throw new ArgumentOutOfRangeException($"Character '{c}' in word '{word}' is out of the expected range.");
                }

                if (!groups.TryGetValue(alphabetCount, out var list))
                {
                    list = new List<string>();
                    groups[alphabetCount] = list;
                }
                list.Add(word);
            }

            var result = new List<List<string>>(groups.Count);
            foreach (var pair in groups)
            {
                result.Add(pair.Value);
            }
            return result;
        }

        public class IntCustomArrayComparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y)
            {
                if (x == null || y == null) return x == y;
                if (x.Length != y.Length) return false;
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != y[i]) return false;
                }
                return true;
            }

            public int GetHashCode(int[] obj)
            {
                if (obj == null) return 0;
                int hash = 17;
                foreach (int element in obj)
                {
                    hash = hash * 31 + element;
                }
                return hash;
            }
        }
    }
}