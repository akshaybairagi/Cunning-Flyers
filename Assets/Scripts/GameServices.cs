using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameServices : MonoBehaviour {

    // show achievements UI
    public void ShowAchievementUI()
    {
        Social.ShowAchievementsUI();
    }

    // show leaderboard UI
    public void ShowLeaderBoardUI()
    {
        Social.ShowLeaderboardUI();
    }
}
