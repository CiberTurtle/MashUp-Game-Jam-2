using UnityEngine;
using UnityEngine.Events;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Middle/Switch")]
	public class Switch : MonoBehaviour
	{
		public bool on = true;
		public void Set(bool on) => this.on = on;
		public void Toggle() => on = !on;

		[Space]

		public UnityEvent onTriggerOn = new UnityEvent();
		public UnityEvent onTriggerOff = new UnityEvent();

		public void Trigger()
		{
			if (on) onTriggerOn.Invoke();
			else onTriggerOff.Invoke();
		}
	}
}