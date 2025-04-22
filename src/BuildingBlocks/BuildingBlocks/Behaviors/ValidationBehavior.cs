using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        var context = new ValidationContext<TRequest>(request);
        var validatorsTasks = validators.Select(x => x.ValidateAsync(context, cancellationToken))
            .ToList();
        var validationResults = await Task.WhenAll(validatorsTasks);
        var failures = validationResults
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToList();
        if (failures.Any())
            throw new ValidationException(failures);
        return await next(cancellationToken);
    }
}
