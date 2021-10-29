##### Table of Contents  
[Objective](#Objective)  
[Dependencies](#Dependencies)    
[Important details](#ImportantDetails)  
[Implementation](#Implementation)  
[Example](#Example)  
[Further work](#FurtherWork)  

### Objective  
The objective of this library is to provide a way to search for values in intervals.
### Dependencies
* Base library
    * .net core 3.1
* Tests
    * .net core 3.1
    * Microsoft.NET.Test.Sdk 16.5.0
    * xunit 2.4.0
    * xunit.runner.visualstudio 2.4.0
    * coverlet.collector 1.2.0
  <a name="ImportantDetails"/>
### Important details  
* There is a mutable structure and an immutable structure provided.
* The interval may have associated a value from a different type of the init and end.
* Intervals may be overlapped, this option can be changed by activating overlapping detection.
* An interval is considered greater than other interval if its end value is greater than the others interval end value.
* An interval is considered lower than other interval if its init value is greater than the others interval init value.
* An interval internal value is not taken in account while evaluating the interval, its only purpose is to me stored and queried.
* ImmutableIntervalBalancedTree implements IReadOnlyCollection and MutableIntervalBalancedTree implements ICollection, base class IntervalBalancedTree implements IEnumerable.
  
### Implementation  
The library uses a balanced binary tree to locate the intervals and searches over the nodes for matching intervals given a value T.
#### Interval
Interval is a class that uses T and TK, where T must implement comparable and TK is the value associated to the given interval.
```c#
var interval = new Interval<long, MyClass>(init, end, new MyClass());
```
#### Node
Node contains its interval and may contain a left child and a right child. It also uses T and TK. This class is not intended to be manipulated directly.
```c#
var node = new TreeNode<long, MyClass>(interval, null, null);
```
#### Tree
The Tree is the actual class used to do the search, it receives a IEnumerable of Interval, builds the nodes and exposes a method to search a value in those intervals.

The Tree also offers a validation in the constructor to detect overlapping intervals, to activate this function pass a boolean as second parameter to the constructor (By default is false), if validation is unsuccessfull a IntervalOverlappedException will be thrown.

##### Immutable Tree
```c#
var tree = new ImmutableIntervalBalancedTree<long, MyClass>(intervals);
``` 
##### Mutable Tree
```c#
var tree = new MutableIntervalBalancedTree<long, MyClass>(intervals);
``` 

### Example
Suppose you want to search among intervals of long with a string value, then the definition would be like
```c#
var intervals = new List<Interval<long, string>>{
  new IntervalWithValue<long, string>(0,5,""),
  new IntervalWithValue<long, string>(4,10,""),
  new IntervalWithValue<long, string>(15,25,"")
};
var tree = new ImmutableIntervalBalancedTree<long, string>(intervals);
//Tree with overlapping validation
var tree = new ImmutableIntervalBalancedTree<long, string>(intervals, true);
//Mutable tree
var tree = new MutableIntervalBalancedTree<long, string>(intervals);
//Mutable tree with overlapping validation
var tree = new MutableIntervalBalancedTree<long, string>(intervals, true);
```
To do a search in the tree simply call the exposed method. This method receives the value to search, if the init value should be included as part of the interval and if the end value should be included as part of the interval. In this case init is excluded and end is included, the method returns a List of intervals, being this result the List of all intervals in the tree where the given value was contained.
```c#
var result = tree.Search(2, includeInit: false, includeEnd: true);
```
To add and remove elements from the tree simply call the exposed methods, both of them receive the Interval to delete.
```c#
var intervalWithValue = new IntervalWithValue<long, MyClass>();
tree.Add(intervalWithValue);
var result = tree.Remove(intervalWithValue);
```

### Further work
For add and removal operation in MutableIntervalBalancedTree the elements were modified in a list and then a new tree was created, in the future there will be added a Self Balanced Tree for these cases. 