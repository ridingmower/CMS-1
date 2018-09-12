using CMS.Domain.Domains;
using CMS.Domain.SessionSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using static CMS.Core.Repository.AbstractBaseRepository<CMS.Domain.Domains.User>;
using static System.Collections.Specialized.BitVector32;

namespace CMS.Core.Services.Interfaces
{
    public class LoginService : BaseService, ILoginService
    {
        public bool Login(User user)
        {
            using (BaseRepository<User> _repo = new BaseRepository<User>())
            {
                if (user!=null )
                {
                    var result = _repo.Query<User>().Where(x => (x.Username == user.Username) && (x.Password == user.Password)).Any();
                    var getUser = _repo.Query<User>().Where(x => (x.Username == user.Username) && (x.Password == user.Password)).ToList();
                    if (result == true)
                    {
                        User _temp = new User();
                        foreach (var item in getUser)
                        {

                            _temp.Username = item.Username;
                            _temp.Password = item.Password;
                            _temp.Id = item.Id;
                            _temp.Name = item.Name;
                            _temp.yetki = item.yetki;
                        }

                    
                        SessionSet<User>.Set(_temp, "User");
                        
                        return true;
                    }
                    else
                    {
                        
                        return false;
                    }
                }
                else return false; 

            }
        }

        public bool Register(User user)
        {
            using (BaseRepository<User> _repo = new BaseRepository<User>())
            {
                if (user.Username != null && user.Password != null && user.Name != null)
                {
                    var result = _repo.Query<User>().Where(x => (x.Username == user.Username) && (x.Password == user.Password)).Any();
                    if (!result)
                    {
                        User newuser = new User()
                        {
                            CreateTime = DateTime.Now,
                            IsDeleted = false,
                            Name = user.Name,
                            Username = user.Username,
                            Password = user.Password,
                            yetki = 1
                        };
                        _repo.Add(newuser);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else return false;
            }
        }

        public void Logout()
        {
            
            
        }
    }
}
