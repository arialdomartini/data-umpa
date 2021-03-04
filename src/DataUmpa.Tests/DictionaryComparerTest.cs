using System.Collections.Generic;
using System.Collections.Immutable;
using FluentAssertions;
using Xunit;

namespace DataUmpa.Tests
{
    public class DictionaryComparerTest
    {
        [Fact]
        void generates_actions_from_a_dictionary()
        {
            var left = new List<int> {1, /*2,*/ 3, 4}.ToImmutableDictionary(i => i, i => i.ToString());
            var right = new List<int>{1, 2, 3}.ToImmutableDictionary(i => i, i => i.ToString());

            var executedActions = new List<string>();

            void WhenBoth(string leftItem, string rightItem) =>
                executedActions.Add($"Both {leftItem}");

            void WhenLeft(string leftItem) =>
                executedActions.Add($"Left {leftItem}");

            void WhenRight(string rightItem) =>
                executedActions.Add($"Right {rightItem}");

            DictionaryComparer.Sync(left, right, WhenLeft, WhenRight, WhenBoth);

            executedActions.Should().BeEquivalentTo(
                new List<string>
                {
                    "Both 1",
                    "Both 3",
                    "Left 4",
                    "Right 2"
                });
        }
    }
}