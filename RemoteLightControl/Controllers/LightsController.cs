using Microsoft.AspNet.SignalR;
using RemoteLightControl.Hubs;
using RemoteLightControl.Models;
using RemoteLightControl.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RemoteLightControl.Controllers
{
    public class LightsController : Controller
    {
        List<LightModel> lights;

        public LightsController()
        {
            lights = new List<LightModel>();

            lights.Add(new LightModel() { Id = 1, Description = "Light #1", Value = 0, DeviceId = "simulated-device" });
            }

        // GET: Lights
        public ActionResult Index()
        {
            return View(lights);
        }

        // GET: Lights/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Lights/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> TurnOff(int id)
        {
            await UpdateLightValue(id, "0.0");

            return Json(new { value = 0.0 });
        }

        [HttpPost]
        public async Task<ActionResult> TurnOn(int id)
        {
            await UpdateLightValue(id, "1.0");

            return Json(new { value = 1.0 });
        }

        private async Task UpdateLightValue(int id, String value)
        {
            LightModel light = lights.First(l => l.Id == id);

            CloudToDeviceManager deviceManager = new CloudToDeviceManager();

            await deviceManager.SendCloudToDeviceMessageAsync(light.DeviceId, value);
        }

        [HttpPost]
        public ActionResult NotifyLightChange(int id, Single value)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();
            context.Clients.All.notifyLightChange(id, value);
            return Json(new { success = true });
        }

        // POST: Lights/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lights/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lights/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lights/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Lights/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
