using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {

    public static GameController control;

    public int score;
    public int highScore;
    public int coins;
    public string fileName = "/playerInfo.dat";

	// Use this for initialization
	void Awake () {
        if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }
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
}

[Serializable]
class PlayerData
{
    public int highScore;
    public int coins;
}
