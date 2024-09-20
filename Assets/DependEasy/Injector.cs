using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DependEasy
{
	internal static class Injector
	{
		private const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

		internal static void FillDeps(object obj)
		{
			var fields = GetAllFields(obj.GetType());
			foreach (var field in fields) InjectFieldRef(obj, field);
		}

		private static void InjectFieldRef(object service, FieldInfo field)
		{
			var atts = field.GetCustomAttributes();
			if (atts.All(a => a is not InjectAttribute)) return;
			field.SetValue(service, ServiceLocator.RequestService(field.FieldType));
		}

		private static IEnumerable<FieldInfo> GetAllFields(Type t)
		{
			if (t == null || t == typeof(MonoBehaviour))
				return Enumerable.Empty<FieldInfo>();

			return t.GetFields(flags).Concat(GetAllFields(t.BaseType));
		}
	}
}