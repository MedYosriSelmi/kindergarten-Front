using kindergarten_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace kindergarten_Front.Controllers
{
    public class PlanningController : Controller
    {
        // GET: Planning
        public ActionResult Index()
        {
            IEnumerable<Planning> plan = null;
            using (var x = new HttpClient())
            {
                x.BaseAddress = new Uri("http://localhost:8081");
                var responseTask = x.GetAsync("/SpringMVC/servlet/planning/getAllPlannings");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Planning>>();
                    readJob.Wait();
                    plan = readJob.Result;
                }
                else
                {
                    //return the error
                    plan = Enumerable.Empty<Planning>();
                    ModelState.AddModelError(String.Empty, "error");
                }

            }
            return View(plan);
    }

        // GET: Planning/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Planning/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Planning/Create
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

        // GET: Planning/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Planning/Edit/5
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

        // GET: Planning/DeletePlanning/5
        public ActionResult DeletePlanning(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");

                //HTTP POST
                var deleteTask = client.DeleteAsync("/SpringMVC/servlet/planning/deletePlanning/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");

                }
                return RedirectToAction("Index");

            }
        }

        // POST: Planning/Delete/5
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
