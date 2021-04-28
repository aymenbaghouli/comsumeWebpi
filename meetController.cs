using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class meetController : Controller


    {


        // GET: meet

        public ActionResult Index()
        {

         
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8081/SpringMVC/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("servlet/getAllmeets").Result;
            if (response.IsSuccessStatusCode)
            {
                var Meetings
                     = response.Content.ReadAsAsync<IEnumerable<Meeting>>().Result;

                return View(Meetings);

            }


            else
            {
                ViewBag.result = "error";
                return View(new List<Meeting>());


            }



        }



      
        public ActionResult  AddOrEdit(int id =0)
        {

          
            if (id == 0)
                return View(new Meeting());
            else
            {
               HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("servlet/getAllmeets").Result;



                var meting = new Meeting();
                var meet = response.Content.ReadAsAsync<List<Meeting>>().Result;
                foreach(var item in meet)
                {
                    meting = item;
                }

                return View(meting);
            }

        }
       



       [HttpPost]
        public ActionResult AddOrEdit(Meeting evm)

        {



            if (evm.idmeet == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("servlet/saveMeet", evm).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("servlet/updateMeet" , evm).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
     
          
            return RedirectToAction("Index");
        }




   








      




       








     






        public ActionResult Delete (int id )
        {
            HttpClient Client = new HttpClient();
          
          

            var response = Client.DeleteAsync("http://localhost:8081/SpringMVC/servlet/deleteMeet?idmeet=" + id.ToString()).ContinueWith(DeleteTask=>DeleteTask.Result.EnsureSuccessStatusCode());
         
            
            TempData["SuccessMessage"] = "Deleted Successfully";
        

            return RedirectToAction("Index");
        }





     /*  public ActionResult FileUpload()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> FileUpload(IFormFile file)
        {
            await UploadFile(file);
            TempData["msg"] = "File Uploaded successfully.";
            return View();
        }
        // Upload file on server
        public async Task<bool> UploadFile(IFormFile file)
        {
            string path = "";
            bool iscopied = false;
            try
            {
                if (file.Length >0)
                {
                    string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Upload"));
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                    iscopied = true;
                }
                else
                {
                    iscopied = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return iscopied;
        }

        public interface IFormFile
        {
            int Length { get; }
            string FileName { get; }

            Task CopyToAsync(FileStream filestream);
        }





         <div class="form-group">
        <div class="col-md-10">
            <p>Upload File</p>
            <input type = "file" name= "file" />
        </ div >
    </ div >
    < div class="form-group">
        <input type = "submit" value="Upload file" />
    </div>
    <div class="form-group">
        <span>@TempData["msg"]</span>
    </div> */















    }



  

}











/*< link href = "~/Content/css/bootstrap.min.css" rel = "stylesheet" >


      < !--//booststrap end-->

      < !--font - awesome icons-- >

        < link rel = "stylesheet" href = "css/font-awesome.min.css" />


           < !-- //font-awesome icons -->

           < !--stylesheets-- >

           < link href = "css/style.css" rel = 'stylesheet' type = 'text/css' media = "all" >

                  < link rel = "stylesheet" href = "css/flexslider.css" type = "text/css" media = "screen" /> < !--For - News - CSS-- >


                         < link href = "css/main.css" rel = "stylesheet" /> < !--For - Portfolio - CSS-- >

                            < link href = "//fonts.googleapis.com/css?family=Asap+Condensed:400,500,600,700" rel = "stylesheet" >






  <script type="application/x-javascript">
       addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false);
              function hideURLbar(){ window.scrollTo(0,1); } </script>
   <!--//meta tags ends here-->
   <!--booststrap-->

   <!--//style sheet end here-->




 @Html.ActionLink("Edit", "AddOrEdit", new { id = item.idmeet }) |
                @Html.ActionLink("Details", "Details", new { id = item.idmeet }) |
                @Html.ActionLink("Delete", "Delete", new { id = item.idmeet })

 */
