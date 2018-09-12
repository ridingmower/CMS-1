using CMS.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Domains
{
    public class PageContent:BaseEntity
    {
        public Page Page { get; set; }
        public int PageId { get; set; }
        public string Content { get; set; }
        public int divs { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsPublish { get; set; }
    }
}
