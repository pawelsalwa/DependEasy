#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DependEasy.Internal
{
	[InitializeOnLoad]
	internal static class InjectorEditorExtensions
	{
		static InjectorEditorExtensions()
		{
			ObjectFactory.componentWasAdded -= ComponentAddedEditor; 
			ObjectFactory.componentWasAdded += ComponentAddedEditor;

			ObjectChangeEvents.changesPublished -= ChangesPublished;
			ObjectChangeEvents.changesPublished += ChangesPublished;
		}

		private static void ChangesPublished(ref ObjectChangeEventStream stream)
		{
			if (!Application.isPlaying) return;
			List<GameObject> gos = new();
			for (int i = 0; i < stream.length; ++i)
			{
				switch (stream.GetEventType(i))
				{
					case ObjectChangeKind.CreateGameObjectHierarchy:
						stream.GetCreateGameObjectHierarchyEvent(i, out var data);
						var createdGo = EditorUtility.InstanceIDToObject(data.instanceId) as GameObject;
						gos.Add(createdGo);
						break;
					case ObjectChangeKind.ChangeGameObjectStructureHierarchy:
						stream.GetChangeGameObjectStructureHierarchyEvent(i, out var data1);
						var createdGo1 = EditorUtility.InstanceIDToObject(data1.instanceId) as GameObject;
						gos.Add(createdGo1);
						break;
					case ObjectChangeKind.ChangeGameObjectStructure:
						stream.GetChangeGameObjectStructureEvent(i, out var data2);
						var createdGo2 = EditorUtility.InstanceIDToObject(data2.instanceId) as GameObject;
						gos.Add(createdGo2);
						break;
				}
			}

			foreach (var go in gos.SelectMany(x => x.GetComponentsInChildren<MonoBehaviour>(true).Where(x => x))) 
				Injector.FillDeps(go);
		}
		
		private static void ComponentAddedEditor(Component obj)
		{
			if (!Application.isPlaying) return;
			var mbs = obj.GetComponentsInChildren<MonoBehaviour>(true).Where(x => x);
			foreach (var mb in mbs) Injector.FillDeps(mb);
		}

	}
}
#endif