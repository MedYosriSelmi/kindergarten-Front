using kindergarten_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace kindergarten_Front.Controllers
{
    public class PublicationController : Controller
    {
        // GET: Publication
        public ActionResult Index()
        {
            IEnumerable<Publication> pub = null;
            using (var x = new HttpClient())
            {
                x.BaseAddress = new Uri("http://localhost:8081");
                var responseTask = x.GetAsync("/SpringMVC/servlet/publication/getAllPublications");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Publication>>();
                    readJob.Wait();
                    pub = readJob.Result;
                }
                else
                {
                    //return the error
                    pub = Enumerable.Empty<Publication>();
                    ModelState.AddModelError(String.Empty, "error");
                }

            }
            return View(pub);
        }

       
        // GET: Publication/addPub
        public ActionResult addPub()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addPub(Publication pub)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                var postJob = client.PostAsJsonAsync<Publication>("/SpringMVC/servlet/publication/addPub/10/4", pub);
                postJob.Wait();
                // return View();
                var postResult = postJob.Result;
                DateTime dateCreation = DateTime.Now;

                if (postResult.IsSuccessStatusCode)

                    return RedirectToAction("getPubFront");
            }
            //ModelState.AddModelError(string.Empty, "Server occured errors. Please check with admin!");
            return View(pub);
        }


        // GET: Publication/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }


        // GET: Publication/DeletePub/5
        public ActionResult DeletePub(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");

                //HTTP POST
                var deleteTask = client.DeleteAsync("/SpringMVC/servlet/publication/deletePublication/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");

                }
                return RedirectToAction("Index");

            }
        }
        [HttpPost]
        public ActionResult updatePub(int id, string description)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");

                //HTTP GET
                var putTask = client.PutAsJsonAsync<Publication>("/SpringMVC/servlet/publication/updatePub/" + id.ToString() + "/" + description.ToString(), null);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");

                }
                return View("updatePub");
            }
        }
        public ActionResult updatePub(int id)
        {
            Publication rec = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");

                //HTTP GET

                var responseTask = client.GetAsync("/SpringMVC/servlet/publication/getPubById/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Publication>();
                    readTask.Wait();

                    rec = readTask.Result;
                }
            }

            return View(rec);
        }
        public ActionResult getPubFront()
        {
            IEnumerable<Publication> pub = null;
            using (var x = new HttpClient())
            {
                x.BaseAddress = new Uri("http://localhost:8081");
                var responseTask = x.GetAsync("/SpringMVC/servlet/publication/getAllPublications");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Publication>>();
                    readJob.Wait();
                    pub = readJob.Result;
                }
                else
                {
                    //return the error
                    pub = Enumerable.Empty<Publication>();
                    ModelState.AddModelError(String.Empty, "error");
                }

            }
            return View(pub);
        }




    }
}
