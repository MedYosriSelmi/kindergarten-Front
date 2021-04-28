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
    public class CommentController : Controller
    {

        HttpClient httpClient;
        string baseAddress = "http://localhost:8081/SpringMVC/servlet/";
        public CommentController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);

        }
        // GET: Comment
        public async Task<ActionResult> Parent(int subjectId)
        {
            var tokenResponse = await httpClient.GetAsync(baseAddress + "getCommentBySubjectId/" + subjectId);
            
                var app = await tokenResponse.Content.ReadAsAsync<IEnumerable<Comment>>();
                return View(app);
            

        }

        // GET: Comment
        public async Task<ActionResult> Child(int parentId)
        {
            var tokenResponse = await httpClient.GetAsync(baseAddress + "getCommentByParentId/" + parentId);
            if (tokenResponse.IsSuccessStatusCode)
            {
                var app = await tokenResponse.Content.ReadAsAsync<IEnumerable<Comment>>();
                return View("~/Views/Comment/Child/"+ parentId, app.OrderByDescending(bi => bi.creationDate).Reverse());
            }
            else
            {
                return View("~/Views/Comment/Index.cshtml", new List<Comment>());
            }
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comment/Create
        public ActionResult AddParentComment()
        {
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        public async Task<ActionResult> AddParentComment(Comment comment, int subjectId, int userId)
        {
            var APIResponse = await httpClient.PostAsJsonAsync<Comment>(baseAddress +
                    "addParentComment/" + subjectId 
                    + "/"+ userId 
                    + "?description=" 
                    + comment.description , comment);

            return RedirectToAction("../Subject/IndexFront");


        }

        public ActionResult AddChildComment()
        {
            return View();
        }

        public async Task<ActionResult> AddChildComment(Comment comment, int subjectId, int userId, int parentId)
        {
            var APIResponse = await httpClient.PostAsJsonAsync<Comment>(baseAddress +
                    "addChildComment/" + subjectId
                    + "/" + userId
                    + "/" + parentId
                    + "?description="
                    + comment.description, comment);

            return RedirectToAction("../Subject/IndexFront");


        }

        // GET: Subject/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            var response = await httpClient.GetAsync(baseAddress + "getCommentBytId/" + id);
            var a = await response.Content.ReadAsAsync<Comment>();
            return View(a);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Comment comment)
        {
            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PutAsJsonAsync<Comment>(baseAddress +
                    "updateComment/" + id + "?description=" + comment.description, comment);

                return RedirectToAction("Index");
            }
            return View();
        }
        // GET: Subject/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getCommentBytId/" + id);
            var a = await response.Content.ReadAsAsync<Comment>();
            return View(a);
        }

        // POST: Appointment/Delete/
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {

                var APIResponse = await httpClient.DeleteAsync(baseAddress + "deleteComment/" + id);

                return RedirectToAction("AddParentComment");

            }
            catch
            {
                return View();
            }
        }
    }
}
