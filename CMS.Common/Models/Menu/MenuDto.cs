using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Common.Models.Menu
{
    public class MenuDto
    {
        public string MenuName { get; set; }
        public int MenuId { get; set; }
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public int PageId { get; set; }
        public int SubPageId { get; set; }
    }
}
