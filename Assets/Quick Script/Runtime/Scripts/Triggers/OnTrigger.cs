using UnityEngine;
using UnityEngine.Events;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Triggers/On Trigger")]
	public class OnTrigger : MonoBehaviour
	{
		public UnityEvent<GameObject> onTriggerEnter = new UnityEvent<GameObject>();
		public UnityEvent<GameObject> onTriggerStay = new UnityEvent<GameObject>();
		public UnityEvent<GameObject> onTriggerExit = new UnityEvent<GameObject>();

		void OnTriggerEnter2D(Collider2D collider2D)
		{
			onTriggerEnter.Invoke(collider2D.gameObject);
		}

		void OnTriggerStay2D(Collider2D collider2D)
		{
			onTriggerStay.Invoke(collider2D.gameObject);
		}

		void OnTriggerExit2D(Collider2D collider2D)
		{
			onTriggerExit.Invoke(collider2D.gameObject);
		}
	}
}