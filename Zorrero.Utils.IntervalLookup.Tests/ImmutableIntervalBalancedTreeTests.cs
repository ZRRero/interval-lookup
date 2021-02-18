using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Zorrero.Utils.IntervalLookup.Model;

namespace Zorrero.Utils.IntervalLookup.Tests
{
    public class ImmutableIntervalBalancedTreeTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ImmutableIntervalBalancedTreeTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldBuildCorrectlySortedTreeLayersComplete()
        {
            var intervalNodeFour = new IntervalWithValue<long, long>(0, 5, 1);
            var nodeFour = new TreeNode<long, long>(intervalNodeFour, null, null);
            var intervalNodeFive = new IntervalWithValue<long, long>(10, 15, 2);
            var nodeFive = new TreeNode<long, long>(intervalNodeFive, null, null);
            var intervalNodeSix = new IntervalWithValue<long, long>(20, 25, 0);
            var nodeSix = new TreeNode<long, long>(intervalNodeSix, null, null);
            var intervalNodeSeven = new IntervalWithValue<long, long>(30, 35, 0);
            var nodeSeven = new TreeNode<long, long>(intervalNodeSeven, null, null);
            var intervalNodeTwo = new IntervalWithValue<long, long>(5, 10, 1);
            var nodeTwo = new TreeNode<long, long>(intervalNodeTwo, nodeFour, nodeFive);
            var intervalNodeThree = new IntervalWithValue<long, long>(25, 30, 2);
            var nodeThree = new TreeNode<long, long>(intervalNodeThree, nodeSix, nodeSeven);
            var intervalNodeOne = new IntervalWithValue<long, long>(15, 20, 0);
            var nodeOne = new TreeNode<long, long>(intervalNodeOne, nodeTwo, nodeThree);
            var treeIntervals = new List<IntervalWithValue<long, long>>
            {
                intervalNodeOne, intervalNodeTwo, intervalNodeThree, intervalNodeFour, intervalNodeFive,
                intervalNodeSix, intervalNodeSeven
            };
            var tree = new ImmutableIntervalBalancedTree<long, long>(treeIntervals);
            Assert.Equal(nodeOne, tree.Root);
        }

        [Fact]
        public void ShouldBuildCorrectlySortedTreeLayersIncomplete()
        {
            var intervalNodeFour = new IntervalWithValue<long, long>(0, 5, 1);
            var nodeFour = new TreeNode<long, long>(intervalNodeFour, null, null);
            var intervalNodeFive = new IntervalWithValue<long, long>(10, 15, 2);
            var nodeFive = new TreeNode<long, long>(intervalNodeFive, null, null);
            var intervalNodeSix = new IntervalWithValue<long, long>(20, 25, 0);
            var nodeSix = new TreeNode<long, long>(intervalNodeSix, null, null);
            var intervalNodeTwo = new IntervalWithValue<long, long>(5, 10, 1);
            var nodeTwo = new TreeNode<long, long>(intervalNodeTwo, nodeFour, nodeFive);
            var intervalNodeThree = new IntervalWithValue<long, long>(25, 30, 2);
            var nodeThree = new TreeNode<long, long>(intervalNodeThree, nodeSix, null);
            var intervalNodeOne = new IntervalWithValue<long, long>(15, 20, 0);
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
            var intervalNodeFour = new IntervalWithValue<long, long>(0, 5, 1);
            var intervalNodeFive = new IntervalWithValue<long, long>(10, 15, 2);
            var intervalNodeSix = new IntervalWithValue<long, long>(20, 25, 0);
            var intervalNodeSeven = new IntervalWithValue<long, long>(30, 35, 0);
            var intervalNodeTwo = new IntervalWithValue<long, long>(5, 10, 1);
            var intervalNodeThree = new IntervalWithValue<long, long>(25, 30, 2);
            var intervalNodeOne = new IntervalWithValue<long, long>(15, 20, 0);
            var treeIntervals = new List<IntervalWithValue<long, long>>
            {
                intervalNodeOne, intervalNodeTwo, intervalNodeThree, intervalNodeFour, intervalNodeFive,
                intervalNodeSix, intervalNodeSeven
            };
            var tree = new ImmutableIntervalBalancedTree<long, long>(treeIntervals);
            var result = tree.Search(3, false, false);
            Assert.Single(result);
            Assert.Equal(intervalNodeFour, result.First());
        }
        
