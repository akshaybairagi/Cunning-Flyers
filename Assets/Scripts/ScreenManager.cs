using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {

	public void ChangeScene(string SceneName)
    {
        GameController.control.trainingMode = false;
        SceneManager.LoadScene(SceneName);
    }
}
