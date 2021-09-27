using System;

namespace Zorrero.Utils.IntervalLookup.Exceptions
{
    public class IntervalsOverlappedException: Exception
    {
        public IntervalsOverlappedException(string message) : base(message)
        {
        }
    }
}