namespace LeetCode
{
    public class LC0859_BuddyStrings
    {
        public bool BuddyStrings(string s, string goal)
        {
            if (s.Length != goal.Length || s.Length < 2 || goal.Length < 2)
                return false;

            if (s.Equals(goal))
            {
                // Check if there are any repeated characters in s
                var uniqueChars = new HashSet<char>(s);
                return uniqueChars.Count < s.Length;
            }

            var diffIndexes = new List<int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != goal[i])
                    diffIndexes.Add(i);
                if (diffIndexes.Count > 2)
                    return false;
            }

            if (diffIndexes.Count != 2)
                return false;

            var fixIsOkChar1 = s[diffIndexes[0]] == goal[diffIndexes[1]];
            var fixIsOkChar2 = s[diffIndexes[1]] == goal[diffIndexes[0]];

            return fixIsOkChar1 && fixIsOkChar2;
        }
    }
}