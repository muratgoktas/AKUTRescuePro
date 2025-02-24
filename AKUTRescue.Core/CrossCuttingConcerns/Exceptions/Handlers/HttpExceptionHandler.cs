using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading.Tasks;

public class HttpExceptionHandler : ExceptionHandler
{
    private readonly HttpContext _context;
    private readonly IHostEnvironment _environment;

    public HttpExceptionHandler(HttpContext context, IHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    protected override Task HandleException(BusinessException businessException)
    {
        var details = new BusinessProblemDetails(businessException.Message);
        var response = JsonSerializer.Serialize(details);
        
        _context.Response.StatusCode = StatusCodes.Status400BadRequest;
        _context.Response.ContentType = "application/json";
        return _context.Response.WriteAsync(response);
    }

    protected override Task HandleException(ValidationException validationException)
    {
        var details = new ValidationProblemDetails(validationException.Errors);
        var response = JsonSerializer.Serialize(details);

        _context.Response.StatusCode = StatusCodes.Status400BadRequest;
        _context.Response.ContentType = "application/json";
        return _context.Response.WriteAsync(response);
    }

    protected override Task HandleException(AuthorizationException authorizationException)
    {
        var details = new AuthorizationProblemDetails(authorizationException.Message);
        var response = JsonSerializer.Serialize(details);

        _context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        _context.Response.ContentType = "application/json";
        return _context.Response.WriteAsync(response);
    }

    protected override Task HandleException(Exception exception)
    {
        var detail = _environment.IsDevelopment() 
            ? exception.ToString() 
            : "Internal Server Error";

        var details = new InternalServerErrorProblemDetails(detail);
        var response = JsonSerializer.Serialize(details);

        _context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        _context.Response.ContentType = "application/json";
        return _context.Response.WriteAsync(response);
    }
} 