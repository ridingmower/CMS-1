using CMS.Common.Models.Layout;
using CMS.Common.Models.Page;
using CMS.Common.Models.PageContent;
using CMS.Core.Services.Interfaces;
using CMS.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.Repository.AbstractBaseRepository<CMS.Domain.Domains.PageContent>;

namespace CMS.Core.Services
{
    public class PageContentService : BaseService, IPageContentService
    {
        public void CreatePageContent(Array deger, int pageid, Array order, DateTime PublishDate)
        {
            using (BaseRepository<PageContent> _repo = new BaseRepository<PageContent>())
            {
                if (_repo.Query<PageContent>().Where(x => x.PageId == pageid).ToList().Any())
                {
                    int i = 0;
                    var model = _repo.Query<PageContent>().Where(p => p.PageId == pageid).ToList();
                    foreach (var item in model)
                    {
                        item.divs = Convert.ToInt32(order.GetValue(i));
                        item.Content = deger.GetValue(i).ToString();
                        item.CreateTime = DateTime.Now;
                        if (item.PublishDate < DateTime.Now)
                        {
                            item.PublishDate = PublishDate;
                            item.IsPublish = true;
                        }
                        else
                        {
                            item.PublishDate = PublishDate;
                            item.IsPublish = false;
                        }
                        _repo.UpdateContent(item);
                        i++;
                    }
                }
                else
                {
                    int i = 0;
                    foreach (var item in deger)
                    {

                        PageContent pc = new PageContent()
                        {
                            Content = item.ToString(),
                            IsDeleted = false,
                            CreateTime = DateTime.Now,
                            PageId = pageid,
                            Page = _repo.Query<Page>().Where(x => x.Id == pageid).First(),
                            divs = Convert.ToInt32(order.GetValue(i)),
                            PublishDate = PublishDate,
                            IsPublish = false,
                        };
                        if (pc.PublishDate < DateTime.Now)
                        {
                            pc.PublishDate = PublishDate;
                            pc.IsPublish = true;
                        }

                        i++;
                        _repo.Add(pc);


                    }
                }
            }
        }


        public List<ContentDto> EditPageContentList(int id)
        {
            using (BaseRepository<PageContent> _repo = new BaseRepository<PageContent>())
            {
                var page = _repo.Query<Page>().Where(p => p.Id == id).First();
                var layout = _repo.Query<PLItem>().Where(p => p.PageLayoutId == page.PageLayoutId);


                return _repo.Query<PageContent>().Where(p => (p.PageId == page.Id))
                    .Select(p => new ContentDto()
                    {
                        Content = p.Content,
                        DivId = p.divs,
                        PageID = page.Id,
                        PageName = page.PageName,
                        Size = layout.Where(x => x.Order == p.divs).Select(x => x.SizeValue).FirstOrDefault(),
                        PublishDate = p.PublishDate

                    }).ToList();

            }
        }

    
        public List<PLItem> GetPageByIdList(int id)
        {
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {
                var layoutid = _repo.Query<Page>().Where(p => p.Id == id).Select(p => p.PageLayoutId).First();
                var itemlist = _repo.Query<PLItem>().Where(p => p.PageLayoutId == layoutid).ToList();

                return itemlist;

            }
        }

        public List<Page> GetPageContent()
        {
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {
                var model = _repo.Query<Page>().ToList();
                return model;
            }
        }
        public List<ContentDto> GetPageContentList(int id)
        {
            using (BaseRepository<PageContent> _repo = new BaseRepository<PageContent>())
            {
                var page = _repo.Query<Page>().Where(p => p.Id == id).First();
                var layout = _repo.Query<PLItem>().Where(p => p.PageLayoutId == page.PageLayoutId);

                var pagecontent = _repo.Query<PageContent>().ToList();
                foreach (var item in pagecontent)
                {
                    if (item.PublishDate < DateTime.Now)
                    {
                        item.IsPublish = true;
                        _repo.Update(item);
                    }
                }


                return _repo.Query<PageContent>().Where(p => (p.PageId == page.Id) && (p.IsPublish == true))
                    .Select(p => new ContentDto()
                    {
                        Content = p.Content,
                        DivId = p.divs,
                        PageID = page.Id,
                        PageName = page.PageName,
                        Size = layout.Where(x => x.Order == p.divs).Select(x => x.SizeValue).FirstOrDefault()

                    }).ToList();

            }
        }

        public void UpdateContent(Array deger, int pageid, Array order)
        {
            using (BaseRepository<PageContent> _repo = new BaseRepository<PageContent>())
            {
                int i = 0;
                var model = _repo.Query<PageContent>().Where(p => p.PageId == pageid).ToList();
                foreach (var item in model)
                {
                    item.divs = Convert.ToInt32(order.GetValue(i));
                    item.Content = deger.GetValue(i).ToString();
                    item.CreateTime = DateTime.Now;
                    if (item.PublishDate < DateTime.Now)
                    {
                       
                        item.IsPublish = true;
                    }
                    else
                    {
                        
                        item.IsPublish = false;
                    }
                    _repo.UpdateContent(item);
                    i++;
                }
            }
        }
    }
}
