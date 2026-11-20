using System.Text;

namespace LeetCode;

/// <summary>
/// aka Japan Dictionary ex SKB Kontur
/// </summary>
public class LC0642_DesignSearchAutocompleteSystem
{
    private const char EndOfSentence = '#';
    private const int SuggestionCount = 3;

    private sealed class TrieNode
    {
        public readonly Dictionary<char, TrieNode> Children = [];
        public readonly Dictionary<string, int> Counts = [];
    }

    private readonly TrieNode _root = new();
    private readonly StringBuilder _currentQuery = new();
    private TrieNode? _currentNode;

    public LC0642_DesignSearchAutocompleteSystem(string[] sentences, int[] times)
    {
        _currentNode = _root;

        for (int i = 0; i < sentences.Length; i++)
        {
            Insert(sentences[i], times[i]);
        }
    }

    public IList<string> Input(char c)
    {
        if (c == EndOfSentence)
        {
            Insert(_currentQuery.ToString(), 1);
            _currentQuery.Clear();
            _currentNode = _root;
            return [];
        }

        _currentQuery.Append(c);

        if (_currentNode is null || !_currentNode.Children.TryGetValue(c, out var next))
        {
            _currentNode = null;
            return [];
        }

        _currentNode = next;

        return _currentNode.Counts
            .OrderByDescending(entry => entry.Value)
            .ThenBy(entry => entry.Key, StringComparer.Ordinal)
            .Take(SuggestionCount)
            .Select(entry => entry.Key)
            .ToList();
    }

    private void Insert(string sentence, int count)
    {
        var node = _root;

        foreach (char c in sentence)
        {
            if (!node.Children.TryGetValue(c, out var child))
            {
                child = new TrieNode();
                node.Children[c] = child;
            }

            node = child;
            node.Counts[sentence] = node.Counts.GetValueOrDefault(sentence) + count;
        }
    }
}
