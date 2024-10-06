using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonalBlog.Core.Exception;

namespace PersonalBlog.Core.PipelineBehavior;

public class ErrorHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ErrorHandlingBehavior<TRequest, TResponse>> _logger;

    public ErrorHandlingBehavior(ILogger<ErrorHandlingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Not found error occurred");
            throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access error occurred");
            throw new RpcException(new Status(StatusCode.PermissionDenied, ex.Message));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred");
            throw new RpcException(new Status(StatusCode.Internal, "An internal error occurred"));
        }
    }
}