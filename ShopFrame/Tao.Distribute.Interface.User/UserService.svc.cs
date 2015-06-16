using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.DAM.Request.User;
using Tao.DAM.Response.User;
using Tao.Service.Base;

namespace Tao.Distribute.Interface.User
{
    public class UserService : IUserService
    {
        private static readonly IUserService userService = ServiceFactory.Create<IUserService>();

        public UserInfoRsp AddUser(UserInfoRst request)
        {
            return userService.AddUser(request);
        }
    }
}
