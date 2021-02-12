using System;
using Xunit;
using Zorrero.Utils.DataStructures.Exceptions;
using Zorrero.Utils.DataStructures.Model.Interval;

namespace Zorrero.Utils.DataStructures.Tests
{
    public class IntervalTests
    {
        [Fact]
        public void ShouldCreateIntervalCorrectly()
        {
            var interval = new IntervalWithValue<long, string>(1,2,"");
            Assert.Equal(1, interval.Init);
            Assert.Equal(2, interval.End);
            Assert.Equal("", interval.Value);
        }

        [Fact]
        public void ShouldNotCreateIntervalOnbadParameters()
        {
            Assert.Throws<InvalidIntervalException>(() => new IntervalWithValue<long, long>(2,1,0));
        }

        [Fact]
        public void ShouldEvaluateAsEqualIntervals()
        {
            var intervalOne = new IntervalWithValue<long, long>(1,5,0);
            var intervalTwo = new IntervalWithValue<long, long>(1,5,0);
            Assert.Equal(0, intervalOne.CompareTo(intervalTwo));
        }
        
        [Fact]
        public void ShouldEvaluateAsMinorIntervalWithInitEqualAndEndMajor()
        {
            var intervalOne = new IntervalWithValue<long, long>(1,5,0);
            var intervalTwo = new IntervalWithValue<long, long>(1,6,0);
            Assert.Equal(-1, intervalOne.CompareTo(intervalTwo));
        }
        
        [Fact]
        public void ShouldEvaluateAsMinorIntervalsWithInitAndEndMajor()
        {
            var intervalOne = new IntervalWithValue<long, long>(1,5,0);
            var intervalTwo = new IntervalWithValue<long, long>(2,6,0);
            Assert.Equal(-1, intervalOne.CompareTo(intervalTwo));
        }
        
        [Fact]
        public void ShouldEvaluateAsMinorIntervalsWithInitAndEndMajorExclusive()
        {
            var intervalOne = new IntervalWithValue<long, long>(1,5,0);
            var intervalTwo = new IntervalWithValue<long, long>(6,10,0);
            Assert.Equal(-1, intervalOne.CompareTo(intervalTwo));
        }
        
        [Fact]
        public void ShouldEvaluateAsMajorIntervalWithInitMinorAndEndEqual()
        {
            var intervalOne = new IntervalWithValue<long, long>(1,5,0);
            var intervalTwo = new IntervalWithValue<long, long>(0,5,0);
            Assert.Equal(1, intervalOne.CompareTo(intervalTwo));
        }
        
        [Fact]
        public void ShouldEvaluateAsMajorIntervalsWithInitAndEndMinor()
        {
            var intervalOne = new IntervalWithValue<long, long>(1,5,0);
            var intervalTwo = new IntervalWithValue<long, long>(0,3,0);
            Assert.Equal(1, intervalOne.CompareTo(intervalTwo));
        }
        
        [Fact]
        public void ShouldEvaluateAsMajorIntervalsWithInitAndEndMinorExclusive()
        {
            var intervalOne = new IntervalWithValue<long, long>(1,5,0);
            var intervalTwo = new IntervalWithValue<long, long>(-5,0,0);
            Assert.Equal(1, intervalOne.CompareTo(intervalTwo));
        }

        [Fact]
        public void ShouldEvaluateAsMajorIntervalContainedInOtherInterval()
        {
            var intervalOne = new IntervalWithValue<long, long>(1,5,0);
            var intervalTwo = new IntervalWithValue<long, long>(-5,10,0);
            Assert.Equal(1, intervalOne.CompareTo(intervalTwo));
        }
    }
}