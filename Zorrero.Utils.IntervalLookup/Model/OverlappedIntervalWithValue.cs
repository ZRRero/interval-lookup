using System;

namespace Zorrero.Utils.IntervalLookup.Model
{
    public class OverlappedIntervalWithValue<T, TK>: IntervalWithValue<T,TK> where T: IComparable<T>
    {
        public OverlappedIntervalWithValue(T init, T end, TK value) : base(init, end, value)
        {
        }
        
        public override int CompareTo(IntervalWithValue<T, TK> other)
        {
            if (Init.CompareTo(other.Init) == 0 && End.CompareTo(other.End) == 0) return 0;
            if (Init.CompareTo(other.Init) > 0) return 1;
            if (End.CompareTo(other.End) < 0) return -1;
            return 0;
        }
    }
}