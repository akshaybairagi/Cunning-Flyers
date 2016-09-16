using UnityEngine;

public class ButtonManagerScript : MonoBehaviour {

    public void ShowLeaderboardUI()
    {
        // show leaderboard UI
        if (GameController.instance.IsUserAuthenticated == true)
            Social.ShowLeaderboardUI();
    }

    public void ShowSettingsUI()
    {
        GameController.instance.SetCurrentState(GameState.Settings);
        UIManager.instance.MenuController(GameState.Settings);
    }

    public void ExitSettingsUI()
    {
        GameController.instance.SetCurrentState(GameState.Gameover);
        UIManager.instance.MenuController(GameState.Gameover);
    }
}
