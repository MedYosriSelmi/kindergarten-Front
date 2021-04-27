using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using kindergarten_Front.Models;


namespace kindergarten_Front.Controllers
{
    public class EventController : Controller
    {
        HttpClient httpClient;
        string baseAddress;
        public EventController()
        {
            baseAddress = "http://localhost:8081/SpringMVC/servlet/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            HttpContent content = new StringContent("");

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }



        public ActionResult get()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "getAllEvent").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var products = tokenResponse.Content.ReadAsAsync<IEnumerable<Event>>().Result;
                return View("~/Views/Event/get.cshtml", products);
            }
            else
            {
                return View("~/Views/Event/get.cshtml", new List<Event>());
            }
        }





        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getEventById/" + id);
            var b = await response.Content.ReadAsAsync<Event>();
            return View(b);
        }

        // POST: 
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {

                var APIResponse = await httpClient.DeleteAsync(baseAddress + "deleteEventById/" + id);

                return RedirectToAction("get");

            }
            catch
            {
                return View();
            }
        }

        // GET: Publication/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getEventById/" + id);
            var pub = await response.Content.ReadAsAsync<Event>();
            //      ViewBag.Value=10;
            //     ViewData["val"] = 20;
            return View(pub);

        }





        public async Task<ActionResult> Edit(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getEventById/" + id);
            var pub = await response.Content.ReadAsAsync<Event>();
            return View(pub);
        }




        // POST: Bill/Edit/
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Event a)
        {



            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PutAsJsonAsync<Event>(baseAddress + "update/" + id, a);

                return RedirectToAction("get");
            }
            return View();
        }
    




    public ActionResult getEventOfToday()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "retrievealleventsoftoday").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var products = tokenResponse.Content.ReadAsAsync<IEnumerable<Event>>().Result;
                return View("~/Views/Event/getEventOfToday.cshtml", products);
            }
            else
            {
                return View("~/Views/Event/getEventOfToday.cshtml", new List<Event>());
            }
        }


        public async Task<ActionResult> upload(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getEventById/" + id);
            var pub = await response.Content.ReadAsAsync<Event>();
            //      ViewBag.Value=10;
            //     ViewData["val"] = 20;
            return View(pub);
        }

        // POST: 
        [HttpPost]
        public async Task<ActionResult> upload(Event e, int id, HttpPostedFileBase myfile)
        {

            var n = System.Guid.NewGuid().ToString() + "_" + myfile.FileName;
            myfile.SaveAs("C:/Users/rihab/downloads/" + n);
            byte[] file = System.IO.File.ReadAllBytes("C:/Users/rihab/downloads/" + n);
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new ByteArrayContent(file, 0, myfile.ContentLength), "file", "file");
            if (ModelState.IsValid)
            {
                            var APIResponse = await httpClient.PostAsJsonAsync<Event>(baseAddress + "add-event/1/1", e);
                            //.ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                            return RedirectToAction("get");
                        }

            return View();
        }


        public ActionResult getFrontEvent()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "getAllEvent").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var products = tokenResponse.Content.ReadAsAsync<IEnumerable<Event>>().Result;
                return View("~/Views/Event/getFrontEvent.cshtml", products);
            }
            else
            {
                return View("~/Views/Event/getFrontEvent.cshtml", new List<Event>());
            }
        }

        // GET:
        public async Task<ActionResult> DetailsFront(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getEventById/" + id);
            var pub = await response.Content.ReadAsAsync<Event>();
            //      ViewBag.Value=10;
            //     ViewData["val"] = 20;
            return View(pub);

        }


        [HttpPost]
        public async Task<ActionResult> Create(Event pub, HttpPostedFileBase myfile)
        {
            var n = System.Guid.NewGuid().ToString() + "_" + myfile.FileName;
            myfile.SaveAs("C:/Users/rihab/downloads/" + n);
            byte[] file = System.IO.File.ReadAllBytes("C:/Users/rihab/downloads/" + n);
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new ByteArrayContent(file, 0, myfile.ContentLength), "file", "file");
            form.Add(new StringContent("1"), "idUser");
            form.Add(new StringContent("1"), "idkinder");
            form.Add(new StringContent(pub.name), "name");

            

            form.Add(new StringContent(Convert.ToString(pub.DateOfEvent)), "date");
            form.Add(new StringContent(pub.description), "description");
            


            if (ModelState.IsValid)
            {

                var APIResponse = await httpClient.PostAsync(baseAddress +"AddE", form);
                //.ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("get");
            }
            return View();
            // TODO: Add insert logic here

        }
        public ActionResult Create()
        {
            return View();
        }



       
    }






}

