using CMS.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class PageContentController : Controller
    {

        public ActionResult GetPage()
        {
            var model = Services.PageService.GetPageList();
            return View("CreatePageContent", model);
        }
        public PartialViewResult GetPageByIdList(int id)
        {
            var model = Services.PageContentService.GetPageByIdList(id);
            return PartialView("_DisplayLayoutItem", model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreatePageContent(Array deger, int pageid, Array order,DateTime PublishDate)
        {
            Services.PageContentService.CreatePageContent(deger, pageid, order,PublishDate);
            return RedirectToAction("GetPage");
        }

        public ActionResult GetPageContent()
        {
            var model = Services.PageContentService.GetPageContent();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public PartialViewResult ShowPageContent(int id)
        {
            var model = Services.PageContentService.GetPageContentList(id);
            return PartialView("_ShowPageContent", model);
        }
        public ActionResult EditPage()
        {
            var model = Services.PageContentService.GetPageContent();
            return View(model);
        }
        public PartialViewResult EditPageContent(int id)
        {
            var model = Services.PageContentService.EditPageContentList(id);
            return PartialView("_EditPageContent", model);
            
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateContent(Array deger, int pageid, Array order)
        {
            Services.PageContentService.UpdateContent(deger, pageid, order);
            return RedirectToAction("GetPage");
        }

    }
}