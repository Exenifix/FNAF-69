using System.Collections.Generic;
using System.Linq;

public static class GameDataManager {
    private static GameData gameData = GameData.GetFromFile();
    public static Dictionary<string, int> GetLeaderboard() {
        return gameData.leaderboard;
    }

    public static int GetMoney() {
        return gameData.money;
    }

    public static void SetMoney(int newMoney) {
        gameData.money = newMoney;
        gameData.Save();
    }

    public static void AddMoney(int amount) {
        gameData.money += amount;
        gameData.Save();
    }

    public static void RemoveMoney(int amount) {
        AddMoney(-amount);
    }

    public static bool IsRecord(int score) {
        return GetLowestScore().Value < score || gameData.leaderboard.Count < 10;
    }

    public static void AddScore(string name, int score) {
        if (gameData.leaderboard.Count > 10) {
            KeyValuePair<string, int> lowest = GetLowestScore();
            gameData.leaderboard.Remove(lowest.Key);
        }

        gameData.leaderboard[name] = score;
        gameData.Save();
    }

    public static void ResetScores() {
        gameData.leaderboard.Clear();
        gameData.Save();
    }

    private static KeyValuePair<string, int> GetLowestScore() {
        if (gameData.leaderboard.Count == 0) {
            return new KeyValuePair<string, int>(null, 0);
        }
        KeyValuePair<string, int> minimum = gameData.leaderboard.First();
        foreach (KeyValuePair<string, int> kvp in gameData.leaderboard) {
            if (kvp.Value < minimum.Value) {
                minimum = kvp;
            }
        }
        return minimum;
    }
}
