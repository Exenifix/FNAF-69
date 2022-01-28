using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject donut;
    private bool donutsSpawningEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnDonuts());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        donutsSpawningEnabled = false;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void QuitGame() {
        Application.Quit();
    }

    private IEnumerator SpawnDonuts() {
        while (donutsSpawningEnabled) {
            yield return new WaitForSeconds(Random.Range(0.2f, 2.0f));
            Instantiate(donut, 
            new Vector3(
                Random.Range(-2f, 2f),
                Random.Range(2f, 4f),
                0
            ), donut.transform.rotation);
        }
    }
}
