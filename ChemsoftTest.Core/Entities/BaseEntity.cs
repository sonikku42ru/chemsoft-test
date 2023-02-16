using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemsoftTest.Core.Entities;

public record BaseEntity
{
    [Key]
    [DatabaseGenerated((DatabaseGeneratedOption.Identity))]
    public int Id { get; init; }

    [NotMapped] 
    public bool IsNew => Id == 0;
}