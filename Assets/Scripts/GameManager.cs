using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameActive = false;
    public GameObject freddy, donut, amogus;
    public GameObject[] buttons;
    private AudioSource pooPlayer;
    private FreddyBehaviour freddyBehaviour;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        pooPlayer = GameObject.Find("PooPlayer").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetAllButtonsTo(bool state) {
        foreach (GameObject button in buttons) {
            button.SetActive(state);
        }
    }

    public void StartGame()
    {
        score = 0;
        UpdateScore();
        SetAllButtonsTo(false);
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
        SetAllButtonsTo(true);
    }

    public void PushFreddy()
    {
        freddyBehaviour.PushUp();
    }

    public void PushFreddyHigher() {
        freddyBehaviour.PushUp(500);
    }

    public void PushFreddyDown() {
        freddyBehaviour.PushUp(-200);
    }

    private IEnumerator GenerateBuffs()
    {
        while (gameActive) {
            yield return new WaitForSeconds(Random.Range(0.3f, 0.8f));
            if (!gameActive) {
                break;
            }
            Instantiate(donut, RandomVector(), donut.transform.rotation);
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

    private Vector3 RandomVector()
    {
        return new Vector3(
            Random.Range(-4.5f, 4.5f),
            Random.Range(-5.0f, 8.0f),
            0
        );
    }
}
