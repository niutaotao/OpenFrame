using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using Tao.Infrastructure.Core.UnitOfWork;

namespace Tao.Infrastructure.Core.IOC
{
    /// <summary>
    /// ToDo：加说明
    /// </summary>
    public sealed class BuilderFactory
    {
        // 定义一个Dummy私有类，将抽象类UnityBuilder具体化
        private class MyBuilder : UnityBuilder
        {
            //private const string configName = "AppUnitOfWork.config";
            private const string section = "unity";
            private const string container = "registers";

            public MyBuilder() : base(ConfigurationManager.AppSettings["AppUnitOfWorkConfig"].ToString(), section, container) { }
        }

        // 自有的Builder，用以通过配置创建对象。
        private static MyBuilder myBuilder = new MyBuilder();

        /// <summary>
        /// 创建ThreadLocal变量，用以在每个线程Context里存放各自的IUnitOfWork对象。
        /// 各个线程私有的的IUnitOfWork将通过电泳CreateUnitOfWork()进行初始化。
        /// </summary>
        private static ThreadLocal<IUnitOfWork> appUnitOfWorkThreadLocal = new ThreadLocal<IUnitOfWork>(() => CreateUnitOfWork());

        /// <summary>
        /// 返回ThreadLocal变量里存放的当前线程的IUnitOfWork对象
        /// </summary>
        /// <returns>IUnitOfWork</returns>
        public static IUnitOfWork GetAppUnitOfWorkRepository()
        {
            return GetAppUnitOfWork();
        }

        private static IUnitOfWork GetAppUnitOfWork()
        {
            const string name = "DbContext";
            object data = CallContext.GetData(name);

            if (data == null || ((IUnitOfWork)data).IsDisposed())
            {
                IUnitOfWork appUnitOfWork = CreateUnitOfWork();
                CallContext.SetData(name, appUnitOfWork);
                return appUnitOfWork;
            }

            return (IUnitOfWork)data;
        }


        /// <summary>
        /// 根据配置创建IUnitOfWork对象
        /// </summary>
        /// <returns></returns>
        private static IUnitOfWork CreateUnitOfWork()
        {
            IUnitOfWork ret = myBuilder.Resolve<IUnitOfWork>();
            HttpContextManager.AddValue(ret);
            return ret;
        }

        /// <summary>
        /// AppUnitOfWork调用此方法获得ORM的配置对象
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IOnModelCreating> CreateAllOnModelCreatings()
        {
            return myBuilder.ResolveAll<IOnModelCreating>();
        }

        /// <summary>
        /// 尝试释放 IUnitOfWork 对象，如果存在的话
        /// add by fengyan 
        /// </summary>
        /// <returns>返回false 代表当前线程不存在 IUnitOfWork 对象 无须释放</returns>
        public static bool TryDisposeIfOpenTransaction()
        {
            if (appUnitOfWorkThreadLocal.Value == null || appUnitOfWorkThreadLocal.Value.IsDisposed())
            {
                return false;//无须释放
            }
            if (!appUnitOfWorkThreadLocal.Value.HasNoneDisposeTransaction())
            {
                //不存在开启的事物
                return false;
            }
            //释放
            appUnitOfWorkThreadLocal.Value.Dispose();
            return true;
        }
    }
}
