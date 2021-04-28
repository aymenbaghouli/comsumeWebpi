using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MedecinController : Controller
    {
        // GET: Medecin
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8081/SpringMVC/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("servlet/getAllmeetsmed").Result;
            if (response.IsSuccessStatusCode)
            {
                var Meetingss
                     = response.Content.ReadAsAsync<IEnumerable<Meetmedecin>>().Result;

                return View(Meetingss);

            }


            else
            {
                ViewBag.result = "error";
                return View(new List<Meetmedecin>());


            }
        }



        public ActionResult AddOrEdit(int id = 0)
        {


            if (id == 0)
                return View(new Meetmedecin());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("servlet/getAllmeetsmed").Result;



                var metingg = new Meetmedecin();
                var Medecin = response.Content.ReadAsAsync<List<Meetmedecin>>().Result;
                foreach (var item in Medecin)
                {
                    metingg = item;
                }

                return View(metingg);
            }

        }




        [HttpPost]
        public ActionResult AddOrEdit(Meetmedecin evm)

        {



            if (evm.idmeetmed == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("servlet/saveMeetmed", evm).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("servlet/updateMeetmed", evm).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }


            return RedirectToAction("Index");
        }






        public ActionResult Delete(int id)
        {
            HttpClient Client = new HttpClient();



            var response = Client.DeleteAsync("http://localhost:8081/SpringMVC/servlet/deleteMeetmed?idmeetmed=" + id.ToString()).ContinueWith(DeleteTask => DeleteTask.Result.EnsureSuccessStatusCode());


            TempData["SuccessMessage"] = "Deleted Successfully";


            return RedirectToAction("Index");
        }





        public FileResult Download()

        {


            String path = Server.MapPath("~/App_Data/File");
            String fileName = Path.GetFileName("testenfant.PNG");
            String fullPath = Path.Combine(path, fileName);
            return File(fullPath, " image/PNG");

        }























































    }
}