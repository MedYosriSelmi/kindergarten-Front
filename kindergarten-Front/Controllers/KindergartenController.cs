using kindergarten_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace kindergarten_Front.Controllers
{
    public class KindergartenController : Controller
    {
        // GET: Kindergarten
        public ActionResult Index()
        {
                IEnumerable<Kindergarten> kindergarten = null;
                using (var x = new HttpClient())
                {
                    x.BaseAddress = new Uri("http://localhost:8081");
                    var responseTask = x.GetAsync("/SpringMVC/servlet/kindergarten/getAllKindergartens");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readJob = result.Content.ReadAsAsync<IList<Kindergarten>>();
                        readJob.Wait();
                        kindergarten = readJob.Result;
                    }
                    else
                    {
                        //return the error
                        kindergarten = Enumerable.Empty<Kindergarten>();
                        ModelState.AddModelError(String.Empty, "error");
                    }

                }
                return View(kindergarten);
            }
       

        // GET: Kindergarten/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Kindergarten/Create
        public ActionResult Create(Kindergarten kindergarten)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                var postJob = client.PostAsJsonAsync<Kindergarten>("/SpringMVC/servlet/kindergarten/addKindergarten", kindergarten);
                postJob.Wait();
                // return View();
                var postResult = postJob.Result;
                DateTime dateCreation = DateTime.Now;

                if (postResult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
            }
            //ModelState.AddModelError(string.Empty, "Server occured errors. Please check with admin!");
            return View(kindergarten);
        }

        // POST: Kindergarten/addKindergarten
        [HttpPost]
        public ActionResult addKindergarten(Kindergarten kindergarten)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                var postJob = client.PostAsJsonAsync<Kindergarten>("/SpringMVC/servlet/kindergarten/addKindergarten", kindergarten);
                postJob.Wait();
                // return View();
                var postResult = postJob.Result;
                DateTime dateCreation = DateTime.Now;

                if (postResult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
            }
            //ModelState.AddModelError(string.Empty, "Server occured errors. Please check with admin!");
           return View(kindergarten);
        }

        // GET: Kindergarten/Edit/5
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: Kindergarten/Edit/5
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

        // GET: Kindergarten/DeleteKindergarten/5
       
            public ActionResult DeleteKindergarten(int id)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8081");

                    //HTTP POST
                    var deleteTask = client.DeleteAsync("/SpringMVC/servlet/kindergarten/deleteKindergarten/" + id.ToString());
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");

                    }
                    return RedirectToAction("Index");

                }
            }

        // POST: Kindergarten/Delete/5
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