        [Fact]
        public void ShouldSearchInLargeAmountOfExclusiveIntervals()
        {
            var intervals = new List<IntervalWithValue<long, long>>();
            for (var i = 0; i < 50000000; i += 50) intervals.Add(new IntervalWithValue<long, long>(i, i + 50, 0));
            var tree = new ImmutableIntervalBalancedTree<long, long>(intervals);

            var result = tree.Search(25, false, false);
            Assert.Single(result);
        }

        [Fact]
        public void ShouldSearchInLargeAmountOfOverlappedIntervals()
        {
            var intervals = new List<IntervalWithValue<long, long>>();
            for (var i = 0; i < 25000000; i += 25) intervals.Add(new IntervalWithValue<long, long>(i, i + 50, 0));
            var tree = new ImmutableIntervalBalancedTree<long, long>(intervals);
            
            var result = tree.Search(100000, true, true);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void ShouldResolveCorrectlyEnumerator()
        {
            var intervalNodeFour = new IntervalWithValue<long, long>(0, 5, 1);
            var intervalNodeFive = new IntervalWithValue<long, long>(10, 15, 2);
            var intervalNodeSix = new IntervalWithValue<long, long>(20, 25, 0);
            var intervalNodeSeven = new IntervalWithValue<long, long>(30, 35, 0);
            var intervalNodeTwo = new IntervalWithValue<long, long>(5, 10, 1);
            var intervalNodeThree = new IntervalWithValue<long, long>(25, 30, 2);
            var intervalNodeOne = new IntervalWithValue<long, long>(15, 20, 0);
            var treeIntervals = new List<IntervalWithValue<long, long>>
            {
                intervalNodeOne, intervalNodeTwo, intervalNodeThree, intervalNodeFour, intervalNodeFive,
                intervalNodeSix, intervalNodeSeven
            };
            var tree = new ImmutableIntervalBalancedTree<long, long>(treeIntervals);
            using var enumerator = tree.GetEnumerator();
            var enumeratorList = new List<IntervalWithValue<long, long>>();
            while (enumerator.MoveNext())
            {
                enumeratorList.Add(enumerator.Current);
            }
            enumeratorList.Sort();
            treeIntervals.Sort();
            Assert.Equal(treeIntervals, enumeratorList);
        }

        [Fact]
        public void ShouldCountCorrectlyItems()
        {
            var intervalNodeFour = new IntervalWithValue<long, long>(0, 5, 1);
            var intervalNodeFive = new IntervalWithValue<long, long>(10, 15, 2);
            var intervalNodeSix = new IntervalWithValue<long, long>(20, 25, 0);
            var intervalNodeSeven = new IntervalWithValue<long, long>(30, 35, 0);
            var intervalNodeTwo = new IntervalWithValue<long, long>(5, 10, 1);
            var intervalNodeThree = new IntervalWithValue<long, long>(25, 30, 2);
            var intervalNodeOne = new IntervalWithValue<long, long>(15, 20, 0);
            var treeIntervals = new List<IntervalWithValue<long, long>>
            {
                intervalNodeOne, intervalNodeTwo, intervalNodeThree, intervalNodeFour, intervalNodeFive,
                intervalNodeSix, intervalNodeSeven
            };
            var tree = new ImmutableIntervalBalancedTree<long, long>(treeIntervals);
            Assert.Equal(7, tree.Count);
        }
        
        [Fact]
        public void ShouldCountCorrectlyLargeAmountOfIntervals()
        {
            var intervals = new List<IntervalWithValue<long, long>>();
            for (var i = 0; i < 25000000; i += 25) intervals.Add(new IntervalWithValue<long, long>(i, i + 50, 0));
            var tree = new ImmutableIntervalBalancedTree<long, long>(intervals);
            Assert.Equal(1000000, tree.Count);
        }
    }
}