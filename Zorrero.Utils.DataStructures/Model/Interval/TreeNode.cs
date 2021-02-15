using System;
using System.Collections.Generic;

namespace Zorrero.Utils.DataStructures.Model.Interval
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
    }
}