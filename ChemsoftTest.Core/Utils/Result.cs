namespace ChemsoftTest.Core.Utils;

public record Result<TValue, TStatus>(TValue Value, TStatus Status);