using CMS.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Domains
{
    public class Menu:BaseEntity
    {
        public Page Page { get; set; }
        public int PageId { get; set; }
        public Menu MenU { get; set; }
        public int? ParentId { get; set; }
        public string MenuName { get; set; }
    }
}
