using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tao.Infrastructure.Core
{
    public abstract class BaseEntity
    {
        private Guid keyId;

        /// <summary>
        /// ORM层面的实体唯一标识，全系统唯一。必须被设置以及持久化。
        /// </summary>
        public Guid KeyId
        {
            get
            {
                return this.keyId;
            }
            set
            {
                if (this.keyId == null || Guid.Empty.Equals(this.keyId))
                {
                    this.keyId = value;
                }
                else
                {
                    throw new Exception("Can not change the key Id of an existing object.");
                }
            }
        }
    }
}
