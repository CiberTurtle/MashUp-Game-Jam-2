using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Actions/Move")]
	public class Move : MonoBehaviour
	{
		public GameObject gameObj;
		public void SetGameObj(GameObject gameObj) => this.gameObj = gameObj;

		[Space]

		public Ease ease = Ease.Unset;

		[Space]

		public Vector2 position = Vector2.zero;
		public bool relitive = true;

		[Space]

		public LoopType loopType = LoopType.Yoyo;
		public int loops = 0;

		[Space]

		public float delay = 0;
		public float duration = 1;

		[Space]

		public UnityEvent onMoveFinished = new UnityEvent();

		public void DoMove()
		{
			gameObj.transform
			.DOMove(relitive ? (Vector2)gameObj.transform.position + position : position, duration)
			.SetEase(ease)
			.SetLoops(loops, loopType)
			.SetDelay(delay)
			.OnComplete(() => onMoveFinished.Invoke());
		}

		void OnDrawGizmos()
		{
			if (!gameObj) return;

			Gizmos.color = new Color(1, 0, 0, 0.5f);

			Gizmos.DrawLine(gameObj.transform.position, relitive ? (Vector2)gameObj.transform.position + position : position);
		}
	}
}