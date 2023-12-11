using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

public class RapidControlStatusEntity
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("DeviceStatusId")]
    public DeviceStatusEntity DeviceStatus { get; set; }
    public CombinedStatusEntity CombinedStatus { get; set; }
}