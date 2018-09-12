using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using CMS.Domain.Domains;
using System.Net.Http;

namespace CMS.UI.Controllers
{
    public class MenuController : Controller
    {
        // GET: Page

        public ActionResult Index()
        {
            string url = "http://localhost:10473/api/CmsApi/GetMenu";
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var json = client.DownloadString(url);
            var model = JsonConvert.DeserializeObject<string>(json);
            ViewBag.Menu = model;
            return View();

        }
    }
}