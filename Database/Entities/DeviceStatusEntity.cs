using System.ComponentModel.DataAnnotations;

namespace Database.Entities;

public class DeviceStatusEntity
{
    [Key]
    public string ModuleCategoryID { get; set; }
    public int IndexWithinRole { get; set; }
    
    public InstrumentStatusEntity InstrumentStatus { get; set; }
    public RapidControlStatusEntity RapidControlStatus { get; set; }
}