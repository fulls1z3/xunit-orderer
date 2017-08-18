# xUnit Orderer
> Please support this project by simply putting a Github star. Share this library with friends on Twitter and everywhere else you can.

**`xUnit Orderer`** is an implementation of ITestCaseOrderer enforcing [xUnit] to run the facts in strict order.

There're some scenarios that you might need to chain the `fact`s to be evaluated in a strict order. This project implements the `ITestCaseOrderer` interface of [xUnit] and provides the `TestPriority` decorator to perform the test case ordering.

Check out the example below for details.

## Prerequisites
Packages in this project depend on
- [xUnit v2.2.0](https://www.nuget.org/packages/xunit)

> Older versions contain outdated dependencies, might produce errors.

## Getting started
### Installation
You can install **`xUnit Orderer`** by running following commands in the Package Manager Console
```
Install-Package XunitOrderer
```

## Usage
#### AssemblyInfo.cs
```csharp
using System.Reflection;
using System.Runtime.InteropServices;
using Xunit;

...

[assembly:CollectionBehavior(DisableTestParallelization = true)]
[assembly:TestCollectionOrderer("XunitOrderer.TestCollectionOrderer", "XunitOrderer.Testing")]
```

#### TestClass1.cs
```csharp
[TestPriority(10)]
public class TestClass1 : TestClassBase
{
    [Fact]
    [TestPriority(101)]
    public void First()
    {
        Assert.Equal(1, 1);
    }
 
    [Fact]
    [TestPriority(102)]
    public void Second()
    {
        Assert.Equal(2, 2);
    }
}
```

#### TestClass2.cs
```csharp
[TestPriority(20)]
public class TestClass2 : TestClassBase
{
    [Fact]
    [TestPriority(103)]
    public void Third()
    {
        Assert.Equal(3, 3);
    }
 
    [Fact]
    [TestPriority(104)]
    public void Fourth()
    {
        Assert.Equal(4, 4);
    }
}
```

The execution order of the `fact`s are:
- TestClass1 -> First
- TestClass1 -> Second
- TestClass2 -> Third
- TestClass2 -> Fourth

> Built with `.NET Framework v4.6.2`, solution currently supports `xUnit v2.2.0`.

## Contributing
If you want to file a bug, contribute some code, or improve documentation, please read up on the following contribution guidelines:
- [Issue guidelines](CONTRIBUTING.md#submit)
- [Contributing guidelines](CONTRIBUTING.md)
- [Coding rules](CONTRIBUTING.md#rules)
- [ChangeLog](CHANGELOG.md)

## License
The MIT License (MIT)

Copyright (c) 2017 [Burak Tasci]

[xUnit]: https://xunit.github.io/
[Burak Tasci]: http://www.buraktasci.com
