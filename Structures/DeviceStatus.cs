using System.Text.Json.Serialization;
using Structures.Helpers;

namespace Structures;

public class DeviceStatus
{
    public string ModuleCategoryID { get; set; }
    public int IndexWithinRole { get; set; }
        
    [JsonConverter(typeof(RapidControlStatusJsonConverter))]
    public RapidControlStatus RapidControlStatus { get; set; }
}