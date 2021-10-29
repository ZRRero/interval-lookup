using System;
using System.Collections.Generic;
using System.Linq;
using Zorrero.Utils.IntervalLookup.Exceptions;

namespace Zorrero.Utils.IntervalLookup.Model
{
    public class MutableIntervalBalancedTree<T, TK> : IntervalBalancedTree<T, TK>, ICollection<IntervalWithValue<T, TK>>
        where T : IComparable<T>
    {
        private readonly bool _detectOverlappingIntervals;

        public MutableIntervalBalancedTree(IEnumerable<IntervalWithValue<T, TK>> intervals,
            bool detectOverlappingIntervals = false) : base(intervals, detectOverlappingIntervals)
        {
            _detectOverlappingIntervals = detectOverlappingIntervals;
        }

        public int Count => Root?.Count ?? 0;
        public bool IsReadOnly => false;

        public void Add(IntervalWithValue<T, TK> item)
        {
            var intervals = Root?.Intervals.ToList() ?? new List<IntervalWithValue<T, TK>>();
            intervals.Add(item);
            var sortedIntervals = intervals.OrderBy(interval => interval).ToList();
            if (_detectOverlappingIntervals)
            {
                var overlappedIntervals = GetOverlappingIntervals(sortedIntervals);
                if (overlappedIntervals.Count > 0)
                    throw new IntervalsOverlappedException(overlappedIntervals.ToString());
            }

            Root = BuildTreeNode(sortedIntervals, 0, sortedIntervals.Count);
        }

        public void Clear()
        {
            Root = null;
        }

        public bool Contains(IntervalWithValue<T, TK> item)
        {
            return Root?.Contains(item) ?? false;
        }

        public void CopyTo(IntervalWithValue<T, TK>[] array, int arrayIndex)
        {
            Root?.Intervals.ToList().CopyTo(array, arrayIndex);
        }

        public bool Remove(IntervalWithValue<T, TK> item)
        {
            var intervals = Root?.Intervals.ToList() ?? new List<IntervalWithValue<T, TK>>();
            if (!intervals.Remove(item)) return false;
            var sorted = intervals.OrderBy(interval => interval).ToList();
            Root = BuildTreeNode(sorted, 0, sorted.Count);

            return true;
        }
    }
}