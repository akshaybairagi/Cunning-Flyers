using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //UI Controls
    public Text scoreText;
    public Text highScoreText;


    private void UpdatePlayerStats()
    {
        scoreText.text = GameController.instance.score.ToString();
        highScoreText.text = GameController.instance.highScore.ToString();
    }
}
