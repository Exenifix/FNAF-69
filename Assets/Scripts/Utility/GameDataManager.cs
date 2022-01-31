using System.Collections.Generic;
using System.Linq;

public static class GameDataManager {
    private static GameData gameData = GameData.GetFromFile();
    public static Dictionary<int, string> GetLeaderboard() {
        Dictionary<int, string> dictLeaderboard = new Dictionary<int, string>();
        List<KeyValuePair<int, string>> leaderboard = gameData.leaderboard.ToList();
        leaderboard.Sort(
            delegate(KeyValuePair<int, string> pair1, KeyValuePair<int, string> pair2) {
                return pair1.Key.CompareTo(pair2.Key);
            }
        );
        foreach (KeyValuePair<int, string> kvp in leaderboard) {
            dictLeaderboard[kvp.Key] = kvp.Value;
        }
        return dictLeaderboard;
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
        return GetLowestScore().Key < score || gameData.leaderboard.Count < 10;
    }

    public static void AddScore(string name, int score) {
        if (gameData.leaderboard.Count > 10) {
            KeyValuePair<int, string> lowest = GetLowestScore();
            gameData.leaderboard.Remove(lowest.Key);
        }

        gameData.leaderboard[score] = name;
        gameData.Save();
    }

    public static void ResetScores() {
        gameData.leaderboard.Clear();
        gameData.Save();
    }

    private static KeyValuePair<int, string> GetLowestScore() {
        if (gameData.leaderboard.Count == 0) {
            return new KeyValuePair<int, string>(0, null);
        }
        KeyValuePair<int, string> minimum = gameData.leaderboard.First();
        foreach (KeyValuePair<int, string> kvp in gameData.leaderboard) {
            if (kvp.Key < minimum.Key) {
                minimum = kvp;
            }
        }
        return minimum;
    }
}
