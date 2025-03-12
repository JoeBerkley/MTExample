namespace MultiTenantExample.Models.Domain
{
    public class Device
    {
        public long? Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Device()
        {
        }
    }
}