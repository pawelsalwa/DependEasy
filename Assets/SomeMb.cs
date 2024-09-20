using DependEasy;
using UnityEngine;

namespace DefaultNamespace
{
	public class SomeMb : MonoBehaviour
	{
		[Inject] private SomeService _service;

		private void Awake() => Debug.Log($"<color=orange> service awake: {_service}</color>");

		private void Start() => Debug.Log($"<color=orange> service start: {_service}</color>");

	}
}