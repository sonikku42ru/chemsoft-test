using System;
using System.ComponentModel.DataAnnotations;

namespace ChemsoftTest.Core.Entities;

public record PersonEntity : BaseEntity
{
    [Required] 
    public string FirstName { get; init; } = string.Empty;
    
    [Required]
    public string LastName { get; init; } = string.Empty;

    public string PatronymicName { get; init; } = string.Empty;
    
    [Required]
    public string Email { get; init; } = string.Empty;
    
    [Required]
    public DateTime Birthday { get; init; } = DateTime.UtcNow;
}