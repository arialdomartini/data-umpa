using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DataUmpa
{
    public static class DictionaryComparer
    {
        internal static IEnumerable<Action> GetActions<TKey, TFrom, TTo>(ImmutableDictionary<TKey, TFrom> left,
            ImmutableDictionary<TKey, TTo> right,
            Action<TFrom> whenLeft,
            Action<TTo> whenRight,
            Action<TFrom, TTo> whenBoth)
        {
            Action MakeLeftAndBoth(KeyValuePair<TKey, TFrom> from) =>
                () => LeftOrBoth(from.Key, from.Value);

            Action MakeRight(KeyValuePair<TKey, TTo> to) =>
                () => { Right(to.Key, to.Value); };

            void LeftOrBoth(TKey key, TFrom value)
            {
                if (right.ContainsKey(key))
                    whenBoth(value, right[key]);
                else
                    whenLeft(value);
            }

            void Right(TKey key, TTo value)
            {
                if (!left.ContainsKey(key))
                    whenRight(value);
            }

            var leftAndBoth = left.Select(MakeLeftAndBoth);
            var rights = right.Select(MakeRight);

            return leftAndBoth.Union(rights);
        }

        public static void Sync<TKey, TFrom, TTo>(ImmutableDictionary<TKey, TFrom> left,
            ImmutableDictionary<TKey, TTo> right,
            Action<TFrom> whenLeft,
            Action<TTo> whenRight,
            Action<TFrom, TTo> whenBoth)
        {
            GetActions(left, right, whenLeft, whenRight, whenBoth).Run();
        }

        internal static void Run(this IEnumerable<Action> actions) =>
            actions.ToList().ForEach(f => f.Invoke());
    }
}