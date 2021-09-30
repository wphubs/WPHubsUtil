using Microsoft.Extensions.DependencyInjection;
using System;

namespace WPHubsUtil
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class UseDependencyInjectionAttribute : Attribute
    {

        public ServiceLifetime lifetime;

        /// <summary>
        /// 生命周期，瞬时注入 
        /// </summary>
        /// <param name="argLifetime"></param>
        public UseDependencyInjectionAttribute(ServiceLifetime argLifetime = ServiceLifetime.Transient)
        {
            lifetime = argLifetime;
        }

        public ServiceLifetime Lifetime
        {
            get
            {
                return this.lifetime;
            }
        }
    }
}
