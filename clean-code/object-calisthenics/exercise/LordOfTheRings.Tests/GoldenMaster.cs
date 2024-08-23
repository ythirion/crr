using LordOfTheRings.App;

namespace LordOfTheRings.Tests;

public class GoldenMaster
{
    private StringWriter _console;

    [SetUp]
    public void Setup()
    {
        _console = new StringWriter();
        Console.SetOut(_console);
    }

    [Test]
    public Task Run_Application()
    {
        Application.Run();
        return Verify(_console);
    }

    [TearDown]
    public void TearDown() => _console.Dispose();
}