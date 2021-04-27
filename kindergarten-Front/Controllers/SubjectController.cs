
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using Kiindergarten.Models;

namespace kindergarten_Front.Controllers
{
    public class SubjectController : Controller
    {

        HttpClient httpClient;
        string baseAddress = "http://localhost:8081/SpringMVC/servlet/";
        public SubjectController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);

        }
        // GET: Subject
        public async Task<ActionResult> Index()
        {
            var tokenResponse = await httpClient.GetAsync(baseAddress + "getAllSubjects");
            if (tokenResponse.IsSuccessStatusCode)
            {
                var app = await tokenResponse.Content.ReadAsAsync<IEnumerable<Subject>>();
                return View("~/Views/Subject/Index.cshtml", app.OrderByDescending(bi => bi.creationDate));
            }
            else
            {
                return View("~/Views/Subject/Index.cshtml", new List<Subject>());
            }
        }

        // GET: Subject/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getSubjecBytId/" + id);
            var a = await response.Content.ReadAsAsync<Subject>();
            return View(a);
        }

        // GET: Subject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        public async Task<ActionResult> Create(Subject subject)
        {
            var APIResponse = await httpClient.PostAsJsonAsync<Subject>(baseAddress +
                    "addSubjectWithImage?description=" + subject.description + "&name="
                    + subject.name + "&userId=" + subject.user.id , subject);

            return View();


        }

        // GET: Subject/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            var response = await httpClient.GetAsync(baseAddress + "getSubjecBytId/" + id);
            var a = await response.Content.ReadAsAsync<Subject>();
            return View(a);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Subject subject)
        {
            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PutAsJsonAsync<Subject>(baseAddress +
                    "updateSubject/"+ id +"?description=" + subject.description + "&name="
                    + subject.name, subject);

                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Subject/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getSubjecBytId/" + id);
            var a = await response.Content.ReadAsAsync<Subject>();
            return View(a);
        }

        // POST: Appointment/Delete/
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {

                var APIResponse = await httpClient.DeleteAsync(baseAddress + "deleteSubjectById/" + id);

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
    }
}
