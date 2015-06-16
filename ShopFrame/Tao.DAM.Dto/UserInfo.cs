using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tao.Infrastructure.Core;

namespace Tao.DAM.Dto
{
    [DataContract]
    [Serializable]
    public class UserInfo:BaseEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Required]
        [DataMember]
        public string UserCode { get; set; }
        
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// 性别
        /// 0-未填，1-男，2-女
        /// </summary>
        [DataMember]
        public int Sex { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DataMember]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
    }
}
