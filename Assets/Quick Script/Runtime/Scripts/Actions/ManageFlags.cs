using UnityEngine;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Actions/Manage Flags")]
	public class ManageFlags : MonoBehaviour
	{
		public void SetFlag(string flag)
		{
			FlagManager.manager.SetFlag(flag);
		}

		public void SetFlag(Object flag)
		{
			SetFlag(flag.name);
		}

		public void UnsetFlag(string flag)
		{
			FlagManager.manager.UnsetFlag(flag);
		}

		public void UnsetFlag(Object flag)
		{
			UnsetFlag(flag.name);
		}

		public bool HasFlag(string flag)
		{
			return FlagManager.manager.HasFlag(flag);
		}

		public bool HasFlag(Object flag)
		{
			return HasFlag(flag.name);
		}
	}
}