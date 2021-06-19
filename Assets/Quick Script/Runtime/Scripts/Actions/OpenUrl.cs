using UnityEngine;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Actions/Open URL")]
	public class OpenUrl : MonoBehaviour
	{
		public string url;

		public void Open()
		{
			OpenURL(url);
		}

		public void OpenURL(string url)
		{
			Application.OpenURL(url);
		}
	}
}