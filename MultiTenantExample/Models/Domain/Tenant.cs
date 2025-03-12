namespace MultiTenantExample.Models.Domain
{
    public class Tenant
    {
        public int Id { get; set; } // Optional: GUID or DB ID
        public string Name { get; set; }
        public string Hostname { get; set; }
        public string ConnectionString { get; set; }

        public string ThemeCssFile { get; set; }
    }
}