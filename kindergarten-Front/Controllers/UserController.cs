
using kindergarten_Front.Models;
using Newtonsoft.Json;
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
    public class UserController : Controller
    {

        HttpClient httpClient;
        string baseAddress = "http://localhost:8081/api/auth/";
        public UserController()
        {

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);

        }
        // GET: User
        public async Task<ActionResult> Index()
        {
            var tokenResponse = await httpClient.GetAsync(baseAddress + "getAllUsers");
            if (tokenResponse.IsSuccessStatusCode)
            {
                var users = await tokenResponse.Content.ReadAsAsync<IEnumerable<User>>();
                return View(users.OrderByDescending(c => c.dateOfBirth));
            }
            else
            {
                return View(new List<User>());
            }

        }

        // GET: SignUp
        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(User user)
        {


            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PostAsJsonAsync<User>(baseAddress + "register", user);
                //.ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("SignIn");
            }
            return View();
            // TODO: Add insert logic here

        }

        //public ActionResult index()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult SignUp(User customer)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:8081/");
        //        var postJob = client.PostAsJsonAsync<User>("api/auth/register", customer);
        //        postJob.Wait();
        //        // return View();
        //        var postResult = postJob.Result;
        //        //DateTime dateCreation = DateTime.Now;

        //        if (postResult.IsSuccessStatusCode)

        //            return RedirectToAction("SignIn");
        //    }
        //    //ModelState.AddModelError(string.Empty, "Server occured errors. Please check with admin!");
        //    return View(customer);
        //}

        public async Task<ActionResult> Details(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "getUserById/" + id);
            var c = await response.Content.ReadAsAsync<User>();

            return View(c);

        }

        

        public ActionResult login()


        {


            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(LoginRequest loginRequest)  //JwtRequest model 3emluo ena maoujoud e5er lfichier


        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginRequest), System.Text.Encoding.UTF8, "application/json");

                using (var response1 = await httpClient.PostAsync("http://localhost:8081/api/auth/authenticate", stringContent))
                {
                    String myTok = await response1.Content.ReadAsStringAsync();

                    if (myTok == "Invalid credentials")
                    {
                        ViewBag.mssg = "incorrct usr";
                    }

                    HttpContext.Session.Add("access_token", myTok); 
                    var _AccessToken = System.Web.HttpContext.Current.Session["access_token"];


                    ViewBag.bareer = stringContent;
                    ViewBag.bareerConsumedResps = stringContent;

                    ViewBag.bareerConsumed = myTok;
                    ViewBag.bareeSession = _AccessToken;
                }
            }

            return View();
        }

    }
}