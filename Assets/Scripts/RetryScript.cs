using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScript : MonoBehaviour {

    public string sceneName = "StartScreen";

    public void ChangeScene()
    {
        GameController.instance.isDead = false;
        SceneManager.LoadScene(sceneName);
    }
}
