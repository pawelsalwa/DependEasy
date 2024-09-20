using UnityEngine;

namespace DependEasy
{
	/// <summary>
	/// Use it instead of Object.Instantiate() to inject dependancies to objects created at runtime. 
	/// </summary>
	public static class Factory
	{
		public static Object Instantiate(Object obj, Transform parent)
		{
			switch (obj)
			{
				case Behaviour com:
				{
					var cache = com.gameObject.activeSelf;
					com.gameObject.SetActive(false);
					var ret = Object.Instantiate(com, parent);
					FillRefs(ret.gameObject);
					com.gameObject.SetActive(cache);
					ret.gameObject.SetActive(cache);
					return ret;
				}
				case GameObject go:
				{
					var cache = go.activeSelf;
					go.SetActive(false);
					var ret = Object.Instantiate(go, parent);
					FillRefs(ret);
					go.SetActive(cache);
					ret.SetActive(cache);
					return ret;
				}
				default: return Object.Instantiate(obj);
			}
		}

		public static T Instantiate<T>(T original) where T : Object
			=> Instantiate(original as Object, null) as T;

		public static T Instantiate<T>(T original, Transform parent) where T : Object
			=> Instantiate(original as Object, parent) as T;

		public static T AddComponent<T>(GameObject gameObject) where T : Component
		{
			var cache = gameObject.activeSelf;
			gameObject.SetActive(false);
			var comp = gameObject.AddComponent<T>();
			Injector.FillDeps(comp);
			gameObject.SetActive(cache);
			return comp;
		}

		private static void FillRefs(GameObject go)
		{
			foreach (var mb in go.GetComponentsInChildren<MonoBehaviour>(true))
				Injector.FillDeps(mb);
		}
	}
}