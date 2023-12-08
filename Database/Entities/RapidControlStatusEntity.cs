using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

public class RapidControlStatusEntity
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("CombinedStatusId")]
    public CombinedStatusEntity CombinedStatus { get; set; }
}