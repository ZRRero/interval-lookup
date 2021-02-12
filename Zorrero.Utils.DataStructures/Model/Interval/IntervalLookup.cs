using System;
using System.Collections.Generic;

namespace Zorrero.Utils.DataStructures.Model.Interval
{
    public class IntervalLookup<T, TK> where T: IComparable<T>
    {
        private readonly ImmutableIntervalBalancedTree<T, TK> _tree;

        public IntervalLookup(IEnumerable<IntervalWithValue<T, TK>> intervals)
        {
            _tree = new ImmutableIntervalBalancedTree<T, TK>(intervals);
        }

        public IEnumerable<IntervalWithValue<T, TK>> Search(T toSearch, bool includeInit, bool includeEnd)
        {
            return _tree.Search(toSearch, includeInit, includeEnd);
        } 
    }
}