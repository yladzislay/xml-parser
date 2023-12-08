namespace Database.Entities;

public class CombinedOvenStatusEntity : CombinedStatusEntity
{
    public bool? UseTemperatureControl { get; set; }
    public bool? OvenOn { get; set; }
    public double? Temperature_Actual { get; set; }
    public double? Temperature_Room { get; set; }
    public int? MaximumTemperatureLimit { get; set; }
    public int?Valve_Position { get; set; }
    public int? Valve_Rotations { get; set; }
    public bool? Buzzer { get; set; }
}