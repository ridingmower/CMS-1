using CMS.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Domains
{
    public class PLItem:BaseEntity
    {
        public int Order { get; set; }
        public int SelectedColumn { get; set; }
        public string SizeValue { get; set; }
        public PageLayout PageLayout{ get; set; }
        public int PageLayoutId { get; set; }
    }
}
