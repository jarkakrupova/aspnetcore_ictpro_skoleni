using Microsoft.AspNetCore.Http.Extensions;
using MVC.Data;
using MVC.Models;
using System.Globalization;

namespace MVC.Middleware;
public class RequestLogMiddleware {
    private readonly RequestDelegate _next;

    //konstruktor
    //tohle zustava, ukradeno z ASP.NET dokumentace
    public RequestLogMiddleware(RequestDelegate next) {
        _next = next;
    }
    //tahle metoda je povinna, injektuje se rovnou do ni, ne pres konstruktor
    public async Task InvokeAsync(HttpContext context, ApplicationDbContext applicationDbContext) {
        //odsud zacina muj kod
        var ipAddress = context.Connection.RemoteIpAddress.ToString();
        var url = context.Request.GetDisplayUrl();
        var log = new Request {
            IPAddress = ipAddress,
            RequestDateTime = DateTime.Now,
            RequestUrl = url
        };
        applicationDbContext.RequestLog.Add(log);
        applicationDbContext.SaveChangesAsync();
        //tady konci muj kod, dal pokracuje kod z dokumentace
        // Call the next delegate/middleware in the pipeline.
        await _next(context);  //posun na dalsi middleware v pipeline (to, co je volano pod tim)
    }
}
public static class RequestLogMiddlewareExtensions {
    public static IApplicationBuilder UseRequestLog(
        this IApplicationBuilder builder) {
        return builder.UseMiddleware<RequestLogMiddleware>();
    }
}

