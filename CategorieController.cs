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
    public class CategorieController : Controller
    {
        // GET: Categorie
        public ActionResult Index()
        {
            IEnumerable<Categorie> categorie = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8089/SpringMVC/servlet/");
                var responseTask = client.GetAsync("getAllCategorie");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Categorie>>();
                    readJob.Wait();
                    categorie = readJob.Result;

                }
                else
                {
                    categorie = Enumerable.Empty<Categorie>();
                    ModelState.AddModelError(string.Empty, "error");
                }
            }
            return View(categorie);
        }
        //Post:Categorie
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Categorie cat)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8089/SpringMVC/servlet/");
                var postJob = client.PostAsJsonAsync<Categorie>("ajouterCategorie", cat);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                    return RedirectToAction("index");
            }
            ModelState.AddModelError(string.Empty, "error");
            ModelState.AddModelError(string.Empty, cat.NomCat);
            ModelState.AddModelError(string.Empty, "error");
            return View(cat); ;


        }



        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8089/SpringMVC/servlet/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("deleteCategorieById/" + id.ToString());
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
        

    