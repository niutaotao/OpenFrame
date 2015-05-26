using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.DAM.Dto;
using Tao.Service.Base;

namespace Tao.Service.User
{
    public class UserService:BaseService
    {
        public void AddUser(UserInfo userInfo)
        {
             base.UserInfoRepository.Add(userInfo);
        }
    }
}
