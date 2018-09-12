using CMS.Common.Models.PageContent;
using CMS.Domain.Domains;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.UInterface.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Menu()
        {
            string url = "http://localhost:10473/api/CmsApi/GetMenu";
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var json = client.DownloadString(url);
            var model = JsonConvert.DeserializeObject<string>(json);
            ViewBag.Menu = model;
            return PartialView();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Show(int id)
        {
            string url= "http://localhost:10473/api/CmsApi/ShowPage/"+id;
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var json = client.DownloadString(url);
            var model = JsonConvert.DeserializeObject<List<ContentDto>>(json);
            ViewBag.ShowPage = model;
            return View( model);
        }

        public ActionResult Slider()
        {
            string url = "http://localhost:10473/api/CmsApi/GetSlider";
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var json = client.DownloadString(url);
            var model = JsonConvert.DeserializeObject<List<Slider>>(json);
            return View(model);
        }
    }
}