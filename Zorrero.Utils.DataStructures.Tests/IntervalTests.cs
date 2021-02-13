using System;
using Xunit;
using Zorrero.Utils.DataStructures.Exceptions;
using Zorrero.Utils.DataStructures.Model.Interval;

namespace Zorrero.Utils.DataStructures.Tests
{
    public class IntervalTests
    {
        //Interval creation
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

        //Interval compare
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

        //Interval evaluate
        [Fact]
        public void GivenIntervalShouldEvaluateUnderIncludingInit()
        {
            var interval = new IntervalWithValue<long, long>(1,5,0);
            Assert.Equal(IntervalResult.UNDER, interval.Evaluate(0, true, false));
        }
        
        [Fact]
        public void GivenIntervalShouldEvaluateUnderNotIncludingInit()
        {
            var interval = new IntervalWithValue<long, long>(1,5,0);
            Assert.Equal(IntervalResult.UNDER, interval.Evaluate(1, false, false));
        }
        
        [Fact]
        public void GivenIntervalShouldEvaluateUpperIncludingEnd()
        {
            var interval = new IntervalWithValue<long, long>(1,5,0);
            Assert.Equal(IntervalResult.UPPER, interval.Evaluate(6, false, true));
        }
        
        [Fact]
        public void GivenIntervalShouldEvaluateUpperNotIncludingEnd()
        {
            var interval = new IntervalWithValue<long, long>(1,5,0);
            Assert.Equal(IntervalResult.UPPER, interval.Evaluate(5, false, false));
        }
        
        [Fact]
        public void GivenIntervalShouldEvaluateContainedIncludingInit()
        {
            var interval = new IntervalWithValue<long, long>(1,5,0);
            Assert.Equal(IntervalResult.CONTAINED, interval.Evaluate(1, true, false));
        }
        
        [Fact]
        public void GivenIntervalShouldEvaluateContainedNotIncludingInit()
        {
            var interval = new IntervalWithValue<long, long>(1,5,0);
            Assert.Equal(IntervalResult.CONTAINED, interval.Evaluate(2, false, false));
        }
        
        [Fact]
        public void GivenIntervalShouldEvaluateContainedIncludingEnd()
        {
            var interval = new IntervalWithValue<long, long>(1,5,0);
            Assert.Equal(IntervalResult.CONTAINED, interval.Evaluate(5, false, true));
        }
        
        [Fact]
        public void GivenIntervalShouldEvaluateContainedNotIncludingEnd()
        {
            var interval = new IntervalWithValue<long, long>(1,5,0);
            Assert.Equal(IntervalResult.CONTAINED, interval.Evaluate(4, false, false));
        }
        
        //Interval Equals
        [Fact]
        public void TwoIntervalWithAllDifferentPropertiesShouldNotBeEqual()
        {
            var intervalOne = new IntervalWithValue<long, long>(1,5,0);
            var intervalTwo = new IntervalWithValue<long, long>(-5,10,-9);
            Assert.False(intervalOne.Equals(intervalTwo));
        }

        [Fact]
        public void TwoIntervalWithSameValueAndDifferentInitAndEndShouldNotBeEqual()
        {
            var intervalOne = new IntervalWithValue<long, long>(1,5,0);
            var intervalTwo = new IntervalWithValue<long, long>(-5,10,0);
            Assert.False(intervalOne.Equals(intervalTwo));
        }
        
        [Fact]
        public void TwoIntervalWithSameInitAndEndAndDifferentValueShouldNotBeEqual()
        {
            var intervalOne = new IntervalWithValue<long, long>(1,5,0);
            var intervalTwo = new IntervalWithValue<long, long>(1,5,1);
            Assert.False(intervalOne.Equals(intervalTwo));
        }
        
        [Fact]
        public void TwoIntervalWithSamePropertiesShouldBeEqual()
        {
            var intervalOne = new IntervalWithValue<long, long>(1,5,0);
            var intervalTwo = new IntervalWithValue<long, long>(1,5,0);
            Assert.True(intervalOne.Equals(intervalTwo));
        }
    }
}