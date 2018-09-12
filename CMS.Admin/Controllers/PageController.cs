using CMS.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class PageController : Controller
    {

        #region Page
        [HttpPost]
        public ActionResult CreatePage(string name, int id)
        {
            Services.PageService.CreatePage(name, id);
            return RedirectToAction("Page");
        }
        public ActionResult GetPageLayout()
        {
            var model = Services.LayoutService.GetLayoutPage();
            return View("Page", model);
        }
        
        #endregion
    }
}