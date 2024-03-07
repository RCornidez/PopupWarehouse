# Testing

## How-to-run

```
dotnet test -v n
```



## Reference

### NUnit Attributes:

- [Test]: Marks a method as a test method.
- [TestFixture]: Indicates that a class contains tests. It can also be used for setting up a common set of objects for multiple test methods.
- [SetUp]: Marks a method that runs before each test method in the class. Useful for initializing common objects and data.
- [TearDown]: Marks a method that runs after each test method in the class. Useful for cleanup activities.
- [OneTimeSetUp]: Marks a method that runs once before any of the tests in its test fixture run. Useful for setting up expensive resources, like database connections.
- [OneTimeTearDown]: Marks a method that runs once after all the tests in its test fixture have run. Useful for cleaning up resources initialized by the [OneTimeSetUp] method.
- [TestCase]: Allows providing inline data to a test method, enabling parameterized tests.
- [TestCaseSource]: Specifies a method, field, or property that provides test cases, allowing for more complex parameterized tests.


### NUnit Assertion Methods:

- Assert.AreEqual(expected, actual): Checks that two values are equal. If not, the test fails.
- Assert.AreNotEqual(notExpected, actual): Checks that two values are not equal. If they are equal, the test fails.
- Assert.IsTrue(condition): Checks that a condition is true. If false, the test fails.
- Assert.IsFalse(condition): Checks that a condition is false. If true, the test fails.
- Assert.IsNull(object): Checks that an object is null. If not, the test fails.
- Assert.IsNotNull(object): Checks that an object is not null. If null, the test fails.
- Assert.Throws<ExceptionType>(TestDelegate): Expects a specific exception to be thrown by the code executed in the delegate.
- Assert.That(actual, Is.EqualTo(expected)): An alternative syntax for asserting that two values are equal using constraint-based model.