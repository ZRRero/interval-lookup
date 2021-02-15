using System.Collections.Generic;
using System.Linq;
using Xunit;
using Zorrero.Utils.DataStructures.Model.Interval;

namespace Zorrero.Utils.DataStructures.Tests
{
    public class TreeNodeTests
    {
        [Fact]
        public void ShouldReturnTheIntervalInOneNode()
        {
            var interval = new IntervalWithValue<long, long>(1,5,0);
            var node = new TreeNode<long, long>(interval, null, null);
            var intervals = new List<IntervalWithValue<long, long>>();
            node.Search(intervals, 3, false, false);
            Assert.Single(intervals);
            Assert.Equal(interval, intervals.First());
        }

        [Fact]
        public void ShouldReturnTheLeftIntervalInNodeWithTwoChildren()
        {
            var intervalNodeTwo = new IntervalWithValue<long, long>(0,5,1);
            var nodeTwo = new TreeNode<long, long>(intervalNodeTwo, null, null);
            var intervalNodeThree = new IntervalWithValue<long, long>(10,15,2);
            var nodeThree = new TreeNode<long, long>(intervalNodeThree, null, null);
            var intervalNodeOne = new IntervalWithValue<long, long>(5,10,0);
            var nodeOne = new TreeNode<long, long>(intervalNodeOne, nodeTwo, nodeThree);
            var intervals = new List<IntervalWithValue<long, long>>();
            nodeOne.Search(intervals, 3, false, false);
            Assert.Single(intervals);
            Assert.Equal(intervalNodeTwo, intervals.First());
        }
        
        [Fact]
        public void ShouldReturnTheRightIntervalInNodeWithTwoChildren()
        {
            var intervalNodeTwo = new IntervalWithValue<long, long>(0,5,1);
            var nodeTwo = new TreeNode<long, long>(intervalNodeTwo, null, null);
            var intervalNodeThree = new IntervalWithValue<long, long>(10,15,2);
            var nodeThree = new TreeNode<long, long>(intervalNodeThree, null, null);
            var intervalNodeOne = new IntervalWithValue<long, long>(5,10,0);
            var nodeOne = new TreeNode<long, long>(intervalNodeOne, nodeTwo, nodeThree);
            var intervals = new List<IntervalWithValue<long, long>>();
            nodeOne.Search(intervals, 12, false, false);
            Assert.Single(intervals);
            Assert.Equal(intervalNodeThree, intervals.First());
        }
        
        [Fact]
        public void ShouldReturnTwoOverlappedIntervalsInNodeWithTwoChildren()
        {
            var intervalNodeTwo = new IntervalWithValue<long, long>(0,5,1);
            var nodeTwo = new TreeNode<long, long>(intervalNodeTwo, null, null);
            var intervalNodeThree = new IntervalWithValue<long, long>(10,15,2);
            var nodeThree = new TreeNode<long, long>(intervalNodeThree, null, null);
            var intervalNodeOne = new IntervalWithValue<long, long>(5,10,0);
            var nodeOne = new TreeNode<long, long>(intervalNodeOne, nodeTwo, nodeThree);
            var intervals = new List<IntervalWithValue<long, long>>();
            nodeOne.Search(intervals, 5, true, true);
            Assert.Equal(2, intervals.Count);
            Assert.Equal(intervalNodeOne, intervals[0]);
            Assert.Equal(intervalNodeTwo, intervals[1]);
        }

