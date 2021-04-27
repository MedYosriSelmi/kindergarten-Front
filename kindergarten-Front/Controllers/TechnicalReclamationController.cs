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
    public class TechnicalReclamationController : Controller
    {
        // GET: TechnicalReclamation
        public ActionResult Index()
        {
            IEnumerable<Reclamation> reclamation = null;
            using (var reclam = new HttpClient())
            {
                reclam.BaseAddress = new Uri("http://localhost:8081/");
                var responseTask = reclam.GetAsync("/SpringMVC/servlet/getAllTechnicalReclamations");
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

        // GET: TechnicalReclamation/Details/5
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

        // GET: TechnicalReclamation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TechnicalReclamation/Create
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

        public ActionResult Notify(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");
                var postJob = client.PostAsJsonAsync<MailMessage>("/SpringMVC/servlet/sendEmail/1/" + id.ToString(),null);
                postJob.Wait();
                var postResult = postJob.Result;
                DateTime dateCreation = DateTime.Now;
                if (postResult.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            return View("Index");
        }

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

        // GET: TechnicalReclamation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Status status)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Reclamation>("/SpringMVC/servlet/updateStatusReclamation/" + id.ToString() + "/" + status.ToString(),null);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("index");

                }
                return View("index");
            }
        }

        public ActionResult Search(string Key)
        {
            IEnumerable<Reclamation> reclamation = null;
            using (var reclam = new HttpClient())
            {
                reclam.BaseAddress = new Uri("http://localhost:8081/");
                var responseTask = reclam.GetAsync("/SpringMVC/servlet/getallReclamationsCombined/" + Key.ToString());
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

