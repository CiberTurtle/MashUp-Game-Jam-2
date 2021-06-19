using UnityEngine;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Actions/Player Pref")]
	public class PlayerPref : MonoBehaviour
	{
		public string key = string.Empty;

		public void SetBool(bool value)
		{
			PlayerPrefs.SetInt(key, value ? 0 : 1);
		}

		public void SetInt(int value)
		{
			PlayerPrefs.SetInt(key, value);
		}

		public void SetFloat(float value)
		{
			PlayerPrefs.SetFloat(key, value);
		}

		public void SetString(string value)
		{
			PlayerPrefs.SetString(key, value);
		}
	}
}