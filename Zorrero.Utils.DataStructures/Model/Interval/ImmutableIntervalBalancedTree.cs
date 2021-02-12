using System;
using System.Collections.Generic;
using System.Linq;

namespace Zorrero.Utils.DataStructures.Model.Interval
{
    public class ImmutableIntervalBalancedTree<T, TK> where T : IComparable<T>
    {
        private readonly TreeNode<T, TK> _root;

        private static TreeNode<T, TK> BuildTreeNode(List<IntervalWithValue<T, TK>> intervals, int min, int max)
        {
            if (min == max) return null;
            var half = (max - min) / 2;
            return new TreeNode<T, TK>(
                    intervals[half],
                    BuildTreeNode(intervals, min, half),
                    BuildTreeNode(intervals, half + 1, max)
                );
        }

        public ImmutableIntervalBalancedTree(IEnumerable<IntervalWithValue<T, TK>> intervals)
        {
            var sortedIntervals = intervals.OrderBy(interval => interval).ToList();
            _root = BuildTreeNode(sortedIntervals, 0, sortedIntervals.Count - 1);
        }

        public List<IntervalWithValue<T, TK>> Search(T value, bool includeInit, bool includeEnd)
        {
            var intervals = new List<IntervalWithValue<T, TK>>();
            _root.Search(intervals, value, includeInit, includeEnd);
            return intervals;
        }
    }
}