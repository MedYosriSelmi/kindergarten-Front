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

        public ActionResult addPlan()
        {
            return View();
        }
        [HttpPost]
        // GET: Planning/Create
        public ActionResult addPlan(Planning plan)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                var postJob = client.PostAsJsonAsync<Planning>("/SpringMVC/servlet/planning/addPlan/10/7", plan);
                postJob.Wait();
                // return View();
                var postResult = postJob.Result;
                DateTime dateCreation = DateTime.Now;

                if (postResult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
            }
            //ModelState.AddModelError(string.Empty, "Server occured errors. Please check with admin!");
            return View(plan);
        }

        [HttpPost]
        public ActionResult updatePlan(int id, string departure, string destination)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");

                //HTTP GET
                var putTask = client.PutAsJsonAsync<Planning>("/SpringMVC/servlet/planning/updatePlan/" + id.ToString() + "/" + departure.ToString() + "/" + destination.ToString(), null);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");

                }
                return View("updatePlan");
            }
        }
        public ActionResult updatePlan(int id)
        {
            Planning rec = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");

                //HTTP GET

                var responseTask = client.GetAsync("/SpringMVC/servlet/planning/getPlanById/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Planning>();
                    readTask.Wait();

                    rec = readTask.Result;
                }
            }

            return View(rec);
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
