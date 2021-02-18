using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Zorrero.Utils.IntervalLookup.Model
{
    public class ImmutableIntervalBalancedTree<T, TK> : IReadOnlyCollection<IntervalWithValue<T, TK>> where T : IComparable<T>
    {
        public readonly TreeNode<T, TK> Root;
        public int Count => Root.Count;

        public ImmutableIntervalBalancedTree(IEnumerable<IntervalWithValue<T, TK>> intervals)
        {
            var sortedIntervals = intervals.OrderBy(interval => interval).ToList();
            Root = BuildTreeNode(sortedIntervals, 0, sortedIntervals.Count);
        }

        private static TreeNode<T, TK> BuildTreeNode(List<IntervalWithValue<T, TK>> intervals, int min, int max)
        {
            if (min == max) return null;
            var half = (max + min) / 2;
            var interval = intervals[half];
            var left = BuildTreeNode(intervals, min, half);
            var right = BuildTreeNode(intervals, half + 1, max);
            return new TreeNode<T, TK>(interval, left, right);
        }

        public List<IntervalWithValue<T, TK>> Search(T value, bool includeInit, bool includeEnd)
        {
            var intervals = new List<IntervalWithValue<T, TK>>();
            Root.Search(intervals, value, includeInit, includeEnd);
            return intervals;
        }

        public IEnumerator<IntervalWithValue<T, TK>> GetEnumerator()
        {
            return Root.Intervals.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}