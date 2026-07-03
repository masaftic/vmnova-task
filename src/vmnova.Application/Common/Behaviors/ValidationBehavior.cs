using FluentValidation;
using MediatR;
using vmnova.Domain.Shared;

namespace vmnova.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : IAppResult
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next(cancellationToken);
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next(cancellationToken);
        }

        var errors = validationResult.Errors
            .Select(error => AppError.Validation(
                $"Validation.{error.PropertyName}",
                error.ErrorMessage,
                new Dictionary<string, object> { ["PropertyName"] = error.PropertyName }))
            .ToList();

        return (dynamic)errors;
    }
}
