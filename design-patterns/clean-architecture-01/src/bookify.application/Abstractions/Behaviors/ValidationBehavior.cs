using bookify.application.Abstractions.Messaging;
using bookify.application.Exceptions;
using FluentValidation;
using MediatR;

namespace bookify.application.Abstractions.Behaviors;
public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    /// <summary>
    /// Allow inject one or more IValidator isntance for each TRequest
    /// </summary>
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationErrors = _validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => validationResult.Errors.Any())
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .ToList();

        if(validationErrors.Any())
        {
            throw new Exceptions.ValidationException(validationErrors);
        }

        return await next();
    }
}
