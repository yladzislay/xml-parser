using System.ComponentModel.DataAnnotations;

namespace Database.Entities;

public class InstrumentStatusEntity
{
    [Key]
    public string PackageID { get; set; }
    
    public List<DeviceStatusEntity> DeviceStatuses { get; set; }
}