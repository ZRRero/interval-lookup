using System;
using System.Collections.Generic;

namespace Zorrero.Utils.IntervalLookup.Model
{
    public class ImmutableIntervalBalancedTree<T, TK> : IntervalBalancedTree<T, TK>,
        IReadOnlyCollection<IntervalWithValue<T, TK>> where T : IComparable<T>
    {
        public ImmutableIntervalBalancedTree(IEnumerable<IntervalWithValue<T, TK>> intervals,
            bool detectOverlappingIntervals = false) : base(intervals, detectOverlappingIntervals)
        {
        }

        public int Count => Root.Count;
    }
}