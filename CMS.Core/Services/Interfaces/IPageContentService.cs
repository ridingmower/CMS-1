using CMS.Common.Models.Page;
using CMS.Common.Models.PageContent;
using CMS.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services.Interfaces
{
    public interface IPageContentService:IService
    {
        
        List<PLItem> GetPageByIdList(int id);
        void CreatePageContent(Array deger, int pageid, Array order,DateTime PublishDate);
        List<Page> GetPageContent();
        List<ContentDto> GetPageContentList(int id);
        List<ContentDto> EditPageContentList(int id);
        void UpdateContent(Array deger, int pageid, Array order);
    }
}
