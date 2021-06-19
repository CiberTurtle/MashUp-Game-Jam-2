using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Actions/Rotate")]
	public class Rotate : MonoBehaviour
	{
		public GameObject gameObj;
		public void SetGameObj(GameObject gameObj) => this.gameObj = gameObj;

		[Space]

		public Ease ease = Ease.Unset;

		[Space]

		public float rotation = 0;
		public bool relitive = false;

		[Space]

		public LoopType loopType = LoopType.Yoyo;
		public int loops = 0;

		[Space]

		public float delay = 0;
		public float duration = 1;

		[Space]

		public UnityEvent onComplete = new UnityEvent();

		public void DoMove()
		{
			gameObj.transform
			.DORotate(relitive ? gameObj.transform.eulerAngles + new Vector3(0, 0, rotation) : new Vector3(0, 0, rotation), duration)
			.SetEase(ease)
			.SetLoops(loops, loopType)
			.SetDelay(delay)
			.OnComplete(() => onComplete.Invoke());
		}

		void OnDrawGizmos()
		{
			if (!gameObj) return;

			Gizmos.color = new Color(1, 0, 0, 0.5f);

			Gizmos.DrawLine(gameObj.transform.position, (Vector2)gameObj.transform.position + (relitive ? AngleToVector(gameObj.transform.eulerAngles.z + rotation) : AngleToVector(rotation)));
		}

		Vector2 AngleToVector(float angle)
		{
			return new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
		}
	}
}