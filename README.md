##### Table of Contents  
[Objective](#Objective)  
[Important details](#ImportantDetails)  
[Implementation](#Implementation)  
[Example](#Example)  

### Objective  
The objective of this library is to provide a way to search for values in intervals.
<a name="ImportantDetails"/>
### Important details  
* The provided data structure is immutable.
* The interval may have associated a value from a different type of the init and end.
* Intervals may be overlapped.
* An interval is considered greater than other interval if its end value is greater than the others interval end value.
* An interval is considered lower than other interval if its init value is greater than the others interval init value.
* An interval internal value is not taken in account while evaluating the interval, its only purpose is to me stored and queried.
  
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
```c#
var tree = new ImmutableIntervalBalancedTree<long, MyClass>(intervals);
```

### Example
Suppose you want to search among intervals of long with a string value, then the definition would be like
```c#
var intervals = new List<Interval<long, string>>{
  new Interval<long, string>(0,5,""),
  new Interval<long, string>(4,10,""),
  new Interval<long, string>(15,25,"")
};
var tree = new ImmutableIntervalBalancedTree<long, string>(intervals);
```
To do a search in the tree simply call the exposed method. This method receives the value to search, if the init value should be included as part of the interval and if the end value should be included as part of the interval. In this case init is excluded and end is included, the method returns a IEnumerable of intervals, being this result the IEnumerable of all intervals in the tree where the given value was contained.
```c#
var result = tree.Search(2, includeInit: false, includeEnd: true);
```
