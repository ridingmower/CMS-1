using CMS.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Domains
{
    public class PageLayout:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<PLItem> PLItems { get; set; }
    }
}
