using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Repository.User;

namespace Tao.Service.Base
{
    public class BaseService
    {
        private IUserInfoRepository userInfoRepository = null;

        public IUserInfoRepository UserInfoRepository
        {
            get
            {
                if (userInfoRepository == null)
                {
                    userInfoRepository = new UserInfoRepository();
                }

                return UserInfoRepository;
            }
        }
    }
}
