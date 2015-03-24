**ModelMatcher** makes vertifying models in your unit tests easier and cleaner. 

No more Linq statements to assert that an item is in a collection, and no need for for ten lines of assert statements just to check that all of the properties on a model are correct.

## Match single items ...

```csharp
var result = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value",
    Property2 = 123,
    Property3 = false
};

result.ShouldMatch(expectedResult);
```

## .. or collections

One match ...

```csharp
var resultCollection = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value",
    Property2 = 123,
    Property3 = false
};

resultCollection.ShouldContainAMatch(expectedResult);
```

... or multiple matches

```csharp
var resultCollection = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value",
    Property2 = 123,
    Property3 = false
};

resultCollection.ShouldContainMatches(expectedResult, Matches.Two);
```
