namespace LeetCode.Tests
{
    public class LC0642_DesignSearchAutocompleteSystemTests
    {
        // Classic example from the LeetCode 642 problem statement.
        // Reused by tests below that don't need an isolated, scenario-specific dataset.
        private readonly LC0642_DesignSearchAutocompleteSystem _solution = new(
            new[] { "i love you", "island", "iroman", "i love leetcode" },
            new[] { 5, 3, 2, 2 });

        [Fact]
        public void Constructor_GivenValidSentencesAndTimes_DoesNotThrow()
        {
            var sentences = new[] { "i love you", "island", "iroman", "i love leetcode" };
            var times = new[] { 5, 3, 2, 2 };

            var exception = Record.Exception(() => new LC0642_DesignSearchAutocompleteSystem(sentences, times));

            Assert.Null(exception);
        }

        [Theory]
        [MemberData(nameof(InputSequenceTestData))]
        public void Input_GivenSequenceOfCharacters_ReturnsExpectedSuggestionsAtEachStep(
            string[] sentences,
            int[] times,
            (char Input, string[] Expected)[] steps)
        {
            var system = new LC0642_DesignSearchAutocompleteSystem(sentences, times);

            foreach (var step in steps)
            {
                var result = system.Input(step.Input);
                Assert.Equal(step.Expected, result);
            }
        }

        public static IEnumerable<object[]> InputSequenceTestData()
        {
            // Classic example from the LeetCode 642 problem statement
            yield return new object[]
            {
                new[] { "i love you", "island", "iroman", "i love leetcode" },
                new[] { 5, 3, 2, 2 },
                new (char, string[])[]
                {
                    ('i', new[] { "i love you", "island", "i love leetcode" }),
                    (' ', new[] { "i love you", "i love leetcode" }),
                    ('a', Array.Empty<string>()),
                    ('#', Array.Empty<string>())
                }
            };
        }

        [Fact]
        public void Input_HashAfterUnmatchedPrefix_CommitsNewSentenceForFutureSearches()
        {
            var sentences = new[] { "i love you" };
            var times = new[] { 5 };
            var system = new LC0642_DesignSearchAutocompleteSystem(sentences, times);

            system.Input('i');
            system.Input(' ');
            system.Input('a');
            system.Input('#'); // commits "i a" as a new sentence (count = 1) and resets the input buffer

            system.Input('i');
            system.Input(' ');

            var result = system.Input('a');

            Assert.Contains("i a", result);
        }

        [Theory]
        [InlineData('x')]
        [InlineData('z')]
        [InlineData('q')]
        public void Input_PrefixMatchingNoSentences_ReturnsEmptyList(char firstChar)
        {
            var sentences = new[] { "i love you", "island", "iroman" };
            var times = new[] { 5, 3, 2 };
            var system = new LC0642_DesignSearchAutocompleteSystem(sentences, times);

            var result = system.Input(firstChar);

            Assert.Empty(result);
        }

        [Fact]
        public void Input_TiedHotDegree_ReturnsResultsInLexicographicalOrder()
        {
            var sentences = new[] { "abc", "abd", "abe" };
            var times = new[] { 3, 3, 3 };
            var system = new LC0642_DesignSearchAutocompleteSystem(sentences, times);

            system.Input('a');
            var result = system.Input('b');

            Assert.Equal(new[] { "abc", "abd", "abe" }, result);
        }

        [Fact]
        public void Input_MoreThanThreeMatches_ReturnsTopThreeOnly()
        {
            var sentences = new[] { "aa", "ab", "ac", "ad", "ae" };
            var times = new[] { 5, 4, 3, 2, 1 };
            var system = new LC0642_DesignSearchAutocompleteSystem(sentences, times);

            var result = system.Input('a');

            Assert.Equal(3, result.Count);
            Assert.Equal(new[] { "aa", "ab", "ac" }, result);
        }

        [Fact]
        public void Input_HigherTimesWinsOverLexicographicalOrder_ReturnsHottestFirst()
        {
            var sentences = new[] { "az", "aa" };
            var times = new[] { 1, 10 };
            var system = new LC0642_DesignSearchAutocompleteSystem(sentences, times);

            var result = system.Input('a');

            Assert.Equal(new[] { "aa", "az" }, result);
        }

        [Fact]
        public void Input_HashResetsCurrentSearchState_NextInputStartsFreshPrefix()
        {
            var sentences = new[] { "i love you", "island" };
            var times = new[] { 5, 3 };
            var system = new LC0642_DesignSearchAutocompleteSystem(sentences, times);

            system.Input('i');
            system.Input('#'); // commits current input as a sentence and resets the input buffer

            var result = system.Input('i');

            Assert.Equal(new[] { "i love you", "island", "i"}, result);
        }
    }
}