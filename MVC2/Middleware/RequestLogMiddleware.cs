using Microsoft.AspNetCore.Http.Extensions;
using MVC.Data;
using MVC.Models;
using System.Globalization;

namespace MVC.Middleware;
public class RequestLogMiddleware {
    private readonly RequestDelegate _next;
    //private ApplicationDbContext _context;
    //private HttpContext _httpContext;

    public RequestLogMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ApplicationDbContext applicationDbContext) {
        var ipAddress = context.Connection.RemoteIpAddress.ToString();
        var url = context.Request.GetDisplayUrl();
        var log = new Request {
            IPAddress = ipAddress,
            RequestDateTime = DateTime.Now,
            RequestUrl = url
        };
        applicationDbContext.RequestLog.Add(log);
        applicationDbContext.SaveChangesAsync();
        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}
public static class RequestLogMiddlewareExtensions {
    public static IApplicationBuilder UseRequestLog(
        this IApplicationBuilder builder) {
        return builder.UseMiddleware<RequestLogMiddleware>();
    }
}
