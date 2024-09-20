using System;
using System.Collections.Generic;
using UnityEngine;

namespace DependEasy
{
    internal static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new();

        internal static void RegisterService(Type type, object service)
        {
            if (!type.IsInstanceOfType(service))
                throw new Exception(
                    $"Cannot register object of type {service.GetType().Name} as service of type {type.Name}");
            if (type.IsValueType) throw new Exception($"Cannot register value type as service: {type.Name}");
            if (services.ContainsKey(type)) Debug.LogWarning($"Service of type: {type.Name} is already created!");
            services[type] = service;
        }

        internal static object RequestService(Type type)
        {
            if (services.TryGetValue(type, out object service)) return service;
            Debug.LogError($"Service of type [{type.FullName}] hasn't been registered!");
            return null;
        }

        internal static void Clear()
        {
            services.Clear();
        }
    }
}