using UnityEngine;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Actions/Destroy")]
	public class Destroy : MonoBehaviour
	{
		public Object obj;
		public void SetObject(Object obj) => this.obj = obj;

		public void DestroyMe(float delay)
		{
			Destroy(gameObject, delay);
		}

		public void DestroyObj()
		{
			Destroy(obj);
		}

		public void DestroyObj(Object obj)
		{
			Destroy(obj);
		}
	}
}