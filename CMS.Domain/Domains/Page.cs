using CMS.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Domains
{
    public class Page:BaseEntity
    {
        public string PageName { get; set; }
        public string Slug { get; set; }
        public PageLayout PageLayout { get; set; }
        public int PageLayoutId { get; set; }
        public string meta { get; set; }
    }
}
