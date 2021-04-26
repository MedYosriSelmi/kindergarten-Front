using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using kindergarten_Front.Models;

namespace kindergarten_Front.Controllers
{
    public class AppointmentController : Controller
    {
        HttpClient httpClient;
        string baseAddress = "http://localhost:8081/SpringMVC/servlet/";
        public AppointmentController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);

        }
        // GET: Appointment
        public async Task<ActionResult> Index()
        {
            var tokenResponse = await httpClient.GetAsync(baseAddress + "findParentAppointment/1");
            if (tokenResponse.IsSuccessStatusCode)
            {
                var app = await tokenResponse.Content.ReadAsAsync<IEnumerable<Appointment>>();
                return View("~/Views/Appointment/Index.cshtml", app.OrderByDescending(bi => bi.date));
            }
            else
            {
                return View("~/Views/Appointment/Index.cshtml", new List<Appointment>());
            }

        }
        // GET: Appointment/Details/
        public async Task<ActionResult> Details(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "oneapp/" + id);
            var a = await response.Content.ReadAsAsync<Appointment>();
            
            return View(a);

        }
        // GET: Appointment/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        public async Task<ActionResult> Create(Appointment bi)
        {


            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PostAsJsonAsync<Appointment>(baseAddress + "ajouter_Doctor_rendezVous/1/4", bi);
                
                return RedirectToAction("Index");
            }
            return View();
            

        }
        // GET: Appointment/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "oneapp/" + id);
            var a = await response.Content.ReadAsAsync<Appointment>();
            return View(a);
        }




        // POST: Appointment/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, int idj, Appointment bi)
        {
            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PutAsJsonAsync<Appointment>(baseAddress + "update_appointment_By_User/" + idj + "/" + id, bi);
                
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "oneapp/" + id);
            var a = await response.Content.ReadAsAsync<Appointment>();
            return View(a);
        }

        // POST: Appointment/Delete/
        [HttpPost]
        public async Task<ActionResult> Delete(int id, int idu ,FormCollection collection)
        {
            try
            {
                
                var APIResponse = await httpClient.DeleteAsync(baseAddress + "delete_appointment/" + idu + "/" + id);
                
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Index2()
        {
            var tokenResponse = await httpClient.GetAsync(baseAddress + "getallappointment_status_1/4");
            if (tokenResponse.IsSuccessStatusCode)
            {
                var app = await tokenResponse.Content.ReadAsAsync<IEnumerable<Appointment>>();
                return View("~/Views/Appointment/Index2.cshtml", app.OrderByDescending(bi => bi.date));
            }
            else
            {
                return View("~/Views/Appointment/Index2.cshtml", new List<Appointment>());
            }

        }
        // GET: Appointment/Edit/5
        public async Task<ActionResult> Edit2(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "oneapp/" + id);
            var a = await response.Content.ReadAsAsync<Appointment>();
            return View(a);
        }




        // POST: Appointment/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit2(int id, int idj, Appointment bi)
        {
            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PutAsJsonAsync<Appointment>(baseAddress + "update_appointment_By_Doctor/" + idj + "/" + id, bi);

                return RedirectToAction("Index2");
            }
            return View();
        }
        public ActionResult Accept()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Accept(int id,int idjj, Appointment bi)
        {
          
            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PutAsJsonAsync<Appointment>(baseAddress + "accepte_appointment/" + idjj + "/" + id, bi);

                return RedirectToAction("Index2");
            }
            return View();
        }
        public ActionResult Refus()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Refus(int id, int idjj, Appointment bi)
        {

            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PostAsJsonAsync<Appointment>(baseAddress + "refut_appointment/" + idjj + "/" + id, bi);

                return RedirectToAction("Index2");
            }
            return View();
        }
        // GET: Appointment/Details/
        public async Task<ActionResult> Details2(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "oneapp/" + id);
            var a = await response.Content.ReadAsAsync<Appointment>();

            return View(a);

        }
    }
}
