using DependEasy;
using DependEasy.Attributes;
using UnityEngine;

namespace DependEasyTests.Tests
{
	public class TestBehaviour : MonoBehaviour
	{
		[Inject] internal ITestDependency dependency;
	}
	
	public interface ITestDependency { }
    
	[Service(typeof(ITestDependency))] public class TestDependency : MonoBehaviour, ITestDependency { }

}