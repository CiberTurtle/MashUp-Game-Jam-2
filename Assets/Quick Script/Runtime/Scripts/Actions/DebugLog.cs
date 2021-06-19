using UnityEngine;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Actions/Debug Log")]
	public class DebugLog : MonoBehaviour
	{
		[TextArea]
		public string message;
		public void SetMessage(string message) => this.message = message;

		public void Log()
		{
			Debug.Log(message);
		}

		public void Log(string message)
		{
			Debug.Log(message);
		}
	}
}