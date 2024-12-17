using DependEasy.Internal;
using UnityEngine;

namespace DependEasy
{
	/// <summary>
	/// Inherit from this and call FillDependencies() at the end of your [SetUp] method.
	/// This solves dependencies for your Unity objects created in unit/integration tests.
	/// </summary>
	public abstract class TestFixture
	{
		protected void FillDependencies() => InitScenes.ForceReinit();
		
		protected void RegisterConfig(ScriptableObject config) => ServiceLocator.RegisterService(config.GetType(), config);
	}
}