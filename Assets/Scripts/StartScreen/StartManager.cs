using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject statsMenu;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowStats() {
        Dictionary<string, int> stats = GameDataManager.GetLeaderboard();
        mainMenu.SetActive(false);
        statsMenu.SetActive(true);
        string text = "";
        if (stats.Count > 0) {
            foreach (KeyValuePair<string, int> kvp in stats) {
                text += kvp.Key + " -- " + kvp.Value + "\n";
            }
        } else {
            text = "No players lmao";
        }
        GameObject.Find("StatsText").GetComponent<TMP_Text>().SetText(text);
    }

    public void ResetStats() {
        GameDataManager.ResetScores();
        ShowStats();
    }
}
