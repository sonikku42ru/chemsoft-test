using System;
using System.Collections.Generic;
using ChemsoftTest.Core.Entities;
using ChemsoftTest.Core.Models.Base;
using ChemsoftTest.Core.Utils;

namespace ChemsoftTest.Core.Models;

public class PersonUi : BaseUiModel
{
    public int? Id { get; init; }
    
    public UiField<string> FirstName { get; init; } = new(string.Empty, PersonValidation.NameIsValid);
    
    public UiField<string> LastName { get; init; } = new(string.Empty, PersonValidation.NameIsValid);
    
    public UiField<string> PatronymicName { get; init; } = new(string.Empty, PersonValidation.StringIsValid);
    
    public UiField<string> Email { get; init; } = new(string.Empty, PersonValidation.StringIsValid);
    
    public UiField<DateTime> Birthday { get; init; } = new(DateTime.UtcNow, PersonValidation.DateIsValid);
}

public static class PersonExtensions
{
    public static PersonEntity ToEntity(this PersonUi p) => new()
    {
        Id = p.Id ?? 0,
        FirstName = p.FirstName.Value,
        LastName = p.LastName.Value,
        PatronymicName = p.PatronymicName.Value,
        Email = p.Email.Value,
        Birthday = p.Birthday.Value
    };

    public static IEnumerable<PersonEntity> ToEntities(
        this IEnumerable<PersonUi> models)
    {
        var list = new List<PersonEntity>();
        foreach (var model in models)
            list.Add(model.ToEntity());
        return list;
    }

    public static PersonUi ToUiModel(this PersonEntity e) => new()
    {
        Id = e.Id,
        FirstName = new UiField<string>(e.FirstName),
        LastName = new UiField<string>(e.LastName),
        PatronymicName = new UiField<string>(e.PatronymicName),
        Email = new UiField<string>(e.Email),
        Birthday = new UiField<DateTime>(e.Birthday)
    };

    public static IEnumerable<PersonUi> ToUiModels(this IEnumerable<PersonEntity> entities)
    {
        var list = new List<PersonUi>();
        foreach (var entity in entities)
            list.Add(entity.ToUiModel());
        return list;
    }

    public static ObservableRangeCollection<PersonUi> ToCollection(this IEnumerable<PersonEntity> entities)
    {
        var collection = new ObservableRangeCollection<PersonUi>();
        foreach (var entity in entities)
            collection.Add(entity.ToUiModel());
        return collection;
    }
}