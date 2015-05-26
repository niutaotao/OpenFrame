using System;
using System.Collections.Generic;
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
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public int Sex { get; set; }

        [DataMember]
        public string Mobile { get; set; }
    }
}
