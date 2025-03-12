using MultiTenantExample.Services;

namespace MultiTenantExample.Middleware
{
    public class TenantResolutionMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantResolutionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantStore tenantStore)
        {
            var host = context.Request.Host.Host;

            var tenant = tenantStore.GetTenantByHost(host);

            if (tenant == null)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("Tenant not found");
                return;
            }

            // Store tenant info in HttpContext.Items
            context.Items["Tenant"] = tenant;

            await _next(context);
        }
    }
}