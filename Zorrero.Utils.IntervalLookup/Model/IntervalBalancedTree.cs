using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zorrero.Utils.IntervalLookup.Exceptions;

namespace Zorrero.Utils.IntervalLookup.Model
{
    public abstract class IntervalBalancedTree<T, TK> : IEnumerable<IntervalWithValue<T, TK>> where T : IComparable<T>
    {
        public TreeNode<T, TK> Root { get; protected set; }

        protected IntervalBalancedTree(IEnumerable<IntervalWithValue<T, TK>> intervals,
            bool detectOverlappingIntervals = false)
        {
            var sortedIntervals = intervals.OrderBy(interval => interval).ToList();
            if (detectOverlappingIntervals)
            {
                var overlappedIntervals = GetOverlappingIntervals(sortedIntervals);
                if(overlappedIntervals.Count > 0) throw new IntervalsOverlappedException(overlappedIntervals.ToString());
            }
            Root = BuildTreeNode(sortedIntervals, 0, sortedIntervals.Count);
        }
        
        public IEnumerator<IntervalWithValue<T, TK>> GetEnumerator()
        {
            return Root?.Intervals.GetEnumerator() ?? new List<IntervalWithValue<T, TK>>.Enumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        protected static TreeNode<T, TK> BuildTreeNode(IReadOnlyList<IntervalWithValue<T, TK>> intervals, int min, int max)
        {
            if (min == max) return null;
            var half = (max + min) / 2;
            var interval = intervals[half];
            var left = BuildTreeNode(intervals, min, half);
            var right = BuildTreeNode(intervals, half + 1, max);
            return new TreeNode<T, TK>(interval, left, right);
        }
        
        protected static List<IntervalWithValue<T, TK>> GetOverlappingIntervals(IReadOnlyCollection<IntervalWithValue<T, TK>> sortedIntervals)
        {
            var result = new List<IntervalWithValue<T, TK>>();
            foreach (var intervalWithValueOne in sortedIntervals)
            {
                result.AddRange(from intervalWithValueTwo in sortedIntervals
                    where intervalWithValueOne != intervalWithValueTwo
                    where intervalWithValueOne.IsOverlapped(intervalWithValueTwo)
                    select intervalWithValueOne);
            }

            return result;
        }
        
        public List<IntervalWithValue<T, TK>> Search(T value, bool includeInit, bool includeEnd)
        {
            var intervals = new List<IntervalWithValue<T, TK>>();
            Root?.Search(intervals, value, includeInit, includeEnd);
            return intervals;
        }

        public IntervalWithValue<T, TK> SearchFirst(T value, bool includeInit, bool includeEnd)
        {
            return Root?.Search(value, includeInit, includeEnd);
        }
    }
}