using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIDEV_NET.Models;
using System.Web.Http;
using System.Net.Http;
using Rotativa;

namespace PIDEV_NET.Controllers
{
    public class JackPotController : Controller
    {
        // GET: JackPot
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8088");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/consommiTounsi/JackPot/getAllJackPot").Result;
            IEnumerable<JackPot> result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<JackPot>>().Result;
            }
            else
            {
                result = null;
            }
            return View(result);
        }

        public ActionResult Delete(long id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8088");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("/consommiTounsi/JackPot/DeleteJackPotById/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        // GET: JackPot/Create
        public ActionResult Create(JackPot evm)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8088");
            Client.PostAsJsonAsync<JackPot>("/consommiTounsi/JackPot/addJackPot", evm).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }
        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            JackPot c = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8088");
                //HTTP GET
                var responseTask = client.GetAsync("/consommiTounsi/JackPot/retrieve-JackPot/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<JackPot>();
                    readTask.Wait();

                    c = readTask.Result;
                }
            }

            return View(c);
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(JackPot c)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8088");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<JackPot>("/consommiTounsi/JackPot/modifyJackPot", c);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(c);
        }
        public ActionResult PrintPDF()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8088");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/consommiTounsi/JackPot/getAllJackPot").Result;
            IEnumerable<JackPot> result;
     
            
                result = response.Content.ReadAsAsync<IEnumerable<JackPot>>().Result;
            
                
            

            return new PartialViewAsPdf("_JobPrint", result )
            {
                FileName = "TestPartialViewAsPdf.pdf"
            };
        }

       
    }
}
