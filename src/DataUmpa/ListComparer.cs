using System.Collections.Generic;
using System.Linq;

namespace DataUmpa
{
    public static class ListComparer
    {
        public static (IEnumerable<T> onlyLeft, IEnumerable<T> both, IEnumerable<T> onlyRight) Split<T>(IEnumerable<T> left, IEnumerable<T> right)
        {
            var onlyLeft = left.Where(l => !right.Contains(l));
            var onlyRight = right.Where(r => !left.Contains(r));
            var both = left.Intersect(right);

            return (onlyLeft, both, onlyRight);
        }
    }
}