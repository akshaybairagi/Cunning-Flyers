using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

//Game States
public enum GameState
{
    Splash,
    PauseBeforeStart,
    Training,TrainingBack,
    Play,
    Pause,Gameover,
    Restart,
    Exit
}

public class GameController : MonoBehaviour {

    public static GameController instance;    

    //Current Game State
    public GameState currentState;

    public GameState lastState;

    //Time Since last state changed
    float lastStateChange = 0.0f;    

    public int score;
    public int highScore;

    private string fileName = "/playerInfo.dat";

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
    }

    void Update()
    {
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
    public int highScore;
}
