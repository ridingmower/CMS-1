using CMS.Common.Models.Layout;
using CMS.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Common.Models.Page
{
    public class PageDto
    {
        public int PageLayoutId { get; set; }
        public IEnumerable<PLItemDto> Items { get; set; }
        public string Pagename { get; set; }
    }
}
