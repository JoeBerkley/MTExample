using MultiTenantExample.Exceptions;
using MultiTenantExample.Models;
using MultiTenantExample.Models.Domain;
using MultiTenantExample.Repositories;

namespace MultiTenantExample.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository, ITenantProvider tenantProvider)
        {
            _deviceRepository = deviceRepository;
        }

        public bool AddDevice(AddEditDeviceViewModel model, out Device device)
        {
            return _deviceRepository.AddDevice(model, out device);
        }

        public bool DeleteDevice(long DeviceId)
        {
            return _deviceRepository.DeleteDevice(DeviceId);
        }

        public bool EditDevice(long DeviceId, AddEditDeviceViewModel model, out Device device)
        {
            return _deviceRepository.EditDevice(DeviceId, model, out device);
        }

        public Device? GetDevice(long DeviceId)
        {
            return _deviceRepository.GetDevice(DeviceId);
        }

        public List<Device> GetDevices(int page, int pageSize)
        {
            return _deviceRepository.GetDevices(page, pageSize);
        }
    }

    public interface IDeviceService
    {
        bool AddDevice(AddEditDeviceViewModel model, out Device device);

        bool EditDevice(long DeviceId, AddEditDeviceViewModel model, out Device device);

        bool DeleteDevice(long DeviceId);

        Device? GetDevice(long DeviceId);

        List<Device> GetDevices(int page, int pageSize);
    }
}