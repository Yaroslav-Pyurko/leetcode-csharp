namespace LeetCode
{
    public class LC0007_ReverseInteger
    {
        public int Reverse(int x)
        {
            // 1. Быстрая проверка на int.MinValue (так как Math.Abs от него вызовет переполнение)
            if (x == int.MinValue) return 0;

            int sign = x < 0 ? -1 : 1;
            x = x < 0 ? -x : x;

            int reversed = 0;

            unchecked
            {
                while (x > 0)
                {
                    int pop = x % 10;
                    x /= 10;

                    // 2. Проверка на переполнение ДО умножения на 10.
                    // int.MaxValue равен 2_147_483_647. Его деление на 10 — это 214_748_364, и остаток 7 
                    if (reversed > 214_748_364 || (reversed == 214_748_364 && pop > 7))
                    {
                        return 0; // Переполнение, возвращаем 0 по условию задачи
                    }

                    reversed = (reversed * 10) + pop;
                }
            }

            return reversed * sign;
        }
    }
}