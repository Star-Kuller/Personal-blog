using System.Text;
using FluentValidation;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonalBlog.Core.Exception;

namespace PersonalBlog.Core.PipelineBehavior;

public class ErrorHandlingBehavior<TRequest, TResponse>(ILogger<ErrorHandlingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (NotFoundException ex)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new RpcException(new Status(StatusCode.PermissionDenied, ex.Message));
        }
        catch (AlreadyExistException ex)
        {
            throw new RpcException(new Status(StatusCode.AlreadyExists, ex.Message));
        }
        catch (PermissionDeniedException ex)
        {
            throw new RpcException(new Status(StatusCode.PermissionDenied, ex.Message));
        }
        catch (ValidationException ex)
        {
            var message = new StringBuilder();
            foreach (var error in ex.Errors)
            {
                message.AppendLine($"{error.PropertyName}: {error.ErrorMessage}");
            }
            message.Remove(message.Length - 2, 2);
            
            throw new RpcException(new Status(StatusCode.InvalidArgument, message.ToString()));
        }
        catch (System.Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred");
            throw new RpcException(new Status(StatusCode.Internal, "An internal error occurred"));
        }
    }
}