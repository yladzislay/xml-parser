using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class DeviceStatusEntity
    {
        [Key]
        public int Id { get; set; }
        public string ModuleCategoryID { get; set; }
        public int IndexWithinRole { get; set; }

        [ForeignKey("RapidControlStatusId")]
        public RapidControlStatusEntity RapidControlStatus { get; set; }
    }
}