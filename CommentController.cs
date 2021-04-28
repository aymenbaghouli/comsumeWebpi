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
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44343");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:8089/SpringMVC/servlet/getAllComment").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<comment>>().Result;
                Console.WriteLine("test1");
            }
            else
            {
                ViewBag.result = "erreur";
            }

            return View();
        }

        //Post:comment
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(comment com)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8089/SpringMVC/servlet/");
                var postJob = client.PostAsJsonAsync<comment>("ajouterComment", com);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                    return RedirectToAction("index");
            }
            ModelState.AddModelError(string.Empty, "error");
            ModelState.AddModelError(string.Empty, com.reaction);
            ModelState.AddModelError(string.Empty, "error");
            return View(com);


        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8089/SpringMVC/servlet/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("deleteCommentById/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");


        }



    }
}