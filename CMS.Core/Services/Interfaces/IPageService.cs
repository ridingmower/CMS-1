using CMS.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services.Interfaces
{
    public interface IPageService:IService
    {
        List<Page> GetPageList();
        void CreatePage(string name, int id);
    }
}
