using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames;

public class ButtonManagerScript : MonoBehaviour {

    public Button signOutBtn;
    public Text signOutText;

    string subject = "Subject text";
    string body = "Actual text (Link)";

    //Share  Button
    public void OnAndroidTextSharingClick()
    {
        subject = "Cunning Flyers: High Score";
        body = "Hey, i scored " + GameController.instance.highScore + " in Cunning Flyers. Beat me if you can!!";
        StartCoroutine(ShareAndroidText());
    }

    // Share Game
    public void ShareGame()
    {
        subject = "Cunning Flyers";
        body = "Falling was never so fun, check out this new arcade adventure - Cunning Flyers";
        StartCoroutine(ShareAndroidText());
    }

    IEnumerator ShareAndroidText()
    {
        yield return new WaitForEndOfFrame();

        //Reference of AndroidJavaClass class for intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        //Reference of AndroidJavaObject class for intent
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        //call setAction method of the Intent object created
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        //set the type of sharing that is happening
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        //add data to be passed to the other activity i.e., the data to be sent
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Text Sharing ");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
        //get the current activity
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //start the activity by sending the intent data
        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
        currentActivity.Call("startActivity", jChooser);
    }
    
    //Rate Button
    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.rimgames.CunningFlyers");
    }

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

    //Sign Out Button
    public void SignOut()
    {
        if (GameController.instance.IsUserAuthenticated)
        {
            // sign out
            PlayGamesPlatform.Instance.SignOut();
            signOutText.text = "sign in";
        }
        else
        {
            Social.localUser.Authenticate((bool success) => {
                // handle success or failure
                if (success == true)
                {
                    GameController.instance.IsUserAuthenticated = true;
                    signOutText.text = "sign out";
                }
                else
                {
                    GameController.instance.IsUserAuthenticated = false;
                    signOutText.text = "sign in";
                }
            });
        }
        
    }
}
