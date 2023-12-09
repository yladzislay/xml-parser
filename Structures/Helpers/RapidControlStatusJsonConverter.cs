using System.Text.Json;
using System.Text.Json.Serialization;
using Structures.CombinedStatuses;

namespace Structures.Helpers;

public class RapidControlStatusJsonConverter : JsonConverter<RapidControlStatus>
{
    public override RapidControlStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        var root = jsonDocument.RootElement;

        var rapidControlStatus = new RapidControlStatus();

        if (root.TryGetProperty("CombinedSamplerStatus", out var samplerStatus))
        {
            rapidControlStatus.CombinedStatus = JsonSerializer.Deserialize<CombinedSamplerStatus>(samplerStatus.GetRawText(), options);
        }
        else if (root.TryGetProperty("CombinedPumpStatus", out var pumpStatus))
        {
            rapidControlStatus.CombinedStatus = JsonSerializer.Deserialize<CombinedPumpStatus>(pumpStatus.GetRawText(), options);
        }
        else if (root.TryGetProperty("CombinedOvenStatus", out var ovenStatus))
        {
            rapidControlStatus.CombinedStatus = JsonSerializer.Deserialize<CombinedOvenStatus>(ovenStatus.GetRawText(), options);
        }

        return rapidControlStatus;
    }

    public override void Write(Utf8JsonWriter writer, RapidControlStatus value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        switch (value.CombinedStatus)
        {
            case CombinedSamplerStatus samplerStatus:
                writer.WritePropertyName("CombinedSamplerStatus");
                JsonSerializer.Serialize(writer, samplerStatus, options);
                break;
            case CombinedPumpStatus pumpStatus:
                writer.WritePropertyName("CombinedPumpStatus");
                JsonSerializer.Serialize(writer, pumpStatus, options);
                break;
            case CombinedOvenStatus ovenStatus:
                writer.WritePropertyName("CombinedOvenStatus");
                JsonSerializer.Serialize(writer, ovenStatus, options);
                break;
        }

        writer.WriteEndObject();
    }
}