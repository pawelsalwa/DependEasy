using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DependEasy.Internal
{
	internal static class InitScenes
	{
		internal static void ForceReinit() => InitAlreadyLoadedScenes();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			SceneManager.sceneLoaded -= InitScene;
			SceneManager.sceneLoaded += InitScene;
		}
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitAlreadyLoadedScenes()
        {
	        ServiceLocator.Clear();
	        RegisterSerializedServices();
	        for (int i = 0; i < SceneManager.sceneCount; i++) 
		        InjectReferences(SceneManager.GetSceneAt(i));
        }

        private static void InitScene(Scene scene, LoadSceneMode mode)
        {
	        RegisterSerializedServices();
	        InjectReferences(scene);
        } 
        
        private static void RegisterSerializedServices()
        {
	        var mbs = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>(true);
	        var services = mbs.Where(x => x.GetType().GetCustomAttributes<ServiceAttribute>().Any());
	        foreach (var service in services)
	        {
		        var att = service.GetType().GetCustomAttribute<ServiceAttribute>();
		        var type = att?.serviceType ?? service.GetType();
		        ServiceLocator.RegisterService(type, service);
	        }
        }
        
		private static void InjectReferences(Scene scene)
		{
			var roots = scene.GetRootGameObjects();
			var mbs = roots.SelectMany(x => x.GetComponentsInChildren<MonoBehaviour>(true));
			mbs = mbs.Where(x => x);
			foreach (var mb in mbs) Injector.FillDeps(mb);
		}
	}
}
