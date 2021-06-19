using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Middle/Has Tag")]
	public class HasTag : MonoBehaviour
	{
		[Tag] public string hasTag;

		[Space]

		public UnityEvent<GameObject> onTrigger = new UnityEvent<GameObject>();

		public void TryTrigger(GameObject gameObj)
		{
			if (gameObj.CompareTag(hasTag))
			{
				onTrigger.Invoke(gameObj);
			}
		}
	}
}