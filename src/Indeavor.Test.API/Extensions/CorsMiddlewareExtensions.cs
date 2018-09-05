using Indeavor.Test.API.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Indeavor.Test.API.Extensions
{
    public static class CorsMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorsMiddleware>();
        }
    }
}
