using pi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace pi.Controllers
{
    public class PublicationController : Controller
    {
        // GET: Publication
        public ActionResult Index()
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44343");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:8089/SpringMVC/servlet/getAllpublication").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Publication>>().Result;
                Console.WriteLine("test1");
            }
            else
            {
                ViewBag.result = "erreur";
            }

            return View();


        }

        //Post:Publication
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Publication pub)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8089/SpringMVC/servlet/");
                var postJob = client.PostAsJsonAsync<Publication>("ajouterPub", pub);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                    return RedirectToAction("index");
            }
            ModelState.AddModelError(string.Empty, "error");
            ModelState.AddModelError(string.Empty, pub.photo);
            ModelState.AddModelError(string.Empty, "error");
            return View(pub);


        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8089/SpringMVC/servlet/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("deletePublicationById/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");


        }
        /* public ActionResult Edit(int id)
         {
             Publication pub = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri("http://localhost:8089/SpringMVC/servlet/");
                 
                 var responseTask = client.GetAsync("update" + pub);
                 responseTask.Wait();

                 var result = responseTask.Result;
                 if (result.IsSuccessStatusCode)
                 {
                     var readTask = result.Content.ReadAsAsync<Publication>();
                     readTask.Wait();

                     pub = readTask.Result;
                 }
             }

             return View(pub);
         }

         public ActionResult Edit(Publication pub)
         {
             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri("http://localhost:8089/SpringMVC/servlet/");

                 //HTTP POST
                 var putTask = client.PutAsJsonAsync<Publication>("ajouterPub", pub);
                 putTask.Wait();

                 var result = putTask.Result;
                 if (result.IsSuccessStatusCode)
                 {

                     return RedirectToAction("Index");
                 }
             }
             return View(pub);
         }
        

       /* public ActionResult Edit(Publication pub)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8089/SpringMVC/servlet/");
                var putTask = client.PutAsJsonAsync<Publication>("mettreAjourPublicationById", pub);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                return View(pub);
            }*/


        public ActionResult AddOrEdit(int id = 0)
        {


            if (id == 0)
                return View(new Publication());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("servlet/getAllpublication").Result;



                var pub = new Publication();
                var jardin = response.Content.ReadAsAsync<List<Publication>>().Result;
                foreach (var item in jardin)
                {
                    pub = item;
                }

                return View(pub);
            }

        }



        [HttpPost]
        public ActionResult AddOrEdit(Publication evm)

        {



            if (evm.idPub == 0)
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
    }
    }   
    
