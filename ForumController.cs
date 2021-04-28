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
    public class ForumController : Controller
    {
        // GET: forum
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44343");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:8089/SpringMVC/servlet/getAllForms").Result;

            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<forum>>().Result;
                Console.WriteLine("test1");
            }
            else
            {
                ViewBag.result = "erreur";
            }

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(forum f)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44343");
            client.PostAsJsonAsync<forum>("http://localhost:8089/SpringMVC/servlet/ajouterForum", f).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            client.BaseAddress = new Uri("http://localhost:8089/SpringMVC/servlet/");
            var categories = client.GetAsync("getAllCategorie");
            ViewBag.IdCat = new SelectList((System.Collections.IEnumerable)categories, "idCat", "NomCat");

            return RedirectToAction("index");



        }

       
        public ActionResult Delete(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8089/SpringMVC/servlet/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("deleteForumById/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");

        }

        [HttpPut]
        public ActionResult Edit(forum f)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44343");
            client.PostAsJsonAsync<forum>("http://localhost:8089/SpringMVC/servlet/mettreAjourForumById", f).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }
    }


}