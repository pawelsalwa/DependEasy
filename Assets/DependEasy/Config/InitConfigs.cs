using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using DependEasy.Internal;
using Object = UnityEngine.Object;

namespace DependEasy.Config
{
	internal class InitConfigs : MonoBehaviour
	{

		[SerializeField]
		public List<ScriptableObject> configAssets = new();

		private void Awake() => RegisterConfigs();

		private void OnDestroy() => ServiceLocator.Clear();

		private void RegisterConfigs()
		{
			foreach (var asset in configAssets)
			{
				ServiceLocator.RegisterService(asset.GetType(), asset);
			}
		}

#if UNITY_EDITOR
		[ContextMenu("AddMissingConfigs")]
		private void AddMissingConfigs()
		{
			var types = TypeCache.GetTypesWithAttribute<ConfigAssetAttribute>();
			foreach (var type in types)
			{
				var assets = FindAssetsOfType(type).ToList();
				if (assets.Count > 1) Debug.Log($"<color=orange>[InitConfigs] multiple config assets found: {type.Name} </color>");
				var asset = assets.FirstOrDefault() as ScriptableObject;
				if (!asset) Debug.LogError($"<color=red>[InitConfigs] Config asset: {type.Name} is not instantiated (thus nor registered)!</color>");
				else if (!configAssets.Contains(asset))
				{
					configAssets.Add(asset);
					Debug.Log($"<color=white>[InitConfigs] Registering {type.Name} in configs list.</color>");
					EditorUtility.SetDirty(this);
				}
			}
		}
		
		private static IEnumerable<Object> FindAssetsOfType(Type type)
		{
			var filter = $"t:{type.Name}";
			var guids = AssetDatabase.FindAssets(filter);
			var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
			var assets = paths.Select(s => AssetDatabase.LoadAssetAtPath(s, type));
			return assets;
		}

		[ContextMenu("SortConfigsByName")]
		private void SortConfigsByName() => configAssets = configAssets.OrderBy(x => x.GetType().Name).ToList();
#endif
	}
}