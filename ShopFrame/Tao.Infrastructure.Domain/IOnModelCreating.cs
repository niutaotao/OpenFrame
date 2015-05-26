using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Tao.Infrastructure.Core
{
    /// <summary>
    /// 模块注册EF entity mapping接口
    /// </summary>
    public interface IOnModelCreating
    {
        /// <summary>
        /// 注册实体映射配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        void OnModelCreating(DbModelBuilder modelBuilder);
    }
}
