using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AKUTRescue.WebAPI.Controllers;

public class BaseController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
        ?? throw new InvalidOperationException("IMediator service not found.");

    protected string? GetIpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
    }

    protected string GetUserAgent()
    {
        return Request.Headers.UserAgent.ToString();
    }

    protected Guid? GetUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            return userId;
        return null;
    }

    protected ActionResult Success(string message = "İşlem başarılı.", object? data = null)
    {
        return Ok(new
        {
            success = true,
            message,
            data
        });
    }

    protected ActionResult Created(string message = "Kayıt başarıyla oluşturuldu.", object? data = null)
    {
        return StatusCode(201, new
        {
            success = true,
            message,
            data
        });
    }

    protected ActionResult NoContent(string message = "İşlem başarılı.")
    {
        return StatusCode(204, new
        {
            success = true,
            message
        });
    }

    protected ActionResult BadRequest(string message = "Geçersiz istek.", object? errors = null)
    {
        return StatusCode(400, new
        {
            success = false,
            message,
            errors
        });
    }

    protected ActionResult NotFound(string message = "Kayıt bulunamadı.")
    {
        return StatusCode(404, new
        {
            success = false,
            message
        });
    }
} 