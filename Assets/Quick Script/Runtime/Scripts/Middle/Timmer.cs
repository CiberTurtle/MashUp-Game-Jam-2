using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Middle/Timmer")]
	public class Timmer : MonoBehaviour
	{
		public float duration = 5.0f;

		[Space]

		public bool oneAtATime = false;
		[EnableIf("oneAtATime")] public bool resetWhenTriggered = true;

		[Space]

		public UnityEvent onTimmerEnd = new UnityEvent();
		public UnityEvent<float> onTimmerTick = new UnityEvent<float>();

		// Hopefully someone doesn't start the timmer more than 255 times in one frame
		byte timmerId;
		float timmerTime;
		bool timmerAlive;

		IEnumerator CountDown(float duration, byte timmerId)
		{
			timmerAlive = true;

			while (timmerTime < duration)
			{
				if (timmerId != this.timmerId) yield break;

				timmerTime += Time.deltaTime;

				onTimmerTick.Invoke(timmerTime);

				yield return null;
			}

			timmerAlive = false;

			onTimmerEnd.Invoke();
		}

		public void StartTimmer()
		{
			StartTimmer(duration);
		}

		public void StartTimmer(float duration)
		{
			if (oneAtATime)
			{
				if (resetWhenTriggered)
				{
					timmerId++;
					StartCoroutine(CountDown(duration, timmerId));
				}
				else
				{
					if (!timmerAlive)
					{
						timmerId = 0;
						StartCoroutine(CountDown(duration, 0));
					}
				}
			}
			else
			{
				timmerId = 0;
				StartCoroutine(CountDown(duration, 0));
			}
		}
	}
}