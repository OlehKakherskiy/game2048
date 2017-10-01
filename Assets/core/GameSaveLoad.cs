using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public static class GameSaveLoad 
{
    public static GameData GAME_DATA;
    
    public static void SaveGame()
    {
        string saveToFile = Application.persistentDataPath + "/savedGame.gd";
        Debug.Log("Save game to file = "+saveToFile);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create (saveToFile);
        bf.Serialize(file, GAME_DATA);
        file.Close();
    }
    
    public static void LoadGame()
    {
        string loadFromFile = Application.persistentDataPath + "/savedGame.gd";
        if(File.Exists(loadFromFile)) {
            Debug.Log("File '"+loadFromFile+"' exist, so loading game");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);
            GAME_DATA = (GameData) bf.Deserialize(file);
            file.Close();
            return;
        }
        Debug.Log("File '"+loadFromFile+"' doesn't exist, so load from stub");
        GAME_DATA = new GameData();
    }
}