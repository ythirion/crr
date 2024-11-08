using System.Collections.Generic;
using System.Text.Json.Serialization;
using Workshop.Tests._03._Real_World.Watch;

namespace Workshop.Tests._03._Real_World.Pocos;

public class Component
{
    public string Name { get; set; }
    public string Id { get; set; }

    [JsonConverter(typeof(ComponentTypeConverter))]
    public ComponentType Type { get; set; }
    public List<Component> Components { get; set; } = [];
}