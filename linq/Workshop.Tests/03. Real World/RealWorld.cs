using System.Text.Json;
using NUnit.Framework;
using Workshop.Tests._03._Real_World.Pocos;
using static System.IO.File;
using static System.Text.Json.JsonSerializer;

namespace Workshop.Tests._03._Real_World;

public class RealWorld
{
    private Component _watch;

    [OneTimeSetUp]
    public void GlobalSetup() => _watch = DeserializeWatch();

    private static Component DeserializeWatch()
        => Deserialize<Component>(
            ReadAllText("03. Real World/watch.json"),
            new JsonSerializerOptions {PropertyNameCaseInsensitive = true}
        );

    [Test]
    public void Load()
    {
        Assert.AreEqual(400, _watch.Components.Count);
    }
}