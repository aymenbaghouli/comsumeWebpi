using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIDEV_NET.Models;
using System.Web.Http;
using System.Net.Http;
using PIDEV_NET.Report;

namespace PIDEV_NET.Controllers
{
    public class ParticipantController : Controller
    {

        // GET: Event
        public ActionResult Participant()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8088");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/consommiTounsi/Participant/getAllParticipant").Result;
            IEnumerable<Participant> result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Participant>>().Result;
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
        // GET: Participant/Create
        public ActionResult Create(Participant p)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8088");
            Client.PostAsJsonAsync<Participant>("/consommiTounsi/Participant/addParticipant", p).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Participant");
        }



        public ActionResult Delete(long idpar)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8088");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("/consommiTounsi/Participant/DeleteParticipantById/" + idpar.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Participant");
                }
            }

            return RedirectToAction("Participant");

        }
        public ActionResult Edit(int iduser)
        {
            Participant c = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8088");
                //HTTP GET
                var responseTask = client.GetAsync("/consommiTounsi/Participant/retrieve-Participant/" + iduser.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Participant>();
                    readTask.Wait();

                    c = readTask.Result;
                }
            }

            return View(c);
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(Participant p)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8088");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Participant>("/consommiTounsi/Participant/modifyParticipant", p);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Participant");
                }
            }
            return View(p);
        }
        public ActionResult Report(Participant participant)
        {

            ParticipantReport participantReport = new ParticipantReport();
            byte[] abytes = participantReport.PrepareReport(GetParticipants());
            return File(abytes, "application/pdf");
        }
        public List<Participant> GetParticipants()
        {
            List<Participant> participants = new List<Participant>();
            Participant participant = new Participant();
       
                for (int i = 1; i < 10; i++)
            {
                participant = new Participant();
                participant.id = i;
                //participant.PhoneNumber = "PhoneNumber" + i;

                //participant.Firstname = "Firstname" + 1;
                // participant.Lastname = "Lastname" + i;


       


            }
            return participants;

        }


    }

}
    
