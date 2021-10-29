using System.Collections.Generic;
using System.Linq;
using Xunit;
using Zorrero.Utils.IntervalLookup.Exceptions;
using Zorrero.Utils.IntervalLookup.Model;

namespace Zorrero.Utils.IntervalLookup.Tests
{
    public class MutableIntervalBalancedTreeTests
    {
        [Fact]
        public void OnNullRootSearchMultipleShouldReturnEmptyList()
        {
            var tree = new MutableIntervalBalancedTree<long, long>(new List<IntervalWithValue<long, long>>());
            Assert.Empty(tree.Search(0, false, false));
        }

        [Fact]
        public void OnNullRootSearchFirstShouldReturnNull()
        {
            var tree = new MutableIntervalBalancedTree<long, long>(new List<IntervalWithValue<long, long>>());
            Assert.Null(tree.SearchFirst(0, false, false));
        }

        [Fact]
        public void OnNullRootGetEnumeratorShouldReturnEmptyListEnumerator()
        {
            var tree = new MutableIntervalBalancedTree<long, long>(new List<IntervalWithValue<long, long>>());
            using var enumerator = tree.GetEnumerator();
            Assert.Null(enumerator.Current);
        }

        [Fact]
        public void OnNullRootContainsShouldReturnFalse()
        {
            var tree = new MutableIntervalBalancedTree<long, long>(new List<IntervalWithValue<long, long>>());
            Assert.DoesNotContain(new IntervalWithValue<long, long>(0, 0, 0), tree);
        }

        [Fact]
        public void OnNotNullRootAndExistingValueContainsShouldReturnTrue()
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
            var tree = new MutableIntervalBalancedTree<long, long>(treeIntervals);
            Assert.Contains(new IntervalWithValue<long, long>(10, 15, 2), tree);
        }

