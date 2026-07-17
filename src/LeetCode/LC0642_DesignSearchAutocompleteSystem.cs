public class LC0642_DesignSearchAutocompleteSystem
{
    // Узел префиксного дерева
    private class TrieNode
    {
        public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
        // Хранит предложения, проходящие через этот узел, и их частоту
        public Dictionary<string, int> Counts = new Dictionary<string, int>();
    }

    private TrieNode root;
    private TrieNode currNode;
    private string currentQuery;

    /// <summary>
    /// aka Japan Dictionary ex SKB Kontur
    /// </summary>
    public LC0642_DesignSearchAutocompleteSystem(string[] sentences, int[] times)
    {
        root = new TrieNode();
        currNode = root;
        currentQuery = "";

        // Инициализируем дерево начальными данными
        for (int i = 0; i < sentences.Length; i++)
        {
            Insert(sentences[i], times[i]);
        }
    }

    public IList<string> Input(char c)
    {
        // Символ '#' означает конец ввода предложения
        if (c == '#')
        {
            Insert(currentQuery, 1); // Сохраняем или обновляем частоту
            currNode = root;         // Сбрасываем указатель для нового поиска
            currentQuery = "";       // Очищаем текущую строку
            return new List<string>();
        }

        currentQuery += c;

        // Если мы уже ушли в ветку, которой нет в дереве
        if (currNode == null || !currNode.Children.ContainsKey(c))
        {
            currNode = null; // Дальнейший ввод в этой сессии не даст результатов
            return new List<string>();
        }

        // Переходим к следующему символу
        currNode = currNode.Children[c];

        // Выбираем ТОП-3 по правилам: сначала по частоте (убывание), затем по алфавиту (возрастание)
        return currNode.Counts
            .OrderByDescending(x => x.Value)
            .ThenBy(x => x.Key, StringComparer.Ordinal)
            .Take(3)
            .Select(x => x.Key)
            .ToList();
    }

    private void Insert(string sentence, int count)
    {
        TrieNode node = root;
        foreach (char c in sentence)
        {
            if (!node.Children.ContainsKey(c))
            {
                node.Children[c] = new TrieNode();
            }
            node = node.Children[c];

            // Обновляем частоту предложения в каждом узле по пути его формирования
            if (!node.Counts.ContainsKey(sentence))
            {
                node.Counts[sentence] = 0;
            }
            node.Counts[sentence] += count;
        }
    }
}
