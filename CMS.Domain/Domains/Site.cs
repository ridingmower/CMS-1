using CMS.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Domains
{
    public class Site:BaseEntity
    {
        public string MaserLayout { get; set; }
        public string SiteName { get; set; }
        public string SiteFooter { get; set; }
    }
}
