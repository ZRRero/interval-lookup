using System;
using System.Collections.Generic;
using Zorrero.Utils.IntervalLookup.Exceptions;

namespace Zorrero.Utils.IntervalLookup.Model
{
    public abstract class IntervalWithValue<T, TK> : IComparable<IntervalWithValue<T, TK>> where T : IComparable<T>
    {
        public readonly T End;
        public readonly T Init;
        public readonly TK Value;

        protected IntervalWithValue(T init, T end, TK value)
        {
            if (init.CompareTo(end) > 0) throw new InvalidIntervalException();
            Init = init;
            End = end;
            Value = value;
        }

        public abstract int CompareTo(IntervalWithValue<T, TK> other);

        public IntervalResult Evaluate(T toEvaluate, bool includeInit, bool includeEnd)
        {
            if (includeInit)
            {
                if (toEvaluate.CompareTo(Init) < 0) return IntervalResult.UNDER;
            }
            else
            {
                if (toEvaluate.CompareTo(Init) <= 0) return IntervalResult.UNDER;
            }

            if (includeEnd)
            {
                if (toEvaluate.CompareTo(End) > 0) return IntervalResult.UPPER;
            }
            else
            {
                if (toEvaluate.CompareTo(End) >= 0) return IntervalResult.UPPER;
            }

            return IntervalResult.CONTAINED;
        }

        private bool Equals(IntervalWithValue<T, TK> other)
        {
            return EqualityComparer<T>.Default.Equals(Init, other.Init) &&
                   EqualityComparer<T>.Default.Equals(End, other.End) &&
                   EqualityComparer<TK>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((IntervalWithValue<T, TK>) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Init, End, Value);
        }
    }
}