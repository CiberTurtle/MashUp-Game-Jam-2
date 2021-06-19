using System.Collections.Generic;
using UnityEngine;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Managers/Flag Manager")]
	public class FlagManager : MonoBehaviour
	{
		public static FlagManager manager { get; private set; }

		public List<string> flags = new List<string>();

		void Awake()
		{
			if (manager)
			{
				Destroy(this);
				return;
			}

			manager = this;
		}

		public void SetFlag(string flag)
		{
			if (!HasFlag(flag))
				flags.Add(flag);
		}

		public void UnsetFlag(string flag)
		{
			flags.Remove(flag);
		}

		public bool HasFlag(string flag)
		{
			return flags.Contains(flag);
		}
	}
}