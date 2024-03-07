How to Run Tests:

```
dotnet test -v n
```

NUnit Attributes:

```
[Test]: Marks a test method.
[TestFixture]: Denotes a class with tests.
[SetUp]: Initializes before each test.
[TearDown]: Cleans up after each test.
[OneTimeSetUp]: Runs once before tests in a class.
[OneTimeTearDown]: Runs once after all tests in a class.
[TestCase]: Provides inline test data.
[TestCaseSource]: Specifies a source for test data.
```

Assertion Methods:

```
Equality: Assert.That(actual, Is.EqualTo(expected));
Inequality: Assert.That(actual, Is.Not.EqualTo(notExpected));
Booleans: Assert.That(condition, Is.True); | Assert.That(condition, Is.False);
Nullability: Assert.That(object, Is.Null); | Assert.That(object, Is.Not.Null);
Exceptions: Assert.That(() => { /* code */ }, Throws.TypeOf<ExceptionType>());
Collections: Assert.That(collection, Is.Empty); | Assert.That(collection, Has.Exactly(1).InstanceOf<ExpectedType>());
```

Use the constraint-based model for readable and expressive tests. Adjust [TestCase] and [TestCaseSource] for parameterized testing, ensuring each test is self-contained with [SetUp] and [TearDown].