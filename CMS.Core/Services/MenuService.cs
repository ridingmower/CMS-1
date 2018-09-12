using CMS.Common.Models.Menu;
using CMS.Core.Services.Interfaces;
using CMS.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using static CMS.Core.Repository.AbstractBaseRepository<CMS.Domain.Domains.Menu>;

namespace CMS.Core.Services
{
    public class MenuService : BaseService, IMenuService
    {

        public void CreateMenu(int id)
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                if (_repo.Query<Menu>().Where(p => (p.PageId == id)&&(p.IsDeleted==false)).ToList().Any() == false)
                {
                    var Page1 = _repo.Query<Page>().Where(p => p.Id == id).First();
                    if (_repo.Query<Menu>().Where(x => x.PageId == Page1.Id).Any())
                    {
                        var remove = _repo.Query<Menu>().Where(x => x.PageId == Page1.Id).ToList();
                        foreach (var item in remove)
                        {
                            _repo.Delete(item);
                        }
                    }

                    Menu menu = new Menu()
                    {
                        IsDeleted = false,
                        PageId = id,
                        Page = Page1,
                        MenuName=Page1.PageName
                    };
                    _repo.Add(menu);
                }

            }
        }

        public List<MenuDto> GetMenuList()
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                var menuList = _repo.Query<Menu>().Where(x=>x.IsDeleted==false).ToList();
                List<MenuDto> list = new List<MenuDto>();

                foreach (var item in menuList)
                {
                    var SubMenuList = _repo.Query<Menu>().Where(x => x.ParentId == item.Id).ToList();
                    var page = _repo.Query<Page>().Where(x => x.Id == item.PageId).First();


                    MenuDto dt = new MenuDto()
                    {
                        MenuName = item.MenuName,
                        MenuId = item.Id,
                        PageId = item.PageId,

                    };
                    if (SubMenuList.Count() != 0)
                    {

                        foreach (var item1 in SubMenuList)
                        {
                            var subPage = _repo.Query<Page>().Where(x => x.Id == item1.PageId).First();
                            dt.SubMenuId = item1.Id;
                            dt.SubMenuName = item1.MenuName;
                            dt.SubPageId = subPage.Id;
                        }

                    }
                    list.Add(dt);
                }
                return list;
            }

        }

        public List<Menu> MenuList()
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                var model = _repo.Query<Menu>().Where(x=>x.IsDeleted==false).ToList();
                List<Menu> menu=new List<Menu>();
                foreach (var item in model)
                {
                    var page = _repo.Query<Page>().Where(x => x.Id == item.PageId).First();
                    item.Page = page;
                    menu.Add(item);
                }
                return menu;
                
            }
        }


        public List<Menu> GetSubMenu(int id)
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                var SubMenu = _repo.Query<Menu>().Where(x => x.ParentId == id).ToList();
                List<Menu> menu = new List<Menu>();
                foreach (var item in SubMenu)
                {
                    var page = _repo.Query<Page>().Where(x => x.Id == item.PageId).First();
                    item.Page = page;
                    menu.Add(item);
                }
                return menu;
            }
        }

        public List<Page> PageList()
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {

                var pageList = _repo.Query<Page>().ToList();
                var menuList = _repo.Query<Menu>().Where(x => x.IsDeleted == false).ToList();
                var NotMenuList = (from p in pageList where !(from m in menuList select m.PageId).Contains(p.Id) select p).ToList();

                return NotMenuList;
            }
        }

        public void MenuUpdateName(int id,string Name)
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {

                var Menu = _repo.Query<Menu>().Where(x => x.PageId == id).First();
                Menu.MenuName = Name;
                _repo.UpdateMenuName(Menu);
                
            }
        }


        public void RemoveMenu(int id)
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                bool result = _repo.Query<Menu>().Where(x => x.Id == id).Any();
                if (result)
                {
                    var Menu = _repo.Query<Menu>().Where(x => x.Id == id).First();

                    var SubMenu = _repo.Query<Menu>().Where(x => x.ParentId == Menu.Id).ToList();

                    Menu.IsDeleted = true;
                    _repo.Update(Menu);

                }
            }
        }

        public void SetSubmenu(int pageid, int subpageid)
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {

                if (pageid != subpageid)
                {

                    if (_repo.Query<Menu>().Where(x => (x.PageId == subpageid) && (x.IsDeleted == false)).Any() == false)
                    {
                        var SubMenu = _repo.Query<Menu>().Where(x => x.PageId == pageid).First();
                        var SubPage = _repo.Query<Page>().Where(x => x.Id == subpageid).First();
                        Menu submenu = new Menu()
                        {
                            PageId = subpageid,
                            CreateTime = DateTime.Now,
                            IsDeleted = false,
                            ParentId = SubMenu.Id,
                            MenU = SubMenu,
                            Page = SubPage,
                            MenuName=SubPage.PageName
                        };
                        _repo.Add(submenu);
                    }

                    var SubMenu1 = _repo.Query<Menu>().Where(x => (x.PageId == subpageid) && (x.IsDeleted == false)).First();
                    var Menu1 = _repo.Query<Menu>().Where(x => (x.PageId == pageid) && (x.IsDeleted == false)).First();
                    if (SubMenu1.Id != Menu1.Id)
                    {
                        bool result = _repo.Query<Menu>().Where(x => x.PageId == pageid).Any();
                        if (result)
                        {
                            var submenu = _repo.Query<Menu>().Where(x => (x.PageId == subpageid) && (x.IsDeleted == false)).First();

                            submenu.ParentId = Menu1.Id;
                            submenu.MenU = Menu1;
                            _repo.Update(submenu);

                        }
                        else
                        {
                            var SubPage = _repo.Query<Page>().Where(x => x.Id == subpageid).First();
                            var Page = _repo.Query<Page>().Where(x => x.Id == pageid).First();
                            Menu submenu = new Menu()
                            {
                                PageId = subpageid,
                                CreateTime = DateTime.Now,
                                IsDeleted = false,
                                Page = SubPage,
                                MenuName=SubPage.PageName

                            };
                            _repo.Add(submenu);
                            var SubMenu2 = _repo.Query<Menu>().Where(x => (x.PageId == submenu.Id) && (x.IsDeleted == false)).First();
                            Menu menu = new Menu()
                            {
                                PageId = pageid,
                                CreateTime = DateTime.Now,
                                IsDeleted = false,
                                MenU = submenu,
                                ParentId = submenu.ParentId,
                                Page = Page,
                                MenuName=Page.PageName
                            };

                            _repo.Add(menu);


                        }
                    }


                }
                else
                {

                }

            }
        }


        public string GetMenuRecursion()
        {
            using (BaseRepository<Menu> repo = new BaseRepository<Menu>())
            {
                var all = repo.Query<Menu>().Where(x => !x.IsDeleted).ToList();
                var strBuilder = new StringBuilder();
                var parentItems = all.Where(x => x.ParentId == null).ToList();
                
                strBuilder.Append("<header><nav><ul> ");
                foreach (var parentcat in parentItems)
                {
                    var childItems = all.Where(x => x.ParentId == parentcat.Id);
                    if (childItems.Count() > 0)
                    {
                       
                        var page = repo.Query<Page>().Where(k => k.Id == parentcat.PageId).FirstOrDefault();
                        strBuilder.Append("<li><a href='/Home/Show/"+parentcat.PageId+ "'><i class='fa fa-navicon'></i>" + page.PageName+"</a>");
                        AddChildItem(parentcat, strBuilder);
                        strBuilder.Append("</li>");
                    }
                    else
                    {
                        
                        var page = repo.Query<Page>().Where(k => k.Id == parentcat.PageId).FirstOrDefault();
                        strBuilder.Append("<li><a href='/Home/Show/"+parentcat.PageId+ "' ><i class='fa fa-briefcase'></i> " + page.PageName + "</a></li>");
                    }
                }
                strBuilder.Append("</ul></nav></header>");

                return strBuilder.ToString();
            }

        }

        public void AddChildItem(Menu childItem, StringBuilder strBuilder)
        {
            using (BaseRepository<Menu> repo = new BaseRepository<Menu>())
            {
                var all = repo.Query<Menu>().Where(x => !x.IsDeleted).ToList();
                
                strBuilder.Append("<ul>");
                var childItems = all.Where(x => x.ParentId == childItem.Id);
                foreach (Menu cItem in childItems)
                {
                    var subChilds = all.Where(x => x.ParentId == cItem.Id);
                    if (subChilds.Count() > 0)
                    {
                        var page = repo.Query<Page>().Where(k => k.Id == cItem.PageId).FirstOrDefault();
                        strBuilder.Append(" <li><a href='/Home/Show/"+cItem.PageId+"'>"+page.PageName+ " <i class='fa fa-angle-double-right position-expand-arrow' ></i></a>");
                        AddChildItem(cItem, strBuilder);
                        strBuilder.Append("</li>");
                    }
                    else
                    {
                        var page = repo.Query<Page>().Where(k => k.Id == cItem.PageId).FirstOrDefault();
                        strBuilder.Append("<li><a href='/Home/Show/" + cItem.PageId + "'><i class='fa fa-bullseye'></i> " + page.PageName + "</a></li>");

                    }

                }

                strBuilder.Append("</ul>");
            }
        }


    }
}

