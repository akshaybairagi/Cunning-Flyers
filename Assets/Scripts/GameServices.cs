using UnityEngine;

public class GameServices : MonoBehaviour {

	
    public void ShowAchievementUI()
    {
        // show achievements UI
        if(GameController.instance.IsUserAuthenticated == true)
            Social.ShowAchievementsUI();
    }

    public void ShowLeaderboardUI()
    {
        // show leaderboard UI
        if (GameController.instance.IsUserAuthenticated == true)
            Social.ShowLeaderboardUI();
    }
}
