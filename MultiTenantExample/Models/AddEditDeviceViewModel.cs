namespace MultiTenantExample.Models
{
    public class AddEditDeviceViewModel
    {
        public long? Id { get; set; }
        public string Make { get; set; }
        public string DeviceModel { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}