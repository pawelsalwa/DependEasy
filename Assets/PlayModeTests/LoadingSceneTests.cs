using System.Collections;
using DependEasy;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace DependEasyTests.Tests
{
	public class LoadingSceneTests
	{
		private TestBehaviour template;
		private TestDependency dependency;
		private GameObject testObj;

		[SetUp]
		public void Setup()
		{
			var xd4 = SceneManager.GetActiveScene();
			var scene = SceneManager.CreateScene("LoadingScene");
			var xd = SceneManager.GetActiveScene();
			var xd3 = SceneManager.sceneCount;

			

			SceneManager.SetActiveScene(scene);
			template = new GameObject("template").AddComponent<TestBehaviour>();
			dependency = new GameObject("serializedDependency").AddComponent<TestDependency>();
			testObj = new GameObject();
			InitScenes.ForceReinit();
		}

		// [UnityTest]
		// public IEnumerator dupa()
		// {
		// 	Assert.IsNotNull(template, "Field hasn't been injected");
		// 	yield break;
		// }
	}
}