using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIDEV_NET.Models;
using System.Web.Http;
using System.Net.Http;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using java.io;


namespace PIDEV_NET.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8087");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/jardin/Event/getAllEvent").Result;
            IEnumerable<Event> result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Event>>().Result;
            }
            else
            {
                result = null;
            }
            return View(result);
        }
        

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        // GET: Event/Create
        public ActionResult Create(Event evm)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8087");
            Client.PostAsJsonAsync<Event>("/jardin/Event/addEvent", evm).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("/jardin/Event/DeleteEventById/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            Event c = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087");
                //HTTP GET
                var responseTask = client.GetAsync("/jardin/Event/modifyEvent" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Event>();
                    readTask.Wait();

                    c = readTask.Result;
                }
            }

            return View(c);
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(Event c)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:80877");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Event>("/jardin/Event/modifyEvent", c);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(c);
        }
    }
    
}
    

