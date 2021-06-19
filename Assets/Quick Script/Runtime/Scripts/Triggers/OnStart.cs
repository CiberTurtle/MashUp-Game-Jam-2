using UnityEngine;
using UnityEngine.Events;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Triggers/On Start")]
	public class OnStart : MonoBehaviour
	{
		public UnityEvent onAwake = new UnityEvent();
		public UnityEvent onEnable = new UnityEvent();
		public UnityEvent onStart = new UnityEvent();

		void Awake()
		{
			onAwake.Invoke();
		}

		void OnEnable()
		{
			onEnable.Invoke();
		}

		void Start()
		{
			onStart.Invoke();
		}
	}
}