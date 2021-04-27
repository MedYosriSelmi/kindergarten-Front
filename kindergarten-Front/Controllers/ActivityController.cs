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
    public class ActivityController : Controller
    {
        HttpClient httpClient;
        string baseAddress;
        public ActivityController()
        {
            baseAddress = "http://localhost:8081/SpringMVC/servlet/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            HttpContent content = new StringContent("");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult getactivity()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress +"getAllActivity").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var products = tokenResponse.Content.ReadAsAsync<IEnumerable<Activity>>().Result;
                return View("~/Views/Activity/getactivity.cshtml", products);
            }
            else
            {
                return View("~/Views/Activity/getactivity.cshtml", new List<Activity>());
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getactivityById/" + id);
            var b = await response.Content.ReadAsAsync<Activity>();
            return View(b);
        }

        // POST: Bill/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {

                var APIResponse = await httpClient.DeleteAsync(baseAddress + "deleteActivityById/" + id);

                return RedirectToAction("getactivity");

            }
            catch
            {
                return View();
            }
        }


        // GET: Publication/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publication/Create
        [HttpPost]
        public async Task<ActionResult> Create(Activity pub, HttpPostedFileBase myfile)
        {


            var n = System.Guid.NewGuid().ToString() + "_" + myfile.FileName;
            myfile.SaveAs("C:/Users/rihab/downloads/" + n);
            byte[] file = System.IO.File.ReadAllBytes("C:/Users/rihab/downloads/" + n);
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new ByteArrayContent(file, 0, myfile.ContentLength), "file", "file");
            form.Add(new StringContent("1"), "idUser");
            form.Add(new StringContent("1"), "idkinder");
            form.Add(new StringContent(pub.name), "name");

            form.Add(new StringContent(Convert.ToString(pub.DateOfActivity)), "date");
            form.Add(new StringContent(pub.description), "description");
            form.Add(new StringContent(Convert.ToString(pub.category)), "category");



            if (ModelState.IsValid)
            {

                var APIResponse = await httpClient.PostAsync(baseAddress + "AddA", form);
                //.ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("getactivity");
            }
            return View();
            // TODO: Add insert logic here

        }


        // GET: 
        public async Task<ActionResult> Details(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getactivityById/" + id);
            var pub = await response.Content.ReadAsAsync<Activity>();
            //      ViewBag.Value=10;
            //     ViewData["val"] = 20;
            return View(pub);

        }


        public ActionResult getactivityFront()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "getAllActivity").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var products = tokenResponse.Content.ReadAsAsync<IEnumerable<Activity>>().Result;
                return View("~/Views/Activity/getactivityFront.cshtml", products);
            }
            else
            {
                return View("~/Views/Activity/getactivityFront.cshtml", new List<Activity>());
            }
        }

        public ActionResult getActivityOfToday()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "retrieveallActivitysoftoday").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var products = tokenResponse.Content.ReadAsAsync<IEnumerable<Activity>>().Result;
                return View("~/Views/Activity/getActivityOfToday.cshtml", products);
            }
            else
            {
                return View("~/Views/Activity/getActivityOfToday.cshtml", new List<Activity>());
            }
        }

        // GET:
        public async Task<ActionResult> DetailsFrontactivity(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getactivityById/" + id);
            var pub = await response.Content.ReadAsAsync<Activity>();
            //      ViewBag.Value=10;
            //     ViewData["val"] = 20;
            return View(pub);

        }

        public ActionResult getActivitybycategory()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "/all").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var products = tokenResponse.Content.ReadAsAsync<IEnumerable<Activity>>().Result;
                return View("~/Views/Activity/getActivityOfToday.cshtml", products);
            }
            else
            {
                return View("~/Views/Activity/getActivityOfToday.cshtml", new List<Activity>());
            }
        }
            public ActionResult pagination()
            {
                var tokenResponse = httpClient.GetAsync(baseAddress + "pagination?pageSize=1&pageNo=0&sortBy=").Result;
                if (tokenResponse.IsSuccessStatusCode)
                {
                    var products = tokenResponse.Content.ReadAsAsync<IEnumerable<Activity>>().Result;
                    return View("~/Views/Activity/getactivityFront.cshtml", products);
                }
                else
                {
                    return View("~/Views/Activity/getactivityFront.cshtml", new List<Activity>());
                }
            }


        public async Task<ActionResult> Edit(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getactivityById/" + id);
            var pub = await response.Content.ReadAsAsync<Activity>();
            return View(pub);
        }




        // POST: Bill/Edit/
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Activity a)
        {

           

            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PutAsJsonAsync<Activity>(baseAddress + "updateA/"+id , a);

                return RedirectToAction("getactivity");
            }
            return View();
        }
    }
}
