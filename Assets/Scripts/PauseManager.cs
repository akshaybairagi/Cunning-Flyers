using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public GameObject pausePanel;

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == true)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        GameController.instance.SetCurrentState(GameState.Pause);
        pausePanel.SetActive(true);
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
            GameController.instance.SetCurrentState(GameController.instance.lastState);
        }
    }
}
