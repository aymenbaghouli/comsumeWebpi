using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIDEV_NET.Models;
using System.Web.Http;
using System.Net.Http;

namespace PIDEV_NET.Controllers
{
    public class DonationsController : Controller
    {
        // GET: Donations
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8088");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/consommiTounsi/Donations/getAllDonations").Result;
            IEnumerable<Donations> result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Donations>>().Result;
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
                var deleteTask = client.DeleteAsync("/consommiTounsi/Donations/DeleteDonationsById/" + id.ToString());
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
