using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    //Score Panel
    public Text scoreText;
    public Text highScoreText;

    //GameoverPanel and Stats Panel
    public Text goHighScoreText;


    //PauseBeforeStart 
    public Animator startBtnAnimator;

    //Training Back Button
    public Animator backBtn;

    //Training Button
    public Animator traningPanel;

    public Animator menuPanel;
    public Animator gameOverPanel;
    public Animator statsPanel;

    //Settings Panel
    public GameObject settingsPanel;

    //Save Me Panel
    public GameObject saveMePanel;

    //Start Tutorial Animation
    public GameObject tapToStart;

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
        if(GameController.instance.currentState != GameState.Training)
            MenuController(GameState.PauseBeforeStart);
        else
            MenuController(GameState.Training);
    }

    public void UpdatePlayerStats()
    {
        scoreText.text = GameController.instance.score.ToString();
        highScoreText.text = "Best " + GameController.instance.highScore.ToString();
    }

    public void MenuController(GameState state)
    {
        switch (state)
        {
            case GameState.PauseBeforeStart:
                    traningPanel.SetBool("IsActive", true);
                    tapToStart.SetActive(true);
                break;

            case GameState.Training:
                traningPanel.SetBool("IsActive", false);
                backBtn.SetBool("IsActive", true);
                tapToStart.SetActive(false);
                break;

            case GameState.TrainingBack:
                break;

            case GameState.Play:
                traningPanel.SetBool("IsActive", false);
                tapToStart.SetActive(false);
                break;

            case GameState.Gameover:

                menuPanel.SetBool("IsActive", true);
                settingsPanel.SetActive(false);

                if (GameController.instance.score == GameController.instance.highScore
                        && GameController.instance.highScore >= 2)
                {
                    goHighScoreText.text = GameController.instance.score.ToString();
                    statsPanel.SetBool("IsActive", true);

                }
                else
                {
                    gameOverPanel.SetBool("IsActive", true);
                }

                if (Advertisement.IsReady())
                    saveMePanel.SetActive(true);
                else
                    saveMePanel.SetActive(false);
                    
                break;

            case GameState.Settings:
                menuPanel.SetBool("IsActive", false);
                settingsPanel.SetActive(true);
                break;

            default:
                break;
        }
    }
}
