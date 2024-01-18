using System.Net;
using System.Threading.Tasks;
using NLog;
using Microsoft.Extensions.Logging;
using PriceNegotiationApp.Exceptions;
using Microsoft.AspNetCore.Http;

namespace PriceNegotiationApp.Middleware;

public class ErrorHandlingMiddleware: IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (BadRequestException badRequestException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(badRequestException.Message);
        }
        catch (UnauthorizedException unauthorizedException)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("You are not authorized to view the requested resources.");
        }
        catch (NotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Not found.");
        }
        catch (ForbiddenException forbiddenException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Forbidden.");
        }
        catch (InternalServerErrorException internalServerErrorException)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal server error.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Invalid action.");
        }
    }

    
}