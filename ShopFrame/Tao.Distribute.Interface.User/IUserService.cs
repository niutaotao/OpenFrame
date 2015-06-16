using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tao.DAM.Request.User;
using Tao.DAM.Response.User;

namespace Tao.Distribute.Interface.User
{

    [ServiceContract]
    interface IUserService
    {
        [OperationContract]
        UserInfoRsp AddUser(UserInfoRst request);
    }
}
