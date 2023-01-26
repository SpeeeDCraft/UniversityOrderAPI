using Microsoft.AspNetCore.Diagnostics;

namespace UniversityOrderAPI.Middleware;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)  
    {  
        app.UseMiddleware<ExceptionMiddleware>();  
    } 
}