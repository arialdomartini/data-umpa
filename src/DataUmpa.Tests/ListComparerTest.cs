using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DataUmpa.Tests
{
    public class ListComparerTest
    {
        [Fact]
        void splits_a_list()
        {
            var left = new List<int> {1, /*2,*/ 3, 4};
            var right = new List<int> {1, 2, 3};

            var result = ListComparer.Split(left, right);

            result.onlyLeft.Should().BeEquivalentTo(4);
            result.onlyRight.Should().BeEquivalentTo(2);
            result.both.Should().BeEquivalentTo(1, 3);
        }
    }
}