        [Fact]
        public void ShouldSearchCorrectIntervalInMultipleLayerNodeLastLayer()
        {
            var intervalNodeFour = new IntervalWithValue<long, long>(0,5,1);
            var nodeFour = new TreeNode<long, long>(intervalNodeFour, null, null);
            var intervalNodeFive = new IntervalWithValue<long, long>(10,15,2);
            var nodeFive = new TreeNode<long, long>(intervalNodeFive, null, null);
            var intervalNodeSix = new IntervalWithValue<long, long>(20,25,0);
            var nodeSix = new TreeNode<long, long>(intervalNodeSix, null, null);
            var intervalNodeSeven = new IntervalWithValue<long, long>(30,35,0);
            var nodeSeven = new TreeNode<long, long>(intervalNodeSeven, null, null);
            var intervalNodeTwo = new IntervalWithValue<long, long>(5,10,1);
            var nodeTwo = new TreeNode<long, long>(intervalNodeTwo, nodeFour, nodeFive);
            var intervalNodeThree = new IntervalWithValue<long, long>(25,30,2);
            var nodeThree = new TreeNode<long, long>(intervalNodeThree, nodeSix, nodeSeven);
            var intervalNodeOne = new IntervalWithValue<long, long>(15,20,0);
            var nodeOne = new TreeNode<long, long>(intervalNodeOne, nodeTwo, nodeThree);
            var intervals = new List<IntervalWithValue<long, long>>();
            nodeOne.Search(intervals, 33, false, false);
            Assert.Single(intervals);
            Assert.Equal(intervalNodeSeven, intervals.First());
        }
        
        [Fact]
        public void ShouldSearchCorrectIntervalInMultipleLayerNodeFirstLayer()
        {
            var intervalNodeFour = new IntervalWithValue<long, long>(0,5,1);
            var nodeFour = new TreeNode<long, long>(intervalNodeFour, null, null);
            var intervalNodeFive = new IntervalWithValue<long, long>(10,15,2);
            var nodeFive = new TreeNode<long, long>(intervalNodeFive, null, null);
            var intervalNodeSix = new IntervalWithValue<long, long>(20,25,0);
            var nodeSix = new TreeNode<long, long>(intervalNodeSix, null, null);
            var intervalNodeSeven = new IntervalWithValue<long, long>(30,35,0);
            var nodeSeven = new TreeNode<long, long>(intervalNodeSeven, null, null);
            var intervalNodeTwo = new IntervalWithValue<long, long>(5,10,1);
            var nodeTwo = new TreeNode<long, long>(intervalNodeTwo, nodeFour, nodeFive);
            var intervalNodeThree = new IntervalWithValue<long, long>(25,30,2);
            var nodeThree = new TreeNode<long, long>(intervalNodeThree, nodeSix, nodeSeven);
            var intervalNodeOne = new IntervalWithValue<long, long>(15,20,0);
            var nodeOne = new TreeNode<long, long>(intervalNodeOne, nodeTwo, nodeThree);
            var intervals = new List<IntervalWithValue<long, long>>();
            nodeOne.Search(intervals, 7, false, false);
            Assert.Single(intervals);
            Assert.Equal(intervalNodeTwo, intervals.First());
        }
        
        [Fact]
        public void ShouldSearchCorrectIntervalInMultipleLayerNodeOverlappedLayers()
        {
            var intervalNodeFour = new IntervalWithValue<long, long>(0,5,1);
            var nodeFour = new TreeNode<long, long>(intervalNodeFour, null, null);
            var intervalNodeFive = new IntervalWithValue<long, long>(10,15,2);
            var nodeFive = new TreeNode<long, long>(intervalNodeFive, null, null);
            var intervalNodeSix = new IntervalWithValue<long, long>(20,25,0);
            var nodeSix = new TreeNode<long, long>(intervalNodeSix, null, null);
            var intervalNodeSeven = new IntervalWithValue<long, long>(30,35,0);
            var nodeSeven = new TreeNode<long, long>(intervalNodeSeven, null, null);
            var intervalNodeTwo = new IntervalWithValue<long, long>(5,10,1);
            var nodeTwo = new TreeNode<long, long>(intervalNodeTwo, nodeFour, nodeFive);
            var intervalNodeThree = new IntervalWithValue<long, long>(25,30,2);
            var nodeThree = new TreeNode<long, long>(intervalNodeThree, nodeSix, nodeSeven);
            var intervalNodeOne = new IntervalWithValue<long, long>(15,20,0);
            var nodeOne = new TreeNode<long, long>(intervalNodeOne, nodeTwo, nodeThree);
            var intervals = new List<IntervalWithValue<long, long>>();
            nodeOne.Search(intervals, 5, true, true);
            Assert.Equal(2, intervals.Count);
            Assert.Equal(intervalNodeTwo, intervals[0]);
            Assert.Equal(intervalNodeFour, intervals[1]);
        }
    }
}