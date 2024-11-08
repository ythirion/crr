using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Workshop.Tests._03._Real_World.Pocos;

namespace Workshop.Tests._03._Real_World.Watch;

public class ComponentTypeConverter : JsonConverter<ComponentType>
{
    public override ComponentType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();

        return stringValue switch
        {
            "Balance Wheel" => ComponentType.BalanceWheel,
            "Shock Absorber" => ComponentType.ShockAbsorber,
            "Gear Train" => ComponentType.GearTrain,
            _ => (ComponentType) Enum.Parse(typeof(ComponentType), stringValue.Replace(" ", ""))
        };
    }

    public override void Write(Utf8JsonWriter writer, ComponentType value, JsonSerializerOptions options)
    {
        string stringValue = value switch
        {
            ComponentType.BalanceWheel => "Balance Wheel",
            ComponentType.ShockAbsorber => "Shock Absorber",
            ComponentType.GearTrain => "Gear Train",
            _ => value.ToString()
        };
        writer.WriteStringValue(stringValue);
    }
}