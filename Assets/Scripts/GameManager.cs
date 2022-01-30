using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameActive = false;
    public GameObject freddy, amogus, menu, recordMenu;
    [Tooltip("Normal, Bad, Good")]
    public GameObject[] donuts;
    private AudioSource pooPlayer;
    private FreddyBehaviour freddyBehaviour;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        pooPlayer = GameObject.Find("PooPlayer").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        score = 0;
        UpdateScore();
        menu.SetActive(false);
        gameActive = true;
        Instantiate(freddy);
        freddyBehaviour = GameObject.FindGameObjectWithTag("freddy").GetComponent<FreddyBehaviour>();
        StartCoroutine(GenerateBuffs());
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("StartScreen");
    }

    public void EndGame()
    {
        gameActive = false;
        Destroy(GameObject.FindGameObjectWithTag("freddy"));
        foreach (GameObject activeDonut in GameObject.FindGameObjectsWithTag("donut"))
        {
            Destroy(activeDonut);
        }
        StartCoroutine(EndGameAnimation());
    }

    private IEnumerator EndGameAnimation()
    {
        amogus.SetActive(true);
        pooPlayer.Play();
        yield return new WaitForSeconds(3);
        pooPlayer.Stop();
        amogus.SetActive(false);
        if (GameDataManager.IsRecord(score)) {
            RegisterNewRecord();
        } else {
            menu.SetActive(true);
        }
    }

    public void PushFreddy()
    {
        freddyBehaviour.PushUp();
    }

    public void PushFreddyHigher() {
        freddyBehaviour.PushUp(600);
    }

    public void PushFreddyDown() {
        freddyBehaviour.PushUp(-200);
    }

    private void RegisterNewRecord() {
        recordMenu.SetActive(true);
    }

    public void SubmitNewRecord() {
        string text = GameObject.Find("NameInput").GetComponent<TMP_InputField>().text;
        if (text.Length == 0) {
            text = "Anonymous";
        }
        GameDataManager.AddScore(text, score);
        recordMenu.SetActive(false);
        menu.SetActive(true);
    }

    private IEnumerator GenerateBuffs()
    {
        while (gameActive) {
            yield return new WaitForSeconds(Random.Range(0.3f, 0.8f));
            if (!gameActive) {
                break;
            }
            int donutIndicy = Random.Range(1, 11);
            GameObject donutPrefab;
            if (donutIndicy < 7) {
                donutPrefab = donuts[0];
            } else if (donutIndicy > 7 && donutIndicy < 9) {
                donutPrefab = donuts[1];
            } else {
                donutPrefab = donuts[2];
            }
            Instantiate(donutPrefab, new Vector3(Random.Range(-4.5f, 4.5f), -11, 0), donutPrefab.transform.rotation);
        }
    }

    private void UpdateScore()
    {
        GameObject.Find("ScoreText").GetComponent<TMP_Text>().SetText("Score: " + score);
    }

    public void AddScore(int _score)
    {
        score += _score;
        UpdateScore();
    }

    public void RemoveScore(int _score) {
        AddScore(-_score);
    }

    private Vector3 RandomVector()
    {
        return new Vector3(
            Random.Range(-4.5f, 4.5f),
            Random.Range(-5.0f, 8.0f),
            0
        );
    }
}
