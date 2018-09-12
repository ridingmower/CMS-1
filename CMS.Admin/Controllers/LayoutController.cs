using CMS.Common.Models.Layout;
using CMS.Core.Services;
using CMS.Domain.Domains;
using CMS.Domain.SessionSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class LayoutController : Controller
    {

        User me = SessionSet<User>.Get("User");
        #region Layout
        // GET: Layout
        public ActionResult List()
        {
            if (me != null)
            {
                var model = Services.LayoutService.GetLayouts();
                return View("PageLayout", model);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }


        public PartialViewResult GetLayoutItems()
        {

            var model = TempData.Peek("ListPLItem") as List<PLItem>;
            return PartialView("_GetLayoutItemsPartial", model);

        }
        #endregion

        #region Insert
        public ViewResult Insert()
        {

            var model = new LayoutDto();
            TempData["InsertModel"] = model;
            return View(model);


        }

        public PartialViewResult GetNewItem()
        {
            var model = TempData.Peek("InsertModel") as LayoutDto;
            return PartialView();
        }
        public JsonResult AddNewItem(PLItemDto dto)
        {
            var model = TempData.Peek("InsertModel") as LayoutDto;
            var List = model.Items.ToList();
            List.Add(dto);
            model.Items = List;
            return Json(new { IsSuccess = true });
        }
        public ActionResult InsertNew(string Name, Array Liste)
        {
            Services.LayoutService.InsertNewLayout(Liste, Name);
            var list = Services.LayoutService.getLayoutItem(Name);
            TempData["ListPLItem"] = list;
            return RedirectToAction("Insert");
        }

        #endregion

        #region Update
        public ViewResult Update(int id)
        {
            var model = Services.LayoutService.GetLayoutById(id);
            return View(model);
        }
        public ActionResult UpdateSize(int id, Array newSize, Array Order)
        {
            Services.LayoutService.UpdateSize(id, newSize, Order);
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult GetSize()
        {
            PLItemDto pdt = new PLItemDto();
            var Value = pdt.Sizes.ToList();
            return Json(Value);
        }

        [HttpPost]
        public ActionResult RemoveItem(int id, int Order)
        {
            Services.LayoutService.RemoveItem(id, Order);
            return RedirectToAction("List");
        }
        #endregion


    }
}