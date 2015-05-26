using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.IO;

namespace Tao.Infrastructure.Core
{
    /// <summary>
    /// Unity依赖注入抽象基类
    /// </summary>
    public abstract class UnityBuilder
    {
        /// <summary>
        /// 容器
        /// </summary>
        protected IUnityContainer Container;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        protected UnityBuilder()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configName">配置文件名称</param>
        /// <param name="sectionName">节点名称</param>
        /// <param name="containerName">容器名称</param>
        protected UnityBuilder(string configName, string sectionName, string containerName)
        {
            this.BuildFactory(configName, sectionName, containerName);
        }

        /// <summary>
        /// 根据配置文件，加载Unity 容器
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="sectionName"></param>
        /// <param name="containerName"></param>
        protected virtual void BuildFactory(string configName, string sectionName, string containerName)
        {
            try
            {
                this.Container = new UnityContainer();
                DirectoryInfo i = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                string configFilePath = Path.Combine(Path.Combine(i.Parent.FullName, ConfigurationManager.AppSettings["ConfigFilePath"]), configName);
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                map.ExeConfigFilename = configFilePath;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                UnityConfigurationSection unitySection = config.GetSection(sectionName) as UnityConfigurationSection;
                unitySection.Configure(this.Container, containerName);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定接口的默认配置的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        /// <summary>
        /// 根据register 名称加载接口实例
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="name">register 名称</param>
        /// <returns></returns>
        public virtual T Resolve<T>(string name)
        {
            return Container.Resolve<T>(name);
        }

        /// <summary>
        /// 根据register名称及构造函数的参数加载接口实例
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="name">register 名称</param>
        /// <param name="parameters">构造参数</param>
        /// <returns></returns>
        public virtual T Resolve<T>(string name, ResolverOverride parameters)
        {
            return Container.Resolve<T>(name, parameters);
        }

        /// <summary>
        /// 获取指定接口的默认配置的实例
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="parameters">构造参数</param>
        /// <returns></returns>
        public virtual T Resolve<T>(ResolverOverride parameters)
        {
            return Container.Resolve<T>(parameters);
        }

        /// <summary>
        /// 获取指定接口的所用配置的实例集合
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns>实例集合</returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return Container.ResolveAll<T>();
        }
    }
}
