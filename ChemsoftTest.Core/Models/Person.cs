using System;
using ChemsoftTest.Core.Entities;
using ChemsoftTest.Core.Interfaces;
using ChemsoftTest.Core.Utils;

namespace ChemsoftTest.Core.Models;

public class Person : IEntityConvertible<PersonEntity>
{
    public int Id { get; init; }

    public Field<string> FirstName { get; init; } = new(string.Empty, PersonValidation.NameIsValid);
    public Field<string> LastName { get; init; } = new(string.Empty, PersonValidation.NameIsValid);
    public Field<string> PatronymicName { get; init; } = new(string.Empty, PersonValidation.NameIsValid);
    public Field<string> Email { get; init; } = new(string.Empty, PersonValidation.NameIsValid);
    public Field<DateTime> Birthday { get; init; } = new(DateTime.UtcNow, PersonValidation.DateIsValid);

    public PersonEntity ToEntity() =>
        new()
        {
            FirstName = FirstName.Value,
            LastName = LastName.Value,
            PatronymicName = PatronymicName.Value,
            Email = Email.Value
        };
}