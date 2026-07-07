namespace LeetCode
{
    public class LC0242_ValidAnagram
    {
        /// <summary>
        /// Time complexity:  O(n)
        /// Space complexity: O(1)
        /// </summary>
        public bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length)
                return false;

            int[] count = new int[26];

            for (int i = 0; i < s.Length; i++)
            {
                count[s[i] - 'a']++;
            }
                
            for (int i = 0; i < t.Length; i++)
            {
                count[t[i] - 'a']--;
                if (count[t[i] - 'a'] < 0)
                    return false;
            }

            return true;
        }
    }
}