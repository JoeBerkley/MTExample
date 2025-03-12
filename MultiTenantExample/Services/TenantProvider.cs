using MultiTenantExample.Models.Domain;

namespace MultiTenantExample.Services
{
    public class HttpContextTenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextTenantProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Tenant GetTenant()
        {
            return _contextAccessor.HttpContext?.Items["Tenant"] as Tenant;
        }
    }

    public interface ITenantProvider
    {
        Tenant GetTenant();
    }
}