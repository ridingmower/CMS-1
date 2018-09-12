using CMS.Common.Models.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Common.Models.PageContent
{
    public class ContentDto
    {
        public int DivId { get; set; }
        public string Content { get; set; }
        public string Size { get; set; }
        public int PageID { get; set; }
        public string PageName { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
