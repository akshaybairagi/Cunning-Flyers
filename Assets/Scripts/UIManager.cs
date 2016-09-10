using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    //Score Panel
    public Text scoreText;
    public Text highScoreText;


    //PauseBeforeStart 
    public GameObject startBtn;
    public Animator startBtnAnimator;

    //Training Back Button
    public Animator backBtn;

    //Training Button
    public Animator traningBtn;

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

    public void MenuController(GameState state)
    {
        switch (state)
        {
            case GameState.PauseBeforeStart:

                break;

            case GameState.Training:
                break;

            case GameState.TrainingBack:
                break;

            case GameState.Play:
                break;

            case GameState.Gameover:
                break;


            case GameState.Pause:
                break;

            case GameState.Restart:
                break;

            case GameState.Exit:
                break;

            default:
                break;
        }
    }
}
