﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tao.DAM.Request
{
    [DataContract]
    [Serializable]
    public class RequestBase : ServiceRequestBase
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RequestBase()
        {
            this.PageIndex = 1;
            this.PageSize = 10;
        }

        /// <summary>
        /// 当前页
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }
    }
}
