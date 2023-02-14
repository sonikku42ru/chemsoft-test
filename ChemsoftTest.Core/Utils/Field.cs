using System;
using System.Collections.Generic;

namespace ChemsoftTest.Core.Utils;

public struct Field<T>
{
    private Func<T, bool> _isValid;
    
    public Field(T def = default, Func<T, bool> isValid = default)
    {
        Value = def;
        _isValid = isValid ?? (_ => true);
    }
    
    public T Value { get; set; }

    public Func<T, bool> Valid
    {
        get => _isValid;
        set => _isValid = value ?? (_ => true);
    }
    
    public static bool operator ==(Field<T> a, Field<T> b) => 
        a.Value.Equals(b.Value);

    public static bool operator !=(Field<T> a, Field<T> b) => 
        !a.Value.Equals(b.Value);

    private bool Equals(Field<T> other) => 
        EqualityComparer<T>.Default.Equals(Value, other.Value);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Field<T>)obj);
    }
    
    public override int GetHashCode() => Value.GetHashCode();
}