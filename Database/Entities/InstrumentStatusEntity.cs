using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class InstrumentStatusEntity
    {
        [Key]
        public int Id { get; set; }
        public string PackageID { get; set; }

        public List<DeviceStatusEntity> DeviceStatusList { get; set; }
    }
}