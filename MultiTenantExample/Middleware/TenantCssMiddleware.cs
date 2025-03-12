using MultiTenantExample.Models.Domain;

namespace MultiTenantExample.Middleware
{
    public class TenantCssMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public TenantCssMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Only intercept specific CSS requests (e.g., /css/site.css)
            var path = context.Request.Path;

            if (path.StartsWithSegments("/css/site.css"))
            {
                var tenant = context.Items["Tenant"] as Tenant;

                if (tenant != null)
                {
                    // Resolve the physical file path
                    var cssFilePath = Path.Combine(_env.WebRootPath, "css", tenant.ThemeCssFile);

                    if (File.Exists(cssFilePath))
                    {
                        context.Response.ContentType = "text/css";
                        await context.Response.SendFileAsync(cssFilePath);
                        return; // Stop processing the pipeline
                    }
                }

                // Tenant or file not found → Optionally fall back to default CSS
                var defaultCssPath = Path.Combine(_env.WebRootPath, "css", "default.css");

                if (File.Exists(defaultCssPath))
                {
                    context.Response.ContentType = "text/css";
                    await context.Response.SendFileAsync(defaultCssPath);
                    return;
                }

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("CSS file not found.");
                return;
            }

            // Continue the pipeline for other requests
            await _next(context);
        }
    }
}