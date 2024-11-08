using System;
using NUnit.Framework;

namespace Workshop.Answers._01._Fundamentals;

public class PlayWithFunctions
{
    private static readonly Func<int, int, int> Add = (x, y) => x + y;
    private static readonly Func<int, int, int> Multiply = (x, y) => x * y;
    private static readonly Func<int, int, int> Divide = (x, y) => x / y;

    private static readonly Func<int, int> Add1 = x => Add(1, x);
    private static readonly Func<int, int> Double = x => Multiply(x, 2);

    [Test]
    public void Add1AndDoubleIt()
    {
        // Create an Add1 function based on Add function
        // Create a Double function based on Multiply
        // Compose the 2 functions together to implement the Add1AndDouble function
        int Add1AndDouble(int x) => Double(Add1(x));

        Assert.AreEqual(6, Add1AndDouble(2));
    }
}