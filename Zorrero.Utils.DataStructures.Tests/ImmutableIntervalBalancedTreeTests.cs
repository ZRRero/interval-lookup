﻿using System.Collections.Generic;
using System.Linq;
using Xunit;
using Zorrero.Utils.DataStructures.Model.Interval;

namespace Zorrero.Utils.DataStructures.Tests
{
    public class ImmutableIntervalBalancedTreeTests
    {
        [Fact]
        public void ShouldBuildCorrectlySortedTreeLayersComplete()
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
            var treeIntervals = new List<IntervalWithValue<long, long>>
            {
                intervalNodeOne, intervalNodeTwo, intervalNodeThree, intervalNodeFour, intervalNodeFive, intervalNodeSix, intervalNodeSeven
            };
            var tree = new ImmutableIntervalBalancedTree<long, long>(treeIntervals);
            Assert.Equal(nodeOne, tree.Root); 
        }
        
        [Fact]
        public void ShouldBuildCorrectlySortedTreeLayersIncomplete()
        {
            var intervalNodeFour = new IntervalWithValue<long, long>(0,5,1);
            var nodeFour = new TreeNode<long, long>(intervalNodeFour, null, null);
            var intervalNodeFive = new IntervalWithValue<long, long>(10,15,2);
            var nodeFive = new TreeNode<long, long>(intervalNodeFive, null, null);
            var intervalNodeSix = new IntervalWithValue<long, long>(20,25,0);
            var nodeSix = new TreeNode<long, long>(intervalNodeSix, null, null);
            var intervalNodeTwo = new IntervalWithValue<long, long>(5,10,1);
            var nodeTwo = new TreeNode<long, long>(intervalNodeTwo, nodeFour, nodeFive);
            var intervalNodeThree = new IntervalWithValue<long, long>(25,30,2);
            var nodeThree = new TreeNode<long, long>(intervalNodeThree, nodeSix, null);
            var intervalNodeOne = new IntervalWithValue<long, long>(15,20,0);
            var nodeOne = new TreeNode<long, long>(intervalNodeOne, nodeTwo, nodeThree);
            var treeIntervals = new List<IntervalWithValue<long, long>>
            {
                intervalNodeOne, intervalNodeTwo, intervalNodeThree, intervalNodeFour, intervalNodeFive, intervalNodeSix
            };
            var tree = new ImmutableIntervalBalancedTree<long, long>(treeIntervals);
            Assert.Equal(nodeOne, tree.Root); 
        }
        
        [Fact]
        public void ShouldSearchInTree()
        {
            var intervalNodeFour = new IntervalWithValue<long, long>(0,5,1);
            var intervalNodeFive = new IntervalWithValue<long, long>(10,15,2);
            var intervalNodeSix = new IntervalWithValue<long, long>(20,25,0);
            var intervalNodeSeven = new IntervalWithValue<long, long>(30,35,0);
            var intervalNodeTwo = new IntervalWithValue<long, long>(5,10,1);
            var intervalNodeThree = new IntervalWithValue<long, long>(25,30,2);
            var intervalNodeOne = new IntervalWithValue<long, long>(15,20,0);
            var treeIntervals = new List<IntervalWithValue<long, long>>
            {
                intervalNodeOne, intervalNodeTwo, intervalNodeThree, intervalNodeFour, intervalNodeFive, intervalNodeSix, intervalNodeSeven
            };
            var tree = new ImmutableIntervalBalancedTree<long, long>(treeIntervals);
            var result = tree.Search(3, false, false);
            Assert.Single(result);
            Assert.Equal(intervalNodeFour, result.First()); 
        }
    }
}