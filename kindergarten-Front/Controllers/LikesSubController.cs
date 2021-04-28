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
    public class LikesSubController : Controller
    {
        HttpClient httpClient;
        string baseAddress = "http://localhost:8081/SpringMVC/servlet/";
        public LikesSubController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);

        }

        // GET: LikesSub
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comment
        public async Task<ActionResult> nbrLikesPerSubject(int subjectId)
        {
            var tokenResponse = await httpClient.GetAsync(baseAddress + "nbrLikesPerSubject/" + subjectId);
            if (tokenResponse.IsSuccessStatusCode)
            {
                var app = await tokenResponse.Content.ReadAsAsync<int>();
                return View("~/Views/Subject/Index.cshtml");
            }
            else
            {
                return View("~/Views/Subject/Index.cshtml");
            }
        }
        // GET: Comment
        public async Task<ActionResult> nbrLikesPerUser(int userId)
        {
            var tokenResponse = await httpClient.GetAsync(baseAddress + "nbrLikesPerUser/" + userId);
            if (tokenResponse.IsSuccessStatusCode)
            {
                var app = await tokenResponse.Content.ReadAsAsync<int>();
                return View("~/Views/Subject/Index.cshtml");
            }
            else
            {
                return View("~/Views/Subject/Index.cshtml");
            }
        }

        // GET: Comment/Create
        public ActionResult AddLike()
        {
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        public async Task<ActionResult> AddLike( LikesSub likesSub, int userId, int subjectId)
        {
            var APIResponse = await httpClient.PostAsJsonAsync<LikesSub>(baseAddress +
                    "addLikesSub/" + userId
                    + "/" + subjectId , likesSub);

            return RedirectToAction("../Subject/IndexFront");


        }

        // GET: LikesSub/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LikesSub/Edit/5
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

        // GET: LikesSub/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LikesSub/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int userId, int subjectId, FormCollection collection)
        {
            try
            {

                var APIResponse = await httpClient.DeleteAsync(baseAddress + "deleteLike/" + userId + "/" + subjectId);

                return RedirectToAction("../Subject/IndexFront");

            }
            catch
            {
                return View();
            }
        }
    }
}
