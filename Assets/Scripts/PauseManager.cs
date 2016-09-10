using UnityEngine;
using System.Collections;

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
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void UnPauseGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
