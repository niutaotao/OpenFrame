using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tao.DAM.Request.User
{
    [DataContract]
    [Serializable]
    public class UserSearchRst:ServiceRequestBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public int Sex { get; set; }
    }
}
