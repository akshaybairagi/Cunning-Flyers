using UnityEngine;

public class ButtonManagerScript : MonoBehaviour {

    // show leaderboard UI
    public void ShowLeaderboardUI()
    {
        //if (GameController.instance.IsUserAuthenticated == true)
            Social.ShowLeaderboardUI();
    }

    // show Settings UI Panel
    public void ShowSettingsUI()
    {
        GameController.instance.SetCurrentState(GameState.Settings);
        UIManager.instance.MenuController(GameState.Settings);
    }


    //Exit Settings Panel
    public void ExitSettingsUI()
    {
        GameController.instance.SetCurrentState(GameState.Gameover);
        UIManager.instance.MenuController(GameState.Gameover);
    }
}
