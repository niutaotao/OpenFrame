using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.Web;

namespace Tao.Infrastructure.Core
{
    public class HttpContextManager
    {
        public static void AddValue(object newValue)
        {
            string typeName = newValue.GetType().AssemblyQualifiedName;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(typeName))
                {
                    HttpContext.Current.Items[typeName] = newValue;
                }
                else
                {
                    HttpContext.Current.Items.Add(typeName, newValue);
                }
            }
        }
        public static object GetValue(string name)
        {
            string typeName = name;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(typeName))
                {
                    return HttpContext.Current.Items[typeName];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpContextLifetimeManager<T> : LifetimeManager, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object GetValue()
        {
            return HttpContext.Current.Items[typeof(T).AssemblyQualifiedName];
        }

        /// <summary>
        /// 
        /// </summary>
        public override void RemoveValue()
        {
            var value = GetValue();
            IDisposable disposableValue = value as IDisposable;

            if (disposableValue != null)
            {
                disposableValue.Dispose();
            }
            HttpContext.Current.Items.Remove(typeof(T).AssemblyQualifiedName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newValue"></param>
        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[typeof(T).AssemblyQualifiedName] = newValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            RemoveValue();
        }
    }
}
