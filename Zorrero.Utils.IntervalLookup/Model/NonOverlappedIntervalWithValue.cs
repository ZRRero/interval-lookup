using System;
using Zorrero.Utils.IntervalLookup.Exceptions;

namespace Zorrero.Utils.IntervalLookup.Model
{
    public class NonOverlappedIntervalWithValue<T, TK>: IntervalWithValue<T,TK> where T: IComparable<T>
    {
        public NonOverlappedIntervalWithValue(T init, T end, TK value) : base(init, end, value)
        {
        }

        public override int CompareTo(IntervalWithValue<T, TK> other)
        {
            if (End.CompareTo(other.Init) < 0) return -1;
            if (Init.CompareTo(other.End) > 0) return 1;
            throw new IntervalsOverlappedException($"Interval {Init.ToString()}: {End.ToString()} is Overlapped with {other.Init.ToString()}:{other.End.ToString()}");
        }
    }
}