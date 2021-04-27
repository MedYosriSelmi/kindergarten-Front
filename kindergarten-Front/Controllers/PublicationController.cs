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

        // GET: Publication/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Publication/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publication/Create
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

        // GET: Publication/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Publication/Edit/5
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

        // POST: Publication/Delete/5
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
