using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tao.DAM.Request
{
    [DataContract]
    [Serializable]
    public class ServiceRequestBase
    {
        private string _clientIp;
        private int _businessCode;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceRequestBase()
        {
            _clientIp = null;
            _businessCode = -1;
        }

        /// <summary>
        /// 业务编码
        /// </summary>
        [DataMember]
        public int BusinessCode
        {
            get
            {
                return _businessCode;
            }
            set
            {
                _businessCode = value;
            }
        }

        /// <summary>
        /// 客户端IP
        /// </summary>
        [DataMember]
        public int ClientIp
        {
            get
            {
                return _businessCode;
            }
            set
            {
                _businessCode = value;
            }
        }
    }
}
