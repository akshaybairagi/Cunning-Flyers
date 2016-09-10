using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    //UI Controls
    public Text scoreText;
    public Text highScoreText;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdatePlayerStats()
    {
        scoreText.text = GameController.instance.score.ToString();
        highScoreText.text = GameController.instance.highScore.ToString();
    }
}
