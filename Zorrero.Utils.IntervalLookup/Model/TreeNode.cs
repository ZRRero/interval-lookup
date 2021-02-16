using System;
using System.Collections.Generic;

namespace Zorrero.Utils.IntervalLookup.Model
{
    public class TreeNode<T, TK> where T : IComparable<T>
    {
        private readonly IntervalWithValue<T, TK> _interval;
        private readonly TreeNode<T, TK> _left;
        private readonly TreeNode<T, TK> _right;

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
    }
}