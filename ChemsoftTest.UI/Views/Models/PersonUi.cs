using System;
using ChemsoftTest.Core.Interfaces;
using ChemsoftTest.Core.Models;
using ChemsoftTest.UI.Views.Base;

namespace ChemsoftTest.UI.Views.Models;

public class PersonUi : BaseUiModel, IModelConvertible<Person>
{
    public UiField<string> FirstName { get; } = new(string.Empty, PersonValidation.StringIsValid);
    
    public UiField<string> LastName { get;} = new(string.Empty, PersonValidation.StringIsValid);
    
    public UiField<string> PatronymicName { get; } = new(string.Empty, PersonValidation.StringIsValid);
    
    public UiField<string> Email { get; } = new(string.Empty, PersonValidation.StringIsValid);
    
    public UiField<DateTime> Birthday { get; } = new(DateTime.UtcNow, PersonValidation.DateIsValid);

    public Person ToModel() => new()
    {
        FirstName = FirstName.ToField(),
        LastName = LastName.ToField(),
        PatronymicName = PatronymicName.ToField(),
        Email = Email.ToField(),
        Birthday = Birthday.ToField()
    };
}