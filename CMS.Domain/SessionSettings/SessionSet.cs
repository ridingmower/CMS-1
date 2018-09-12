using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.Domain.SessionSettings
{
    public class SessionSet<T> where T:class
    {
        public static T CurrentUser(string key)
        {
            return Get(key);
        }
        public static void Set(T model, string key)
        {
            HttpContext.Current.Session[key] = model;
            HttpContext.Current.Session.Timeout = 60;
        }
        public static T Get(string key)
        {
            var a = (T)HttpContext.Current.Session[key];
            return a;
        }
        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
    }
}
