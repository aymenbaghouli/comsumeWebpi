using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class jardinController : Controller
    {
        // GET: jardin
        public ActionResult Index()
        {

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8081/SpringMVC/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("servlet/getAllmeetsjar").Result;
            if (response.IsSuccessStatusCode)
            {
                var Meetingsss
                     = response.Content.ReadAsAsync<IEnumerable<Meetjardin>>().Result;

                return View(Meetingsss);

            }


            else
            {
                ViewBag.result = "error";
                return View(new List<Meetjardin>());


            }
        }







        public ActionResult AddOrEdit(int id = 0)
        {


            if (id == 0)
                return View(new Meetjardin());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("servlet/getAllmeetsjar").Result;



                var metingg = new Meetjardin();
                var jardin = response.Content.ReadAsAsync<List<Meetjardin>>().Result;
                foreach (var item in jardin)
                {
                    metingg = item;
                }

                return View(metingg);
            }

        }



        [HttpPost]
        public ActionResult AddOrEdit(Meetjardin evm)

        {



            if (evm.idmeetjardin == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("servlet/saveMeetjar", evm).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("servlet/updateMeetjar", evm).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }


            return RedirectToAction("Index");
        }



      
      








        [HttpPost]
        public ActionResult Sms(Meetjardin evm)
        {



            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("servlet/sms", evm).Result;


            TempData["SuccessMessage"] = "Message Sended Successfully";

            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Sms()
        {


            return View();


        }




        public ActionResult Delete(int id)
        {
            HttpClient Client = new HttpClient();



            var response = Client.DeleteAsync("http://localhost:8081/SpringMVC/servlet/deleteMeetjar?idmeetjardin=" + id.ToString()).ContinueWith(DeleteTask => DeleteTask.Result.EnsureSuccessStatusCode());


            TempData["SuccessMessage"] = "Deleted Successfully";


            return RedirectToAction("Index");
        }
















































    }
}