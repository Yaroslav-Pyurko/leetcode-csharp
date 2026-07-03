namespace LeetCode
{
    public class LC0242_ValidAnagram
    {
        public bool IsAnagram(string s, string t)
        {
            var sortS = SortSring(s);
            var sortT = SortSring(t);

            if (String.Equals(sortS, sortT))
            {
                return true;
            }
            return false;
        }

        public static string SortSring(string s)
        {
            var charArray = s.ToLower().ToCharArray();
            Array.Sort(charArray);
            return new string(charArray);
        }
    }
}
