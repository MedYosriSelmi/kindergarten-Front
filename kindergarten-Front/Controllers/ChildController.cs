using kindergarten_Front.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace kindergarten_Front.Controllers
{
    public class ChildController : Controller
    {
        HttpClient httpClient;
        string baseAddress = "http://localhost:8081/api/auth/";
        public ChildController()
        {

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);

        }

        // GET: Child
        public async Task<ActionResult> Index()
        {
            var tokenResponse = await httpClient.GetAsync(baseAddress + "Children");
            if (tokenResponse.IsSuccessStatusCode)
            {
                var children = await tokenResponse.Content.ReadAsAsync<IEnumerable<Child>>();
                return View(children.OrderByDescending(c => c.dateOfBirth));
            }
            else
            {
                return View(new List<Child>());
            }

        }

        // GET: Child/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Child/Create
        [HttpPost]
        public async Task<ActionResult> Create(Child child , MultipartFileStreamProvider file)
        {
            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PostAsJsonAsync<Child>(baseAddress + 
                    "ajouterChild?idUser=7&idkinder=1&date="+child.dateOfBirth+"&name="
                    +child.name+"&photo="+file.FileData, child);
                //.ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index");
            }
            return View();
            // TODO: Add insert logic here

        }


        //// GET: Child/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "child/" + id);
            var c = await response.Content.ReadAsAsync<Child>();
            return View(c);
        }
        // POST: Publication/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                var APIResponse = await httpClient.DeleteAsync(baseAddress + "deleteChildById/" + id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        

        public async Task<ActionResult> Details(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "child/" + id);
            var c = await response.Content.ReadAsAsync<Child>();

            return View(c);

        }
    }
}