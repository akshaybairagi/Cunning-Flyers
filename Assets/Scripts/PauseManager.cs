using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public GameObject pausePanel;

    public Text resumeText;

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == true)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        GameController.instance.SetCurrentState(GameState.Pause);
        Time.timeScale = 0;
    }
    
    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);

        if(GameController.instance.lastState == GameState.Play)
        {
            GameController.instance.SetCurrentState(GameState.Play);
        }        
        else if(GameController.instance.lastState == GameState.Training)
        {
            GameController.instance.SetCurrentState(GameState.Training);
        }
        else
        {
            GameController.instance.SetCurrentState(GameState.PauseBeforeStart);
            SceneManager.LoadScene("GameScene");
        }
    }
}
