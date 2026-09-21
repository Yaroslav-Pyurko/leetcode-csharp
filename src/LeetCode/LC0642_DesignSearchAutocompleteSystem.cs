/// <summary>
/// aka Japan Dictionary ex SKB Kontur
/// </summary>
public class LC0642_DesignSearchAutocompleteSystem
{
    private class TrieNode
    {
        public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
        public Dictionary<string, int> Counts = new Dictionary<string, int>();
    }

    private TrieNode root;
    private TrieNode? currNode;
    private string currentQuery;

    public LC0642_DesignSearchAutocompleteSystem(string[] sentences, int[] times)
    {
        root = new TrieNode();
        currNode = root;
        currentQuery = "";

        for (int i = 0; i < sentences.Length; i++)
        {
            Insert(sentences[i], times[i]);
        }
    }

    public IList<string> Input(char c)
    {
        // Symbol '#' means the end of the current sentence
        if (c == '#')
        {
            Insert(currentQuery, 1);
            currNode = root;
            currentQuery = "";
            return new List<string>();
        }

        currentQuery += c;

        if (currNode == null || !currNode.Children.ContainsKey(c))
        {
            currNode = null;
            return new List<string>();
        }

        currNode = currNode.Children[c];

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

            if (!node.Counts.ContainsKey(sentence))
            {
                node.Counts[sentence] = 0;
            }
            node.Counts[sentence] += count;
        }
    }
}
