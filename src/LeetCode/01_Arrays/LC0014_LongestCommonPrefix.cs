namespace LeetCode.Arrays
{
    public class LC0014_LongestCommonPrefix
    {
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0 || strs.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                return string.Empty;
            }

            Array.Sort(strs);
            string first = strs[0];
            string last = strs[strs.Length - 1];
            int i = 0;
            while (i < first.Length && i < last.Length)
            {
                if (first[i] == last[i])
                    i++;
                else break;
            }
            return first.Substring(0, i);
        }
    }
}