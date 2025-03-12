using Microsoft.AspNetCore.Mvc;
using MultiTenantExample.Models;
using MultiTenantExample.Models.Domain;
using MultiTenantExample.Services;

namespace MultiTenantExample.Controllers
{
    public class DeviceController : Controller
    {
        private readonly ILogger<DeviceController> _logger;
        private readonly IDeviceService _deviceService;

        public DeviceController(ILogger<DeviceController> logger, IDeviceService deviceService)
        {
            _logger = logger;
            _deviceService = deviceService;
        }

        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var devices = _deviceService.GetDevices(page, pageSize);
            return View(devices);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddEditDeviceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _deviceService.AddDevice(model, out Device device);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult Edit(AddEditDeviceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _deviceService.EditDevice(model.Id.Value, model, out Device device);
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete([FromBody] long DeviceId)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}