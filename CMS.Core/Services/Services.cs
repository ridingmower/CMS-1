using CMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services
{
    public class Services
    {
        public static ILayoutService LayoutService
        {
            get { return (ILayoutService)new LayoutService(); }
        }

        public static IPageService PageService
        {
            get { return (IPageService)new PageService(); }
        }
        public static IPageContentService PageContentService
        {
            get { return (IPageContentService)new PageContentService(); }
        }
        public static IMenuService MenuService
        {
            get { return (IMenuService)new MenuService(); }
        }
        public static ILoginService LoginService
        {
            get { return (ILoginService)new LoginService(); }
        }
        public static ISliderService SliderService
        {
            get { return (ISliderService)new SliderService(); }
        }
    }
}
