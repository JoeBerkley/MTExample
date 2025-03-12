using MultiTenantExample.Models.Domain;

namespace MultiTenantExample.Services
{
    public interface ITenantStore
    {
        Tenant GetTenantByHost(string hostname);
    }

    public class InMemoryTenantStore : ITenantStore
    {
        public Tenant GetTenantByHost(string hostname)
        {
            return _tenants.FirstOrDefault(t =>
                t.Hostname.Equals(hostname, StringComparison.OrdinalIgnoreCase));
        }

        private readonly List<Tenant> _tenants = new()
        {
            new Tenant
            {
                Id = 1,
                Name = "Green",
                Hostname = "green.huola.space",
                ThemeCssFile = "green.css",
                ConnectionString = "Server=.\\SQLEXPRESS;Database=Green;Integrated Security=False;User Id=Green;Password=green.password;TrustServerCertificate=True"
            },
            new Tenant
            {
                Id = 2,
                Name = "Blue",
                Hostname = "blue.huola.space",
                ThemeCssFile = "blue.css",
                ConnectionString = "Server=.\\SQLExpress;Database=Blue;Integrated Security=False;User Id=blue;Password=blue.password;TrustServerCertificate=True"
            },

            new Tenant
            {
                Id = 3,
                Name = "red",
                Hostname = "red.huola.space",
                ThemeCssFile = "red.css",
                ConnectionString = "Server=.\\SQLExpress;Database=Blue;Integrated Security=False;User Id=blue;Password=blue.password;TrustServerCertificate=True"
            }
        };
    }
}