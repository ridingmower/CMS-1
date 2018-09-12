using CMS.Common.Models.PageContent;
using CMS.Core.Services;
using CMS.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMS.Admin.Controllers
{
    public class CmsApiController : ApiController
    {
        // GET: api/CmsApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CmsApi/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public string GetMenu()
        {
            var menuList= Services.MenuService.GetMenuRecursion();
            return menuList;
        }

        // POST: api/CmsApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CmsApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CmsApi/5
        public void Delete(int id)
        {
        }
        [HttpGet]
        public List<ContentDto> ShowPage(int id)
        {
            var contentList = Services.PageContentService.GetPageContentList(id);
            return contentList;
        }
        [HttpGet]
        public List<Slider> GetSlider()
        {
            var slide = Services.SliderService.ActiveImage();
            return slide;
        }

    }
}
