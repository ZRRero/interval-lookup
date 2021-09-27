using System;

namespace Zorrero.Utils.IntervalLookup.Exceptions
{
    [Serializable]
    public sealed class IntervalsOverlappedException: Exception
    {
        public IntervalsOverlappedException(string message) : base(message)
        {
        }
    }
}