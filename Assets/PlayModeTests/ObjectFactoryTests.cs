using System.Collections;
using DependEasy;
using DependEasy.Internal;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace DependEasyTests.Tests
{
    public class ObjectFactoryTests
    {
        private TestBehaviour template;
        private TestDependency dependency;
        private GameObject testObj;

        [SetUp]
        public void Setup()
        {
            template = new GameObject("template").AddComponent<TestBehaviour>();
            dependency = new GameObject("serializedDependency").AddComponent<TestDependency>();
            testObj = new GameObject();
            InitScenes.ForceReinit();
        }

        [Test]
        public void ShouldHaveDepenedenciesInjectedOnObjectCreatedFromPrefab()
        {
            var newObj = Factory.Instantiate(template);
            Assert.IsNotNull(newObj.dependency, "Field hasn't been injected");
        }

        [Test]
        public void ShouldHaveDepenedenciesInjectedOnComponentAddedAtRuntime()
        {
            var newObj = Factory.AddComponent<TestBehaviour>(testObj);
            Assert.IsNotNull(newObj.dependency, "Field hasn't been injected");
        }

        [UnityTest]
        public IEnumerator ShouldHaveDepenedenciesInjectedOnObjectCreatedFromPrefabDelayed()
        {
            yield return null;
            var newObj = Factory.Instantiate(template);
            Assert.IsNotNull(newObj.dependency, "Field hasn't been injected");
        }

        [UnityTest]
        public IEnumerator ShouldHaveDepenedenciesInjectedOnComponentAddedAtRuntimeDelayed()
        {
            yield return null;
            var newObj = Factory.AddComponent<TestBehaviour>(testObj);
            Assert.IsNotNull(newObj.dependency, "Field hasn't been injected");
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.Destroy(dependency.gameObject);
            Object.Destroy(template.gameObject);
            Object.Destroy(testObj);
            ServiceLocator.Clear();
            yield return null; // for Destroy() to take effect before next test;
        }
    }
}