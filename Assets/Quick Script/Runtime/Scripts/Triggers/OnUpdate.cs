using UnityEngine;
using UnityEngine.Events;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Triggers/On Update")]
	public class OnUpdate : MonoBehaviour
	{
		public UnityEvent onUpdate = new UnityEvent();
		public UnityEvent onFixedUpdate = new UnityEvent();

		void Update()
		{
			onUpdate.Invoke();
		}

		void FixedUpdate()
		{
			onFixedUpdate.Invoke();
		}
	}
}