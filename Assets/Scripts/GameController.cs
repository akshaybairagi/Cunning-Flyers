using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// Game States
public enum GameState {
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

    //Time Since last state changed
    float lastStateChange = 0.0f;

    public int score;
    public int highScore;
    public int coins;
    public string fileName = "/playerInfo.dat";

    // Use this for initialization
    void Awake () {
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

    public void SetCurrentState(GameState state)
    {
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
        data.coins = coins;

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
            coins = data.coins;
        }
    }

    public void OnApplicationQuit()
    {
        instance = null;
    }
}

//Saving Game Data to the phone disk
[Serializable]
class PlayerData
{
    public int highScore;
    public int coins;
}
