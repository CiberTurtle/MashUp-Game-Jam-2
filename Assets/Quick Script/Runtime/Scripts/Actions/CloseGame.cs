using UnityEngine;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Actions/Close Game")]
	public class CloseGame : MonoBehaviour
	{
		public void Close()
		{
			Application.Quit();
		}

		public void Close(int exitCode)
		{
			Application.Quit(exitCode);
		}
	}
}