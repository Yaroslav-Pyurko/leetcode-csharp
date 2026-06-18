namespace LeetCode
{
    public class LC0020_ValidParentheses
    {
        public bool IsValid(string s)
        {
            if (s.Length == 0)
            {
                return true;
            }
            if (s.Length % 2 != 0)
            {
                return false;
            }

            var chars = s.ToCharArray();
            var stack = new Stack<char>();

            for (int i = 0; i < chars.Length; i++)
            {
                var curChat = chars[i];
                if (curChat == '(' || curChat == '{' || curChat == '[')
                {
                    stack.Push(curChat);
                }
                else
                {
                    if (stack.Count == 0)
                    {
                        return false;
                    }

                    var lastChar = stack.Peek();
                    if (curChat == ')' && lastChar == '(' ||
                        curChat == '}' && lastChar == '{' ||
                        curChat == ']' && lastChar == '['
                        )
                    {
                        stack.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return stack.Count == 0;
        }
    }
}
