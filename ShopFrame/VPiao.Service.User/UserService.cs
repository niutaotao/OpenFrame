using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.DAM.Dto;
using Tao.Infrastructure.Core.Repository;
using Tao.Repository.User.Repository;
using Tao.Service.Base;

namespace Tao.Service.User
{
    public class UserService : BaseUserService, IUserService
    {
        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="userInfo"></param>
        public void AddUser(UserInfo userInfo)
        {
            base.UserInfoRepository.Add(userInfo);
            var unit = base.OpenTransaction();
            unit.Commit();
        }
    }
}
