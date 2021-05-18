using kindergarten_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
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

        public ActionResult CheckStatus(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");
                var postJob = client.PostAsJsonAsync<MailMessage>("/SpringMVC/servlet/checkReclamationStatus/2/" + id.ToString(), null);
                postJob.Wait();
                var postResult = postJob.Result;
                DateTime dateCreation = DateTime.Now;
                if (postResult.IsSuccessStatusCode)
                    return RedirectToAction("GetReclamationsById");
            }
            return View("GetReclamationsById");
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
                client.BaseAddress = new Uri("http://localhost:8081/");
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
                client.BaseAddress = new Uri("http://localhost:8081/");
                var postJob = client.PostAsJsonAsync<Reclamation>("/SpringMVC/servlet/addSocReclamation/2/1/", rec);
                postJob.Wait();
                var postResult = postJob.Result;
                DateTime dateCreation = DateTime.Now;
                if (postResult.IsSuccessStatusCode)
                    return RedirectToAction("GetReclamationsById");
            }
            return View(rec);
        }

        [HttpPost]
        public ActionResult Edit(int id, string description)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");

                //HTTP GET
                var putTask = client.PutAsJsonAsync<Reclamation>("/SpringMVC/servlet/updateUserReclamation/" + id.ToString() + "/" + description.ToString(), null);
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

        public ActionResult findByDate(string date)
        {
            IEnumerable<Reclamation> reclamation = null;
            using (var reclam = new HttpClient())
            {
                reclam.BaseAddress = new Uri("http://localhost:8081/");
                var responseTask = reclam.GetAsync("/SpringMVC/servlet/searchReclamationByDate/" + date.ToString());
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
    }

}

