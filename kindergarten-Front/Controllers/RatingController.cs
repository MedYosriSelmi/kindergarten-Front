using kindergarten_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace kindergarten_Front.Controllers
{
    public class RatingController : Controller
    {
        HttpClient httpClient;
        string baseAddress;
        public RatingController()
        {
            baseAddress = "http://localhost:8081/SpringMVC/servlet/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            HttpContent content = new StringContent("");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //public ActionResult View()
        //{
        //    return View();
        //}

        //// POST: Publication/Create
        //[HttpPost]
        //public async Task<ActionResult> View(Rating pub)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        var APIResponse = await httpClient.PostAsJsonAsync<Rating>(baseAddress + "3", pub);
        //        //.ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
        //        return RedirectToAction("get");
        //    }
        //    return View();
        //    // TODO: Add insert logic here

        //}

        // GET: Rating/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rating/Create
      

        // GET: Rating/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rating/Edit/5
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

        // GET: Rating/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rating/Delete/5
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
