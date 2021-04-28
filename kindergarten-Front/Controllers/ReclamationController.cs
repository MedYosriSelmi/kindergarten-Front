using kindergarten_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace kindergarten_Front.Controllers
{
    public class ReclamationController : Controller
    {
        // GET: Reclamation
        public ActionResult Index()
        {
            IEnumerable<Reclamation> reclamation = null;
            using (var reclam = new HttpClient())
            {
                reclam.BaseAddress = new Uri("http://localhost:8081");
                var responseTask = reclam.GetAsync("/SpringMVC/servlet/getAllReclamations");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Reclamation>>();
                    readJob.Wait();
                    reclamation = readJob.Result;
                }
                else
                {
                    //return the error
                    reclamation = Enumerable.Empty<Reclamation>();
                    ModelState.AddModelError(String.Empty, "error");
                }

            }
            return View(reclamation);
        }

        public ActionResult FilterReclamation(Status status, DateTime date1, DateTime date2)
        {
            IEnumerable<Reclamation> reclamation = null;
            using (var reclam = new HttpClient())
            {
                reclam.BaseAddress = new Uri("http://localhost:8081");
                var responseTask = reclam.GetAsync("/SpringMVC/servlet/filterReclamationsByDateAndStatus/"+status.ToString()+"/"+date1.ToString()+"/"+date2.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Reclamation>>();
                    readJob.Wait();
                    reclamation = readJob.Result;
                }
                else
                {
                    //return the error
                    reclamation = Enumerable.Empty<Reclamation>();
                    ModelState.AddModelError(String.Empty, "error");
                }

            }
            return View(reclamation);
        }

        public ActionResult GetReclamationsById()
        {
            IEnumerable<Reclamation> reclamation = null;
            using (var reclam = new HttpClient())
            {
                reclam.BaseAddress = new Uri("http://localhost:8081");
                var responseTask = reclam.GetAsync("/SpringMVC/servlet/getAllReclamationsByUserId/2");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Reclamation>>();
                    readJob.Wait();
                    reclamation = readJob.Result;
                }
                else
                {
                    //return the error
                    reclamation = Enumerable.Empty<Reclamation>();
                    ModelState.AddModelError(String.Empty, "error");
                }

            }
            return View(reclamation);
        }

        // GET: Reclamation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reclamation/Create
        public ActionResult CreateTechnical()
        {
            return View();
        }

        // POST: Reclamation/Create
        [HttpPost]
        public ActionResult CreateTechnical(Reclamation rec)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                var postJob = client.PostAsJsonAsync<Reclamation>("/SpringMVC/servlet/addTechReclamation/2/", rec);
                postJob.Wait();
                var postResult = postJob.Result;
                DateTime dateCreation = DateTime.Now;
                if (postResult.IsSuccessStatusCode)
                    return RedirectToAction("GetReclamationsById");
            }
            return View(rec);
        }

        // GET: Reclamation/CreateSocial
        public ActionResult CreateSocial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSocial(Reclamation rec)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                var postJob = client.PostAsJsonAsync<Reclamation>("/SpringMVC/servlet/addSocReclamation/2/2", rec);
                postJob.Wait();
                var postResult = postJob.Result;
                DateTime dateCreation = DateTime.Now;
                if (postResult.IsSuccessStatusCode)
                    return RedirectToAction("GetReclamationsById");
            }
            return View(rec);
        }

        [HttpPost]
        public ActionResult Edit(int id, string description, string photo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");

                //HTTP GET
                var putTask = client.PutAsJsonAsync<Reclamation>("/SpringMVC/servlet/updateUserReclamation/" + id.ToString() + "/" + description.ToString() + "/" + photo.ToString(), null);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetReclamationsById");

                }
                return View("GetReclamationsById");
            }
        }

        // POST: Driver/Edit/5
        public ActionResult Edit(int id)
        {
            Reclamation rec = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");

                //HTTP GET

                var responseTask = client.GetAsync("/SpringMVC/servlet/getReclamationById/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Reclamation>();
                    readTask.Wait();

                    rec = readTask.Result;
                }
            }

            return View(rec);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");

                //HTTP 
                var deleteTask = client.DeleteAsync("/SpringMVC/servlet/deleteReclamation/2/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetReclamationsById");
                }
                return RedirectToAction("GetReclamationsById");
            }

        }
    }
}
