using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Actions/Change Scene")]
	public class ChangeScene : MonoBehaviour
	{
		public UnityEvent onBeforeChangeScene = new UnityEvent();

		public void PreviousScene()
		{
			onBeforeChangeScene.Invoke();

			Time.timeScale = 1;

			var index = SceneManager.GetActiveScene().buildIndex;

			if (index > 0)
				SceneManager.LoadScene(index - 1);
		}

		public void NextScene()
		{
			onBeforeChangeScene.Invoke();

			Time.timeScale = 1;

			var index = SceneManager.GetActiveScene().buildIndex;

			if (index < SceneManager.sceneCount)
				SceneManager.LoadScene(index + 1);
		}

		public void LoadScene(int index)
		{
			onBeforeChangeScene.Invoke();

			Time.timeScale = 1;

			SceneManager.LoadScene(index);
		}

		public void LoadScene(string name)
		{
			onBeforeChangeScene.Invoke();

			Time.timeScale = 1;

			SceneManager.LoadScene(name);
		}
	}
}