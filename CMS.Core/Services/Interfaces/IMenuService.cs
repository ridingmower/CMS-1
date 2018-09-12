using CMS.Common.Models.Menu;
using CMS.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services.Interfaces
{
    public interface IMenuService
    {
        List<Menu> MenuList();
        List<Page> PageList();
        void CreateMenu(int id);
        void SetSubmenu(int pageid, int subpageid);
        void RemoveMenu(int id);
        List<MenuDto> GetMenuList();
        List<Menu> GetSubMenu(int id);
        string GetMenuRecursion();
        void AddChildItem(Menu childItem, StringBuilder strBuilder);
        void MenuUpdateName(int id, string Name);
    }
}
