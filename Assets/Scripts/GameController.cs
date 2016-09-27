using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using GooglePlayGames;

//Game States
public enum GameState
{
    Splash,
    PauseBeforeStart,
    Training,TrainingBack,
    Play,
    Pause,Gameover,
    Restart,
    Escape,
    Settings,
    GameServices
}

public class GameController : MonoBehaviour {

    public static GameController instance;    

    //Current Game State
    public GameState currentState;
    //last game state
    public GameState lastState;

    //Time Since last state changed
    float lastStateChange = 0.0f;    

    public long score;
    public long highScore;

    //filename to saved on phone
    private string fileName = "/playerInfo.dat";

    //Google Game Services
    public bool IsUserAuthenticated = false;

    //Continue Game - Video Ads
    public int contGameCount = 0;
    public int maxLife = 2;
    public bool continueGame = false;

    // Use this for initialization
    void Awake ()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;            
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
	}

    void Start()
    {
        //Initialize Game State
        SetCurrentState(GameState.Splash);
        //Activate Google Play Games
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        // authenticate user:
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
            if(success == true)
            {
                IsUserAuthenticated = true;
            }
            else
            {
                IsUserAuthenticated = false;
            }
        });

    }

    void Update()
    {
        //Check for back button on mobile phone 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCurrentState(GameState.Escape);
        }

        switch (currentState)
        {
            case GameState.PauseBeforeStart:
                
                if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetMouseButtonDown(0))
                {
                    SetCurrentState(GameState.Play);
                    UIManager.instance.MenuController(GameState.Play);
                }

                break;

            case GameState.Splash:

                Scene scene = SceneManager.GetActiveScene();

                if (scene.name == "GameScene")
                {
                    SetCurrentState(GameState.PauseBeforeStart);
                }

                break;

            case GameState.Escape:

                if (lastState == GameState.Play || lastState == GameState.Training)
                {
                    SetCurrentState(GameState.Gameover);
                    UIManager.instance.MenuController(GameState.Gameover);
                }
                else
                {
                    Application.Quit();
                }

                break;

            default:
                break;
        }
    }
    
    public void SetCurrentState(GameState state)
    {
        lastState = currentState;
        currentState = state;
        lastStateChange = Time.time;
    }

    float GetStateElapsed()
    {
        return Time.time - lastStateChange;
    }

    //Save data to binary file
    public void Save()
    {
        // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
        if(IsUserAuthenticated == true)
        {
            Social.ReportScore(score, CunningFlyersResources.leaderboard_global_scoreboard,(bool success) => {
                // handle success or failure
            });
        }        

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName);

        PlayerData data = new PlayerData();
        data.highScore = highScore;

        bf.Serialize(file, data);
        file.Close();
    }

    //Load data from binary file
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName,FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            
            highScore = data.highScore;
        }
    }

    //On Quiting app delete the instance
    public void OnApplicationQuit()
    {
        //instance = null;
    }
}

//Saving Game Data to the phone disk
[Serializable]
class PlayerData
{
    public long highScore;
}
