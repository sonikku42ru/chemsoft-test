using System;

namespace ChemsoftTest.Core.Models;

public static class PersonValidation
{
    private static readonly DateTime MinBirthday = new(1, 1, 1);
    
    public static bool DateIsValid(DateTime date) =>
        date <= DateTime.UtcNow && date >= MinBirthday;

    public static bool StringIsValid(string str)
    {
        if (str == null)
            return true;
        return str.Length < 128;
    }
    
    public static bool NameIsValid(string name) => 
        !string.IsNullOrEmpty(name) && 
        name.Length < 128;
}