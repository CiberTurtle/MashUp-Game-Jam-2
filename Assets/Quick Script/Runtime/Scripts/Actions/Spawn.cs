using UnityEngine;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Actions/Spawn")]
	public class Spawn : MonoBehaviour
	{
		public GameObject gameObj = null;
		public void SetGameObject(GameObject gameObj) => this.gameObj = gameObj;

		public Vector2 position = Vector2.zero;
		public void SetPosition(Vector2 position) => this.position = position;

		public bool noRotation = true;
		public void SetNoRotation(bool noRotation) => this.noRotation = noRotation;

		public void SpawnGo()
		{
			Instantiate(gameObj, position, Quaternion.identity);
		}

		public void SpawnGo(GameObject gameObj)
		{
			Instantiate(gameObj, position, Quaternion.identity);
		}

		public void SpawnGoAtGo(GameObject gameObj)
		{
			Instantiate(this.gameObj, gameObj.transform.position, noRotation ? Quaternion.identity : Quaternion.Euler(0, 0, gameObj.transform.eulerAngles.z));
		}

		public void SpawnGo(Vector2 position)
		{
			Instantiate(gameObj, position, Quaternion.identity);
		}
	}
}