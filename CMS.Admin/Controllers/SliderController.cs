using CMS.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class SliderController : Controller
    {
        // GET: Slider
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file,string Title,bool IsActive)
        {
            var filename = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/SliderImage"), filename);
            file.SaveAs(path);
            Services.SliderService.Create(filename, Title, IsActive);
            return RedirectToAction("Index");
        }
        public ActionResult ImageList()
        {
            var model = Services.SliderService.ImageList();
            return PartialView(model);
        }
        public ActionResult Activate(int id)
        {
            Services.SliderService.Activate(id);
            return RedirectToAction("Index");
        }
        public ActionResult Preview()
        {
            var model=Services.SliderService.ActiveImage();
            return View(model);
        }
        public ActionResult Delete(int id)
        {
             Services.SliderService.DeleteImage(id);
            return RedirectToAction("Index");
        }
    }
}