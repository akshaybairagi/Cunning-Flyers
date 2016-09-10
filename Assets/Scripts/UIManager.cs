using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    //Score Panel
    public Text scoreText;
    public Text highScoreText;


    //PauseBeforeStart 
    public Animator startBtnAnimator;

    //Training Back Button
    public Animator backBtn;

    //Training Button
    public Animator traningPanel;

    public Animator menuPanel;
    public Animator gameOverPanel;
    public Animator statsPanel;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        MenuController(GameState.PauseBeforeStart);
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
                    traningPanel.SetBool("IsActive", true);
                break;

            case GameState.Training:
                traningPanel.SetBool("IsActive", false);
                break;

            case GameState.TrainingBack:
                break;

            case GameState.Play:
                traningPanel.SetBool("IsActive", false);
                break;

            case GameState.Gameover:
                menuPanel.SetBool("IsActive", true);
                gameOverPanel.SetBool("IsActive", true);
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
