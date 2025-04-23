using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;
public class LogingBehavior<TRequest, TResponse>
    (ILogger<LogingBehavior<TRequest, TResponse>> _logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>

{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[START] Handle Request = {CommandName} with data: {@Command} \r\n & Response ={Response}",
                               request.GetType().Name,
                               request,
                               typeof(TResponse).Name);
        Stopwatch sw = new();
        sw.Start();
        var result = await next(cancellationToken);
        sw.Stop();
        int elapsedTime = (int)sw.ElapsedMilliseconds / 1000;
        if (elapsedTime > 3)
            _logger.LogWarning("Command {CommandName} took {ElapsedTime} seconds to execute",
                               request.GetType().Name,
                               elapsedTime);
        else
            _logger.LogInformation("Command {CommandName} executed in {ElapsedTime} seconds",
                                request.GetType().Name,
                                elapsedTime);
        return result;
    }
}