using System;
using System.ComponentModel.DataAnnotations;
using ChemsoftTest.Core.Interfaces;
using ChemsoftTest.Core.Models;
using ChemsoftTest.Core.Utils;

namespace ChemsoftTest.Core.Entities;

public record PersonEntity : BaseEntity, IModelConvertible<Person>
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
    
    public Person ToModel() =>
        new()
        {
            Id = Id,
            FirstName = new Field<string>(FirstName),
            LastName = new Field<string>(LastName),
            PatronymicName = new Field<string>(PatronymicName),
            Email = new Field<string>(Email),
            Birthday = new Field<DateTime>(Birthday)
        };
}