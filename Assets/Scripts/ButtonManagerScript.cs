using UnityEngine;
using System.Collections;

public class ButtonManagerScript : MonoBehaviour {

    string subject = "Subject text";
    string body = "Actual text (Link)";

    public void OnAndroidTextSharingClick()
    {
        subject = "Cunning Flyers: High Score";
        body = "Hey, i scored " + GameController.instance.highScore + " in Cunning Flyers. Beat me if you can!!";
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
}
