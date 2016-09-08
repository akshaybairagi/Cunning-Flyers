using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdManager : MonoBehaviour
{

    public void ShowAds()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdCallback });
        }
    }

    private void HandleAdCallback(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                GameController.control.ContinuePlay();
                break;

            case ShowResult.Skipped:
                Debug.Log("Ad Skipped");
                break;

            case ShowResult.Failed:
                Debug.Log("Ad Failed");
                break;
        }
    }
}