        [Fact]
        public void OnNotNullRootAndNotExistingValueContainsShouldReturnFalse()
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
            var tree = new MutableIntervalBalancedTree<long, long>(treeIntervals);
            Assert.DoesNotContain(new IntervalWithValue<long, long>(100, 115, 2), tree);
        }

        [Fact]
        public void ShouldRemoveCorrectlyIntervalFromTree()
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
            var tree = new MutableIntervalBalancedTree<long, long>(treeIntervals);
            Assert.True(tree.Remove(new IntervalWithValue<long, long>(15, 20, 0)));
            Assert.Equal(6, tree.Count);
            Assert.DoesNotContain(new IntervalWithValue<long, long>(15, 20, 0), tree);
        }

        [Fact]
        public void ShouldClearCorrectlyTree()
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
            var tree = new MutableIntervalBalancedTree<long, long>(treeIntervals);
            Assert.Equal(7, tree.Count);
            tree.Clear();
            Assert.Empty(tree);
        }

        [Fact]
        public void ShouldNotRemoveNotExistingIntervalFromTree()
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
            var tree = new MutableIntervalBalancedTree<long, long>(treeIntervals);
            Assert.False(tree.Remove(new IntervalWithValue<long, long>(115, 120, 0)));
            Assert.Equal(7, tree.Count);
        }

        [Fact]
        public void ShouldAddCorrectlyOnEmptyTree()
        {
            var tree = new MutableIntervalBalancedTree<long, long>(new List<IntervalWithValue<long, long>>());
            Assert.Empty(tree);
            tree.Add(new IntervalWithValue<long, long>(115, 120, 0));
            Assert.Contains(new IntervalWithValue<long, long>(115, 120, 0), tree);
            Assert.Single(tree);
        }

        [Fact]
        public void ShouldReturnFalseOnRemoveOnEmptyTree()
        {
            var tree = new MutableIntervalBalancedTree<long, long>(new List<IntervalWithValue<long, long>>());
            Assert.False(tree.Remove(new IntervalWithValue<long, long>(115, 120, 0)));
        }

        [Fact]
        public void ShouldReturnFalseOnReadOnly()
        {
            Assert.False(new MutableIntervalBalancedTree<long, long>(new List<IntervalWithValue<long, long>>())
                .IsReadOnly);
        }

        [Fact]
        public void ShouldCopyCorrectlyTreeIntoArray()
        {
            var intervals = new IntervalWithValue<long, long>[7];
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
            var tree = new MutableIntervalBalancedTree<long, long>(treeIntervals);
            tree.CopyTo(intervals, 0);
            treeIntervals.Sort();
            var intervalsAsList = intervals.ToList();
            intervalsAsList.Sort();
            Assert.Equal(treeIntervals, intervalsAsList);
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
            var tree = new MutableIntervalBalancedTree<long, long>(treeIntervals);
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
            var tree = new MutableIntervalBalancedTree<long, long>(treeIntervals);
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
            var tree = new MutableIntervalBalancedTree<long, long>(treeIntervals);
            var result = tree.Search(3, false, false);
            Assert.Single(result);
            Assert.Equal(intervalNodeFour, result.First());
        }

        [Fact]
        public void ShouldSearchInLargeAmountOfExclusiveIntervals()
        {
            var intervals = new List<IntervalWithValue<long, long>>();
            for (var i = 0; i < 50000000; i += 50) intervals.Add(new IntervalWithValue<long, long>(i, i + 50, 0));
            var tree = new MutableIntervalBalancedTree<long, long>(intervals);

            var result = tree.Search(25, false, false);
            Assert.Single(result);
        }

        [Fact]
        public void ShouldSearchFirstInLargeAmountOfExclusiveIntervals()
        {
            var intervals = new List<IntervalWithValue<long, long>>();
            for (var i = 0; i < 50000000; i += 50) intervals.Add(new IntervalWithValue<long, long>(i, i + 50, 0));
            var tree = new MutableIntervalBalancedTree<long, long>(intervals);
            var expected = new IntervalWithValue<long, long>(0, 50, 0);

            var result = tree.SearchFirst(25, false, false);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldSearchFirstInLargeAmountOfExclusiveIntervalsUpper()
        {
            var intervals = new List<IntervalWithValue<long, long>>();
            for (var i = 0; i < 50000000; i += 50) intervals.Add(new IntervalWithValue<long, long>(i, i + 50, 0));
            var tree = new MutableIntervalBalancedTree<long, long>(intervals);
            var expected = new IntervalWithValue<long, long>(49999900, 50000000, 0);

            var result = tree.SearchFirst(49999925, false, false);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldGetNullFirstInLargeAmountOfExclusiveIntervals()
        {
            var intervals = new List<IntervalWithValue<long, long>>();
            for (var i = 0; i < 50000000; i += 50) intervals.Add(new IntervalWithValue<long, long>(i, i + 50, 0));
            var tree = new MutableIntervalBalancedTree<long, long>(intervals);

            var result = tree.SearchFirst(-1, false, false);
            Assert.Null(result);
        }

        [Fact]
        public void ShouldSearchInLargeAmountOfOverlappedIntervals()
        {
            var intervals = new List<IntervalWithValue<long, long>>();
            for (var i = 0; i < 25000000; i += 25) intervals.Add(new IntervalWithValue<long, long>(i, i + 50, 0));
            var tree = new MutableIntervalBalancedTree<long, long>(intervals);

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
            var tree = new MutableIntervalBalancedTree<long, long>(treeIntervals);
            using var enumerator = tree.GetEnumerator();
            var enumeratorList = new List<IntervalWithValue<long, long>>();
            while (enumerator.MoveNext()) enumeratorList.Add(enumerator.Current);
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
            var tree = new MutableIntervalBalancedTree<long, long>(treeIntervals);
            Assert.Equal(7, tree.Count);
        }

        [Fact]
        public void ShouldCountCorrectlyLargeAmountOfIntervals()
        {
            var intervals = new List<IntervalWithValue<long, long>>();
            for (var i = 0; i < 25000000; i += 25) intervals.Add(new IntervalWithValue<long, long>(i, i + 50, 0));
            var tree = new MutableIntervalBalancedTree<long, long>(intervals);
            Assert.Equal(1000000, tree.Count);
        }

        [Fact]
        public void ShouldGenerateCorrectlyTreeWithOverlappingCheckOnNoIntervalsOverlapped()
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
            var tree = new MutableIntervalBalancedTree<long, long>(treeIntervals, true);
            Assert.Equal(7, tree.Count);
        }

        [Fact]
        public void ShouldThrowExceptionWithOverlappingCheckOnOneIntervalOverlapped()
        {
            var intervalNodeFour = new IntervalWithValue<long, long>(0, 5, 1);
            var intervalNodeFive = new IntervalWithValue<long, long>(9, 15, 2);
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
            Assert.Throws<IntervalsOverlappedException>(() =>
                new MutableIntervalBalancedTree<long, long>(treeIntervals, true));
        }

        [Fact]
        public void ShouldThrowExceptionWithOverlappingCheckOnAllIntervalOverlapped()
        {
            var intervalNodeFour = new IntervalWithValue<long, long>(0, 5, 1);
            var intervalNodeFive = new IntervalWithValue<long, long>(0, 5, 2);
            var intervalNodeSix = new IntervalWithValue<long, long>(0, 5, 3);
            var intervalNodeSeven = new IntervalWithValue<long, long>(0, 5, 4);
            var intervalNodeTwo = new IntervalWithValue<long, long>(0, 5, 5);
            var intervalNodeThree = new IntervalWithValue<long, long>(0, 5, 6);
            var intervalNodeOne = new IntervalWithValue<long, long>(0, 5, 7);
            var treeIntervals = new List<IntervalWithValue<long, long>>
            {
                intervalNodeOne, intervalNodeTwo, intervalNodeThree, intervalNodeFour, intervalNodeFive,
                intervalNodeSix, intervalNodeSeven
            };
            Assert.Throws<IntervalsOverlappedException>(() =>
                new MutableIntervalBalancedTree<long, long>(treeIntervals, true));
        }

        [Fact]
        public void ShouldThrowExceptionWithOverlappingCheckOnContainedIntervalsOverlapped()
        {
            var intervalNodeFour = new IntervalWithValue<long, long>(1, 15, 1);
            var intervalNodeFive = new IntervalWithValue<long, long>(2, 3, 2);
            var intervalNodeSix = new IntervalWithValue<long, long>(4, 5, 3);
            var intervalNodeSeven = new IntervalWithValue<long, long>(6, 7, 4);
            var intervalNodeTwo = new IntervalWithValue<long, long>(8, 9, 5);
            var intervalNodeThree = new IntervalWithValue<long, long>(10, 11, 6);
            var intervalNodeOne = new IntervalWithValue<long, long>(12, 13, 7);
            var treeIntervals = new List<IntervalWithValue<long, long>>
            {
                intervalNodeOne, intervalNodeTwo, intervalNodeThree, intervalNodeFour, intervalNodeFive,
                intervalNodeSix, intervalNodeSeven
            };
            Assert.Throws<IntervalsOverlappedException>(() =>
                new MutableIntervalBalancedTree<long, long>(treeIntervals, true));
        }
    }
}