using System.Collections;
using DependEasy;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace EditModeTests
{
	public class EnterPlaymodeTests
	{
		[SetUp]
		public void Setup()
		{
			EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
			new GameObject().AddComponent<TestMonoBehaviour>().gameObject.AddComponent<TestDependency>();
		}

		[UnityTest]
		public IEnumerator ShouldHaveDependencySetAfterEnteredPlaymode()
		{
			var testObj = Object.FindObjectOfType<TestMonoBehaviour>();
			
			EditorApplication.isPlaying = true;
			yield return new WaitUntil(() => EditorApplication.isPlaying);
			
			Assert.IsNotNull(testObj.dependency, "Dependency is null after entering playmode");
		}

		[UnityTest]
		public IEnumerator ShouldHaveDependencySetAfterCreatingNewObjectAtRuntime()
		{
			EditorApplication.isPlaying = true;
			yield return new WaitUntil(() => EditorApplication.isPlaying);

			var testObj = Factory.AddComponent<TestMonoBehaviour>(new GameObject());
			Assert.IsNotNull(testObj.dependency, "Dependency is null after creating with factory");
		}
	}

	[Service] public class TestDependency : MonoBehaviour { }

	public class TestMonoBehaviour : MonoBehaviour
	{
		[Inject] internal TestDependency dependency;
	}
}