namespace LeetCode
{
    public class LC0014_LongestCommonPrefix
    {
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0)
            {
                return string.Empty;
            }

            string candidate = strs[0];

            for (int position = 0; position < candidate.Length; position++)
            {
                char expected = candidate[position];

                for (int other = 1; other < strs.Length; other++)
                {
                    string word = strs[other];

                    if (position == word.Length || word[position] != expected)
                    {
                        return candidate[..position];
                    }
                }
            }

            return candidate;
        }
    }
}
