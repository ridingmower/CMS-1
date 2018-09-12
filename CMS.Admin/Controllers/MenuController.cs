using CMS.Common.Models.Menu;
using CMS.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult MenuIndex()
        {
            var model = Services.MenuService.PageList();
            return View(model);
        }
        public ActionResult CreateMenu(int id)
        {
            Services.MenuService.CreateMenu(id);
            return RedirectToAction("MenuIndex");
        }
        [HttpPost]
        public ActionResult CreateSubMenu(int pageid, int subpageid)
        {
            Services.MenuService.SetSubmenu(pageid, subpageid);
            return RedirectToAction("MenuIndex");
        }
        [HttpPost]
        public ActionResult RemoveMenu(int id)
        {
            Services.MenuService.RemoveMenu(id);
            return RedirectToAction("MenuIndex");
        }
        
        public ActionResult GetMenuList()
        {
            var model = Services.MenuService.GetMenuList();
            return Json(model);
        }
        public ActionResult MenuView()
        {
            var model = Services.MenuService.GetMenuList();
            return View(model);
        }

        public ActionResult GetSubMenu(int id)
        {
            var model = Services.MenuService.GetSubMenu(id);
            return View(model);
        }
        public ActionResult Index()
        {
            var menuUi = Services.MenuService.GetMenuRecursion();
            ViewBag.Menu = menuUi;
            return View();
        }
        public ActionResult UpdateMenuName(int id,string Name)
        {
            Services.MenuService.MenuUpdateName(id,Name);
           
            return RedirectToAction("MenuIndex");
        }


    }
}