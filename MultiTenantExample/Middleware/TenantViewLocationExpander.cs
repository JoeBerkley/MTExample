using Microsoft.AspNetCore.Mvc.Razor;
using MultiTenantExample.Models.Domain;

namespace MultiTenantExample.Middleware
{
    public class TenantViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // Get tenant from HttpContext and store it in context.Values
            var tenant = context.ActionContext.HttpContext.Items["Tenant"] as Tenant;

            context.Values["tenant"] = tenant?.Name.ToLower() ?? "0"; // Use an ID or a name
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            // Insert custom location formats
            var tenant = context.Values["tenant"];

            var tenantViewLocations = new[]
            {
            // Look in Views/Tenants/{tenant}/[Controller]/[View].cshtml
            $"/Views/Tenants/{tenant}/{{1}}/{{0}}.cshtml",

            // Look in Views/Tenants/{tenant}/Shared/[View].cshtml
            $"/Views/Tenants/{tenant}/Shared/{{0}}.cshtml"
        };

            // Add default view locations as fallback
            return tenantViewLocations.Concat(viewLocations);
        }
    }
}