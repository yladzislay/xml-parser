using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class RapidControlStatusEntity
    {
        [Key]
        public int Id { get; set; }
        public string ModuleState { get; set; }
        public bool IsBusy { get; set; }
        public bool IsReady { get; set; }
        public bool IsError { get; set; }
        public bool KeyLock { get; set; }

        [ForeignKey("CombinedStatusId")]
        public CombinedStatusEntity CombinedStatus { get; set; }
    }
}