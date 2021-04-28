using Kiindergarten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace kindergarten_Front.Controllers
{
    public class MessageController : Controller
    {
        HttpClient httpClient;
        string baseAddress = "http://localhost:8081/SpringMVC/servlet/";
        public MessageController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);

        }
        // GET: Comment
        public async Task<ActionResult> getDiscussion()
        {
            var tokenResponse = await httpClient.GetAsync(baseAddress + "getDiscussion/" + 1 + "/" + 2);
            
                var app = await tokenResponse.Content.ReadAsAsync<IEnumerable<Message>>();
                return View("getDiscussion.cshtml");
            
        }

        // GET: Message/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        public async Task<ActionResult> AddMessage(Message message)
        {
            var APIResponse = await httpClient.PostAsJsonAsync<Message>(baseAddress +
                    "addMessage/" + 1
                    + "/" + 2
                    + "?description="
                    + message.description, message);

            return RedirectToAction("getDiscussion");


        }
        // GET: Subject/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            var response = await httpClient.GetAsync(baseAddress + "getMessageById/" + id);
            var a = await response.Content.ReadAsAsync<Message>();
            return View(a);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Message message)
        {
            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PutAsJsonAsync<Message>(baseAddress +
                    "updateMessage/" + id + "?description=" + message.description, message);

                return RedirectToAction("getDiscussion");
            }
            return View();
        }

        // GET: Subject/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getMessageById/" + id);
            var a = await response.Content.ReadAsAsync<Message>();
            return View(a);
        }

        // POST: Appointment/Delete/
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {

                var APIResponse = await httpClient.DeleteAsync(baseAddress + "deleteMessage/" + id);

                return RedirectToAction("getDiscussion");

            }
            catch
            {
                return View();
            }
        }
    }
}
