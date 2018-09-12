using CMS.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services.Interfaces
{
    public interface ILoginService:IService
    {
        bool Register(User user);
        bool Login(User user);
        void Logout();
    }
}
