using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {

    public float seconds = 2;
    public string sceneName = "GameScene";

    // Use this for initialization
    void Start()
    {
        StartCoroutine(GoToScene());
    }

    IEnumerator GoToScene()
    {
        yield return new WaitForSeconds(seconds);
        GameController.instance.SetCurrentState(GameState.PauseBeforeStart);
        SceneManager.LoadScene(sceneName);
    }
}
