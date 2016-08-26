using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public float seconds = 5;
    public string sceneName = "GameScene";

    // Use this for initialization
    void Start () {
        StartCoroutine(GoToScene());
    }

    IEnumerator GoToScene()
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }
}
