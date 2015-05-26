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
    public class UserInfoRst : ServiceRequestBase
    {
        /// <summary>
        /// 用户keyId
        /// </summary>
        [DataMember]
        public Guid UserKeyId { get; set; }
    }
}
