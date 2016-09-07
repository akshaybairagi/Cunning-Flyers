using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrainingScript : MonoBehaviour {

    public string sceneName = "StartScreen";

    // Use this for initialization
    void Start()
    {
        if (GameController.control.trainingMode == true)
        {
            //transform.FindChild("Text").GetComponent<Text>().text = "Game";
            gameObject.SetActive(false);
        }
    }

    public void SetTrainingMode()
    {
        gameObject.SetActive(false);
        GameController.control.trainingMode = true;
        GameController.control.isDead = false;
        SceneManager.LoadScene(sceneName);
    }

    public void ResetTrainingMode()
    {
        GameController.control.trainingMode = false;
    }
}
