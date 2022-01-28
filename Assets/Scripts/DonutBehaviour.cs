using UnityEngine;

public class DonutBehaviour : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource coinCollectSound;
    public float rotationSpeed = 60;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        coinCollectSound = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime * (Random.Range(0, 2) == 0 ? -1 : 1)));
    }

    private void OnMouseDown()
    {
        coinCollectSound.Play();
        gameManager.AddScore(69);
        gameManager.PushFreddy();
        Destroy(gameObject);
    }

}
