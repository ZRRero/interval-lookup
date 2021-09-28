using System;
using System.Collections.Generic;

namespace Zorrero.Utils.IntervalLookup.Model
{
    public class TreeNode<T, TK> where T : IComparable<T>
    {
        private readonly IntervalWithValue<T, TK> _interval;
        private readonly TreeNode<T, TK> _left;
        private readonly TreeNode<T, TK> _right;
        internal IEnumerable<IntervalWithValue<T, TK>> Intervals => GetIntervals();
        internal int Count => CountRecursive();

        public TreeNode(IntervalWithValue<T, TK> interval, TreeNode<T, TK> left, TreeNode<T, TK> right)
        {
            _interval = interval;
            _left = left;
            _right = right;
        }

        public void Search(in List<IntervalWithValue<T, TK>> foundIntervals, T value, bool includeInit, bool includeEnd)
        {
            var intervalEvaluation = _interval.Evaluate(value, includeInit, includeEnd);

            switch (intervalEvaluation)
            {
                case IntervalResult.UNDER:
                    _left?.Search(foundIntervals, value, includeInit, includeEnd);
                    break;
                case IntervalResult.UPPER:
                    _right?.Search(foundIntervals, value, includeInit, includeEnd);
                    break;
                case IntervalResult.CONTAINED:
                    foundIntervals.Add(_interval);
                    _right?.Search(foundIntervals, value, includeInit, includeEnd);
                    _left?.Search(foundIntervals, value, includeInit, includeEnd);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IntervalWithValue<T, TK> Search(T value, bool includeInit, bool includeEnd)
        {
            var intervalEvaluation = _interval.Evaluate(value, includeInit, includeEnd);

            return intervalEvaluation switch
            {
                IntervalResult.UNDER => _left?.Search(value, includeInit, includeEnd),
                IntervalResult.UPPER => _right?.Search(value, includeInit, includeEnd),
                IntervalResult.CONTAINED => _interval,
                _ => throw new ArgumentOutOfRangeException(nameof(intervalEvaluation))
            };
        }

        private bool Equals(TreeNode<T, TK> other)
        {
            return Equals(_interval, other._interval) && Equals(_left, other._left) && Equals(_right, other._right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TreeNode<T, TK>) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_interval, _left, _right);
        }
        
        private IEnumerable<IntervalWithValue<T, TK>> GetIntervals()
        {
            var leftIntervals = _left?.Intervals;
            var rightIntervals = _right?.Intervals;
            var nodeIntervals = new List<IntervalWithValue<T, TK>> {_interval};
            nodeIntervals.AddRange(leftIntervals ?? new List<IntervalWithValue<T, TK>>());
            nodeIntervals.AddRange(rightIntervals ?? new List<IntervalWithValue<T, TK>>());
            return nodeIntervals;
        }

        private int CountRecursive()
        {
            return 1 + (_left?.Count ?? 0) + (_right?.Count ?? 0);
        }
    }
}