using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {

	public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
