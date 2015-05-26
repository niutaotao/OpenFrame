using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tao.DAM.Response
{
    [DataContract]
    [Serializable]
    public class ServiceReponseBase
    {
        private int _responseCode;
        private Exception _exception;
        private string _errorMsg;
        private string _serverIP;
        private double _usedMillionSecond;

        /// <summary>
        /// 构造
        /// </summary>
        public ServiceReponseBase()
        {
            _responseCode = -1;
            _exception = null;
            _errorMsg = null;
            _serverIP = null;
            _usedMillionSecond = 0;
        }

        /// <summary>
        /// 响应业务编码
        /// </summary>
        [DataMember]
        public int ResponseCode
        {
            get
            {
                return _responseCode;
            }
            set
            {
                _responseCode = value;
            }
        }

        /// <summary>
        /// 服务器IP
        /// </summary>
        [DataMember]
        public string ServerIP
        {
            get
            {
                return _serverIP;
            }
            set
            {
                _serverIP = value;
            }
        }

        /// <summary>
        /// 调用耗时（毫秒）
        /// </summary>
        [DataMember]
        public double UsedMillionSecond
        {
            get { return _usedMillionSecond; }
            set { _usedMillionSecond = value; }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        [DataMember]
        public string ErrorMsg
        {
            get
            {
                return _errorMsg;
            }
            set
            {
                _errorMsg = value;
            }
        }
    }
}
