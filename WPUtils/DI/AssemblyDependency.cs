using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WPHubsUtil
{
    public static class AssemblyDependency
    {
        public static void AddAssemblyDependency(this IServiceCollection services, string assemblyName)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            Assembly assembly = Assembly.Load(assemblyName);
            List<Type> ts = assembly.GetTypes().Where(t => !t.IsInterface && !t.IsSealed && !t.IsGenericType).ToList().FindAll(p => !p.Name.ToLower().Contains("model"));
            foreach (var classitem in ts)
            {
                IEnumerable<Attribute> attrs = classitem.GetCustomAttributes();
                foreach (var at in attrs)
                {
                    if (at is UseDependencyInjectionAttribute)
                    {
                        UseDependencyInjectionAttribute attr = classitem.GetCustomAttribute(typeof(UseDependencyInjectionAttribute)) as UseDependencyInjectionAttribute;
                        List<Type> interfaceType = classitem.GetInterfaces().ToList();
                        if (interfaceType.Count == 0)
                        {
                            Type baseType = classitem.BaseType;
                            while (baseType != null)
                            {
                                if (baseType.IsAbstract)
                                {
                                    ServiceDescriptor serviceDescriptor = new ServiceDescriptor(baseType, classitem, attr.Lifetime);
                                    services.Add(serviceDescriptor);
                                    break;
                                }
                                else
                                {
                                    baseType = baseType.BaseType;
                                }
                            }
                        }
                        else
                        {
                            foreach (Type faceitem in interfaceType)
                            {
                                ServiceDescriptor serviceDescriptor = new ServiceDescriptor(faceitem, classitem, attr.Lifetime);
                                services.Add(serviceDescriptor);
                            }
                        }
                    }
                }
            }
        }
    }
}
