# Step-by-step - Test it or Die Trying (35')
## Calculator
- We start by a first simple test case : `9 + 3 = 12`
  - We create a `CalculatorShould`
  - We use the 3A pattern to describe it
  - We assert the expected result

```c#
public class CalculatorShould
{
    [Test]
    public void SupportAdd()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Calculate(9, 3, Calculator.Add);

        // Assert
        Assert.That(result, Is.EqualTo(12));
    }
}
```
- Now that we have 1 test case we can repeat for others
```text
✅ 9 + 3 = 12
3 x 76 = 228
9 / 3 = 3
9 - 3 = 9
Unsupported operator should fail
```

- Other test cases
```c#
[Test]
public void SupportMultiply()
{
    // Arrange
    var calculator = new Calculator();

    // Act
    var result = calculator.Calculate(3,76, Calculator.Multiply);

    // Assert
    Assert.That(result, Is.EqualTo(228));

}

[Test]
public void SupportDivide()
{
    // Arrange
    var calculator = new Calculator();

    // Act
    var result = calculator.Calculate(9,3, Calculator.Divide);

    // Assert
    Assert.That(result, Is.EqualTo(3));
}

[Test]
public void SupportSubtract()
{
    // Arrange
    var calculator = new Calculator();

    // Act
    var result = calculator.Calculate(9,3, Calculator.Subtract);

    // Assert
    Assert.That(result, Is.EqualTo(6));
}
```
- Update our test cases
```text
✅ 9 + 3 = 12
✅ 3 x 76 = 228
✅ 9 / 3 = 3
✅ 9 - 3 = 9
Unsupported operator should fail
```

- Let's implement the failure test
```c#
[Test]
public void FailWhenOperatorNotSupported()
{
    var calculator = new Calculator();
    var exception = Assert.Throws<ArgumentException>(() => calculator.Calculate(9, 3, "UnsupportedOperator"));
    Assert.That(exception!.Message, Is.EqualTo("Not supported operator"));
}
```

### Refactor the tests
> You should pay the same attention to your tests as to your production code.

- Let's remove duplication
  - assertion code is duplicated
  - we instantiate 1 `Calculator` per test, but we can use the same (no state inside)
- In `NUnit` for this kind of test we can use `Parameterized tests`
  - We define `TestCases`
  - We adapt the test method as well `public void SupportOperations(int a, int b, string @operator, int expectedResult)`
- We can use a library that simplify assertion readability as well called [FluentAssertions](https://fluentassertions.com/)

```c#
public class RefactoredCalculatorShould
{
    private readonly Calculator _calculator = new();

    [TestCase(9, 3, Add, 12)]
    [TestCase(3, 76, Multiply, 228)]
    [TestCase(9, 3, Divide, 3)]
    [TestCase(9, 3, Subtract, 6)]
    public void SupportOperations(int a, int b, string @operator, int expectedResult) =>
        _calculator
            .Calculate(a, b, @operator)
            .Should()
            .Be(expectedResult);

    [Test]
    public void FailWhenOperatorNotSupported() =>
        _calculator.Invoking(_ => _.Calculate(9, 3, "UnsupportedOperator"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Not supported operator");
}
```

> There is one Edge case not yet supported, what happens if we divide by 0? 

```text
✅ 9 + 3 = 12
✅ 3 x 76 = 228
✅ 9 / 3 = 3
✅ 9 - 3 = 9
✅ Unsupported operator should fail
Divide by 0 should fail
```

## Time
- Write at least one test for it

```c#
public class TimeUtility
{
    public string GetTimeOfDay()
    {
        var time = DateTime.Now;

        return time.Hour switch
        {
            >= 0 and < 6 => "Night",
            >= 6 and < 12 => "Morning",
            >= 12 and < 18 => "Afternoon",
            _ => "Evening"
        };
    }
}
```

- Here is the simplest test we can write
  - Which problem will you encounter?
```c#
public class TimeUtilityShould
{
    [Test]
    public void BeAfternoon() =>
        new TimeUtility()
            .GetTimeOfDay()
            .Should()
            .Be("Afternoon");
}
```

- This test is not repeatable because the design is coupled to `LocalTime.now()`
    - We need to isolate it to be able to test this unitary
    - A few solutions here :
        - Pass a `DateTime` as method arg
        - Pass a `IClock` which will provide a `Now()`method that we will be able to substitute
        - Pass a function `clock: void => DateTime`

- Identify some examples
```text
6:05AM -> Morning
1:00AM -> Night
1PM -> Afternoon
11PM -> Evening
```        

### Use an IClock interface
- Adapt the `TimeUtility` to inject an `IClock` collaborator
  - Generate your code from usage

```c#
public class TimeUtility
{
    private readonly IClock _clock;

    public TimeUtility(IClock clock) => _clock = clock;

    public string GetTimeOfDay()
    {
        return _clock.Now().Hour switch
        {
            >= 0 and < 6 => "Night",
            >= 6 and < 12 => "Morning",
            >= 12 and < 18 => "Afternoon",
            _ => "Evening"
        };
    }
}

public interface IClock
{
    DateTime Now();
}
```

- Now our code has no `hardcoded` dependency anymore
- We need to adapt our tests
  - How to provide a `IClock` in the given state for our test cases?
  - `Test Doubles` is our solution

- To use TestDoubles we need to use `moq`
  - Instantiate a `IClock` mock
  - We implement our first test case

```c#
[Test]
public void ReturnMorningFor6Am()
{
    var clockMock = new Mock<IClock>();
    clockMock.Setup(c => c.Now())
        .Returns(new DateTime(2022, 12, 1, 6, 5, 0, 0));
    
    new TimeUtility(clockMock.Object)
        .GetTimeOfDay()
        .Should()
        .Be("Morning");
}
```

```text
✅ 6:05AM -> Morning
1:00AM -> Night
1PM -> Afternoon
11PM -> Evening
```

- Implement others by using a `Theory` once again

```c#
[TestCase(0, "Night")]
[TestCase(4, "Night")]
[TestCase(6, "Morning")]
[TestCase(9, "Morning")]
[TestCase(12, "Afternoon")]
[TestCase(17, "Afternoon")]
[TestCase(18, "Evening")]
[TestCase(23, "Evening")]
public void GetADescriptionAtAnyTime(int hour, string expectedDescription)
{
    var clockMock = new Mock<IClock>();
    clockMock.Setup(c => c.Now())
        .Returns(hour.ToDateTime());
    
    new TimeUtility(clockMock.Object)
        .GetTimeOfDay()
        .Should()
        .Be(expectedDescription);
}

internal static class TestExtensions
{
    public static DateTime ToDateTime(this int hour) => new(2022, 12, 1, hour, 0, 0, 0);
}
```