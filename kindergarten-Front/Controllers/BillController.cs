using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using kindergarten_Front.Models;


namespace kindergarten_Front.Controllers
{
    public class BillController : Controller
    {
        // GET: Bill
        HttpClient httpClient;
        string baseAddress = "http://localhost:8081/SpringMVC/servlet/";
        public BillController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);

        }
        // GET: Bill
        public async Task<ActionResult> Index()
        {
            var tokenResponse = await httpClient.GetAsync(baseAddress + "listofbill");
            if (tokenResponse.IsSuccessStatusCode)
            {
                var publications = await tokenResponse.Content.ReadAsAsync<IEnumerable<Bill>>();
                return View("~/Views/Bill/getbill.cshtml", publications.OrderByDescending(bi => bi.dateOfBill));
            }
            else
            {
                return View ("~/Views/Bill/getbill.cshtml", new List<Bill>());
            }


        }
        public async Task<ActionResult> Indexuser()
        {
            var tokenResponse = await httpClient.GetAsync(baseAddress + "getAllBillByUser/1"  );
            if (tokenResponse.IsSuccessStatusCode)
            {
                var publications = await tokenResponse.Content.ReadAsAsync<IEnumerable<Bill>>();
                return View("~/Views/Bill/Indexuser.cshtml", publications.OrderByDescending(bi => bi.dateOfBill));
            }
            else
            {
                return View("~/Views/Bill/Indexuser.cshtml", new List<Bill>());
            }


        }
        public async Task<ActionResult> Details1(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "onebill/" + id);
            var b = await response.Content.ReadAsAsync<Bill>();

            return View(b);

        }

        // GET: Bill/Details/
        public async Task<ActionResult> Details(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "onebill/" + id );
            var b = await response.Content.ReadAsAsync<Bill>();
            
            return View(b);

        }
        // GET: Bill/Create
        public  ActionResult CreateBill()
        {
           
           
            return View();
        }

        // POST: Bill/Create
        [HttpPost]
        public async Task<ActionResult> CreateBill(Bill bi)
        {


            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PostAsJsonAsync<Bill>(baseAddress + "ajout_Bill_To_User/1/1", bi);
                
                return RedirectToAction("Index");
            }
            return View();
            

        }
        // GET: Bill/Edit/
        public async Task<ActionResult> Edit(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "onebill/" + id );
            var pub = await response.Content.ReadAsAsync<Bill>();
            return View(pub);
        }




        // POST: Bill/Edit/
        [HttpPost]
        public async Task<ActionResult> Edit(int id, int idj , Bill bi)
        {
            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PutAsJsonAsync<Bill>(baseAddress + "update_Bill/"+idj+"/"+id  ,bi);
                
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(baseAddress + "onebill/" + id);
            var b = await response.Content.ReadAsAsync<Bill>();
            return View(b);
        }

        // POST: Bill/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                
                var APIResponse = await httpClient.DeleteAsync(baseAddress + "delete_Bill/" + id);
                
                return RedirectToAction("Index");
               
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> calcul(int id  , Bill bi)
        {
            if (ModelState.IsValid)
            {
                var APIResponse = await httpClient.PutAsJsonAsync<Bill>(baseAddress + "calculp/"+ id,bi);
                
                return RedirectToAction("Index");
            }
            return View();
        }
        
               public async Task<ActionResult> imprimer(String a)

        {
            a = "pdf";
           
            var tokenResponse = await httpClient.GetAsync(baseAddress + "report/"+a );
            if (tokenResponse.IsSuccessStatusCode)
            {
              
                return View("~/Views/Bill/pdf.cshtml");
            }
            else
            {
                return View("~/Views/Bill/getbill.cshtml", new List<Bill>());
            }


        }
        public async Task<ActionResult> imprimerforuser(String a )

        {
            a = "pdf";

            var tokenResponse = await httpClient.GetAsync(baseAddress + "reportForUser/1/" + a);
            if (tokenResponse.IsSuccessStatusCode)
            {

                return View("~/Views/Bill/pdf.cshtml");
            }
            else
            {
                return View("~/Views/Bill/Indexuser.cshtml", new List<Bill>());
            }


        }

    }
}