using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrainingScript : MonoBehaviour {

    public string sceneName = "StartScreen";

    // Use this for initialization
    void Start()
    {
        if (GameController.instance.trainingMode == true)
        {
            //transform.FindChild("Text").GetComponent<Text>().text = "Game";
            //gameObject.SetActive(false);
        }
    }

    public void SetTrainingMode()
    {
        //gameObject.SetActive(false);
        GameController.instance.trainingMode = true;
        GameController.instance.isDead = false;
        SceneManager.LoadScene(sceneName);
    }

    public void ResetTrainingMode()
    {
        GameController.instance.trainingMode = false;
    }
}
