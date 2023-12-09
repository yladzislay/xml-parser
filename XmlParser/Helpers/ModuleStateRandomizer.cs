using Structures;

namespace XmlParser.Helpers;

public static class ModuleStateRandomizer
{
    private static string GetRandomModuleState()
    {
        var random = new Random();
        var moduleStates = Enum.GetNames(typeof(ModuleStatesEnum));
        var randomIndex = random.Next(moduleStates.Length);
        return moduleStates[randomIndex];
    }
    
    public static InstrumentStatus RandomizeModuleState(this InstrumentStatus instrumentStatus)
    {
        foreach (var deviceStatus in instrumentStatus.DeviceStatuses)
                deviceStatus.RapidControlStatus.CombinedStatus.ModuleState = GetRandomModuleState();

        return instrumentStatus;
    }
}