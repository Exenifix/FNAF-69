using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class GameData {
    public Dictionary<string, int> leaderboard;
    public int money;
    private static readonly string dataPath = Application.persistentDataPath + "/GameData.dat";

    private GameData() {
        this.money = 0;
        this.leaderboard = new Dictionary<string, int>();
    }

    public static GameData GetFromFile() {
        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(dataPath)) {
            GameData gameData = new GameData();
            gameData.Save();
            return gameData;
        }

        FileStream dataFile = File.Open(dataPath, FileMode.Open);
        GameData data = (GameData) bf.Deserialize(dataFile);
        dataFile.Close();
        return data;
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath);
        bf.Serialize(file, this);
        file.Close();
    }
}