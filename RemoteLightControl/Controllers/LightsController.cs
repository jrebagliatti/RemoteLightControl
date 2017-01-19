﻿using RemoteLightControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            lights.Add(new LightModel() { Id = 1, Description = "Light #1", Value = 0 });
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
        public ActionResult TurnOn(int id)
        {

            return Json(new { value = 1.0 });
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