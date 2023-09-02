namespace bookify.application.Exceptions;
public sealed record ValidationError(string PropertyName, string ErrorMessage);
