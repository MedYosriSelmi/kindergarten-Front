using kindergarten_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace kindergarten_Front.Controllers
{
    public class ListParticipantsController : Controller
    {


        HttpClient httpClient;
        string baseAddress;
        public ListParticipantsController()
        {
            baseAddress = "http://localhost:8081/SpringMVC/servlet/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            HttpContent content = new StringContent("");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        [HttpGet]
        // GET: ListParticipants
        public ActionResult Index()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "getAllLP").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var products = tokenResponse.Content.ReadAsAsync<IEnumerable<ListParticipants>>().Result;
              
                return View("~/Views/ListParticipants/Index.cshtml", products);
            }
            else
            {
                return View("~/Views/ListParticipants/Index.cshtml", new List<ListParticipants>());
            }
        }

        // GET: ListParticipants/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListParticipants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListParticipants/Create
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

        // GET: ListParticipants/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListParticipants/Edit/5
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

        // GET: ListParticipants/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListParticipants/Delete/5
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
