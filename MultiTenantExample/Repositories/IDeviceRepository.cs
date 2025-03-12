using Dapper;
using Microsoft.Data.SqlClient;
using MultiTenantExample.Exceptions;
using MultiTenantExample.Models;
using MultiTenantExample.Models.Domain;
using MultiTenantExample.Services;

namespace MultiTenantExample.Repositories
{
    public interface IDeviceRepository
    {
        bool AddDevice(AddEditDeviceViewModel model, out Device? device);

        Device? GetDevice(long DeviceId);

        List<Device> GetDevices(int page, int pageSize);

        bool EditDevice(long DeviceId, AddEditDeviceViewModel model, out Device? device);

        bool DeleteDevice(long DeviceId);
    }

    public class DeviceRepository : IDeviceRepository
    {
        private readonly ITenantProvider _tenantProvider;
        private readonly Tenant _tenant;

        public DeviceRepository(ITenantProvider tenantProvider)
        {
            _tenantProvider = tenantProvider;
            _tenant = _tenantProvider.GetTenant();
            if (_tenant == null)
            {
                throw new TenantNotFoundException();
            }
        }

        public bool AddDevice(AddEditDeviceViewModel model, out Device? device)
        {
            var newId = (long)0;
            using (var connection = new SqlConnection(_tenant.ConnectionString))
            {
                connection.Open();

                var sql = @"
                    INSERT INTO Device (Make, Model, Price, Description)
                    VALUES (@Make, @DeviceModel, @Price, @Description);

                    SELECT CAST(SCOPE_IDENTITY() as BIGINT);";

                newId = connection.ExecuteScalar<long>(sql, model);

                Console.WriteLine($"Inserted Device with Id: {newId}");
            }

            device = GetDevice(newId);

            return true;
        }

        public Device? GetDevice(long DeviceId)
        {
            using (var connection = new SqlConnection(_tenant.ConnectionString))
            {
                connection.Open();
                var sql = @"
                    SELECT Id, Make, Model, Price, Description
                    FROM Device
                    WHERE Id = @DeviceId;";
                return connection.QueryFirstOrDefault<Device>(sql, new { DeviceId });
            }
        }

        public bool DeleteDevice(long DeviceId)
        {
            using (var connection = new SqlConnection(_tenant.ConnectionString))
            {
                connection.Open();
                var sql = @"
                    DELETE FROM Device
                    WHERE Id = @Id;";
                return connection.Execute(sql, new { Id = DeviceId }) > 0;
            }
        }

        public bool EditDevice(long DeviceId, AddEditDeviceViewModel model, out Device? device)
        {
            using (var connection = new SqlConnection(_tenant.ConnectionString))
            {
                connection.Open();
                var sql = @"
                    UPDATE Device
                    SET Make = @Make, Model = @Model, Price = @Price, Description = @Description
                    WHERE Id = @Id;";
                connection.Execute(sql, model);
            }
            device = GetDevice(DeviceId);
            return true;
        }

        public List<Device> GetDevices(int page, int pageSize)
        {
            using (var connection = new SqlConnection(_tenant.ConnectionString))
            {
                connection.Open();
                var sql = @"
                    SELECT Id, Make, Model, Price, Description
                    FROM Device
                    ORDER BY Id
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;";
                return connection.Query<Device>(sql, new { Offset = (page - 1) * pageSize, PageSize = pageSize }).ToList();
            }
        }
    }
}