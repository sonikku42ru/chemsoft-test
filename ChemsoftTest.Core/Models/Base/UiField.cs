using System;
using ChemsoftTest.Core.Utils;

namespace ChemsoftTest.Core.Models.Base;

public class UiField : BaseUiModel
{
    private bool _valid;

    public bool Valid
    {
        get => _valid;
        protected set
        {
            _valid = value;
            OnPropertyChanged();
        }
    }
}

public class UiField<T> : UiField
{
    private Func<T, bool> _isValidated = _ => true;
    private T _value;
    
    public UiField(T def = default, Func<T, bool> validationFunc = default)
    {
        Value = def;
        if (validationFunc != default)
            _isValidated = validationFunc;
        Valid = _isValidated(Value);
    }

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            if (ValidateFirst)
            {
                Valid = Validated(value);
                OnPropertyChanged();
            }
            else
            {
                OnPropertyChanged();
                Valid = Validated(value);
            }
        }
    }

    public Func<T, bool> Validated
    {
        get => _isValidated;
        set
        {
            _isValidated = value ?? (_ => true);
            Valid = _isValidated(Value);
            OnPropertyChanged();
        }
    }

    public bool ValidateFirst { get; set; } = true;

    public Utils.Field<T> ToField() => new()
    {
        Value = Value,
        Valid = Validated
    };
}