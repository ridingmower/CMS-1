using CMS.Common.Models.Layout;
using CMS.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services.Interfaces
{
    public interface ILayoutService:IService
    {
        IEnumerable<LayoutDto> GetLayouts();
        LayoutDto GetLayoutById(int id);

        void RemoveItem(int id,int Order);
        List<PLItem> GetLayoutByIdList(int id);
        void InsertNewLayout(Array Liste, string name);
        List<PLItem> getLayoutItem(string name);
        void UpdateSize(int id, Array newSize,Array Order);
        IEnumerable<PageLayout> GetLayoutPage();
    }
}
