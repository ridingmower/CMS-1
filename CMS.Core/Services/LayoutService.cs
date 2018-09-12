using CMS.Common.Models.Layout;
using CMS.Core.Services.Interfaces;
using CMS.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.Repository.AbstractBaseRepository<CMS.Domain.Domains.PageLayout>;

namespace CMS.Core.Services
{
    public class LayoutService : BaseService, ILayoutService
    {

        public LayoutDto GetLayoutById(int id)
        {
            using (BaseRepository<PageLayout> _repo = new BaseRepository<PageLayout>())
            {
                return _repo.Query<PageLayout>().Where(p => p.Id == id)
                    .Select(p => new LayoutDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Items = p.PLItems.Where(x => !x.IsDeleted).Select(x => new PLItemDto
                        {
                            Id = x.Id,
                            SizeValue = x.SizeValue,
                            SelectedColumn = x.SelectedColumn,
                            Order=x.Order
                        })
                    }).FirstOrDefault();
            }
        }
        
        public List<PLItem> GetLayoutByIdList(int id)
        {
            using (BaseRepository<PageLayout> _repo = new BaseRepository<PageLayout>())
            {
                var plist = _repo.Query<PLItem>().Where(p => p.PageLayoutId == id).ToList();

                return plist;

            }
        }


        public IEnumerable<LayoutDto> GetLayouts()
        {
            using (BaseRepository<PageLayout> _repo = new BaseRepository<PageLayout>())
            {
                return _repo.Query<PageLayout>().Select(p =>
                new LayoutDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Items = p.PLItems.Where(x => !x.IsDeleted).Select(
                        x => new PLItemDto
                        {
                            Id = x.Id,
                            SizeValue = x.SizeValue,
                            SelectedColumn = x.SelectedColumn,
                            Order=x.Order
                            
                        })
                }).ToList();
            }
        }


        public IEnumerable<PageLayout> GetLayoutPage()
        {
            using (BaseRepository<PageLayout> _repo = new BaseRepository<PageLayout>())
            {
                return _repo.Query<PageLayout>().ToList();
            }
        }


        public void InsertNewLayout(Array Liste, string name)
        {
            using (BaseRepository<PageLayout> _repo = new BaseRepository<PageLayout>())
            {
                if (_repo.Query<PageLayout>().Where(p => p.Name == name).Any())
                {

                }
                else
                {
                    PageLayout pl = new PageLayout();
                    pl.Name = name;
                    _repo.Add(pl);
                    using (BaseRepository<PLItem> _repo1 = new BaseRepository<PLItem>())
                    {
                        int i = 1;
                        foreach (var item in Liste)
                        {

                            PLItem pi = new PLItem();
                            pi.CreateTime = DateTime.Now;
                            pi.PageLayoutId = pl.Id;
                            pi.SizeValue = item.ToString();
                            pi.Order = i;
                            i++;
                            _repo1.Add(pi);
                        }

                    }
                }
            }

        }
        public List<PLItem> getLayoutItem(string name)
        {
            using (BaseRepository<PLItem> _repo1 = new BaseRepository<PLItem>())
            {
                var id = _repo1.Query<PageLayout>().Where(k => k.Name == name).Select(p => p.Id).First();
                var items = _repo1.Query<PLItem>().Where(k => k.PageLayoutId == id).ToList();
                return items;
            }

        }

        public void UpdateSize(int id, Array newSize,Array Order)
        {
            using (BaseRepository<PLItem> _repo1 = new BaseRepository<PLItem>())
            {
                var model = _repo1.Query<PLItem>().Where(p => p.PageLayoutId == id).ToList() ;
                int i = 0;
                foreach (var item in model)
                {
                    item.SizeValue = newSize.GetValue(i).ToString();
                    item.Order = Convert.ToInt32(Order.GetValue(i));
                    _repo1.UpdateLayout(item);
                    i++;
                }

            }
        }

        public void RemoveItem(int id,int Order)
        {
            using(BaseRepository<PLItem> _repo=new BaseRepository<PLItem>())
            {
                var model = _repo.Query<PLItem>().Where(x => x.Id == id).First();
                var pLItem = _repo.Query<PLItem>().Where(x => x.PageLayoutId == model.PageLayoutId).ToList();
                foreach (var item in pLItem)
                {
                    if (item.Order > model.Order)
                    {
                        item.Order--;
                    }
                }

                var page = _repo.Query<Page>().Where(x => x.PageLayoutId == model.PageLayoutId).ToList();
                foreach (var item in page)
                {
                    var pContent = _repo.Query<PageContent>().Where(p => (p.PageId == item.Id)&&(p.divs==Order)).ToList();
                    foreach (var item1 in pContent)
                    {
                        using (BaseRepository<PageContent> _repo1 = new BaseRepository<PageContent>())
                        {
                            _repo1.Delete(item1);
                        }
                    }
                }
                _repo.Delete(model);

            }
        }
    }
}
