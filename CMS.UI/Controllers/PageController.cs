using CMS.Common.Models.PageContent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.UI.Controllers
{
    public class PageController : Controller
    {
        // GET: Page
       
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Index(int id)
        {
            string url = " http://localhost:10473/api/CmsApi/ShowPage/"+id;
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var json = client.DownloadString(url);
            var model = JsonConvert.DeserializeObject<List<ContentDto>>(json);
            return View(model);

        }

    }
}