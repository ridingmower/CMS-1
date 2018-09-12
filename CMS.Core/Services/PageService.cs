using CMS.Core.Services.Interfaces;
using CMS.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.Repository.AbstractBaseRepository<CMS.Domain.Domains.Page>;

namespace CMS.Core.Services
{
    public class PageService:BaseService,IPageService
    {
        public List<Page> GetPageList()
        {
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {
                var pageList = _repo.Query<Page>().ToList();
                return pageList;
            }
        }
        public void CreatePage(string name, int id)
        {
            using (BaseRepository<Page> _repo1 = new BaseRepository<Page>())
            {
                if (_repo1.Query<Page>().Where(p => p.PageName == name).Any())
                {

                }
                else
                {
                    Page p = new Page()
                    {
                        PageName = name,
                        Slug = "/" + name,
                        CreateTime = DateTime.Now,
                        PageLayoutId = id,
                        IsDeleted = false
                    };
                    _repo1.Add(p);
                }
            }
        }
    }
}
