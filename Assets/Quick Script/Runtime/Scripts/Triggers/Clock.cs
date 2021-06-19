using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Triggers/Clock")]
	public class Clock : MonoBehaviour
	{
		public bool paused = false;

		public float duration = 1;
		public void SetDuration(float duration) => this.duration = duration;

		[Space]

		[ProgressBar("Clock", "duration", EColor.Blue)]
		public float time = 0;

		[Space]

		public UnityEvent onClockCycle = new UnityEvent();
		public UnityEvent<float> onClockTick = new UnityEvent<float>();

		void FixedUpdate()
		{
			if (!paused)
			{
				time += Time.fixedDeltaTime;

				if (time > duration)
				{
					onClockCycle.Invoke();

					ResetClock();
				}

				onClockTick.Invoke(time);
			}
		}

		public void StartClock()
		{
			paused = false;
		}

		public void StopClock()
		{
			time = 0;
			paused = true;
		}

		public void PauseClock()
		{
			paused = true;
		}

		public void TogglePauseClock()
		{
			paused = !paused;
		}

		public void ResetClock()
		{
			time = 0;
		}
	}
}