using UnityEngine;

public enum DonutType {Normal, Bad, Buff};

public class DonutBehaviour : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource coinCollectSound, badCoinCollectSound, buffCoinCollectSound;
    public float rotationSpeed = 60;
    public DonutType donutType = DonutType.Normal;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        coinCollectSound = GetAudioSource("CoinCollected");
        badCoinCollectSound = GetAudioSource("BadCoinCollected");
        buffCoinCollectSound = GetAudioSource("BuffCoinCollected");

        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Random.Range(500, 1250)));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -13) {
            Destroy(gameObject);
        }
    }

    private AudioSource GetAudioSource(string name) {
        return GameObject.Find(name).GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (donutType == DonutType.Normal) {
            coinCollectSound.Play();
            gameManager.AddScore(69);
            gameManager.PushFreddy();
        } else if (donutType == DonutType.Bad) {
            badCoinCollectSound.Play();
            gameManager.RemoveScore(420);
            gameManager.PushFreddyDown();
        } else if (donutType == DonutType.Buff) {
            buffCoinCollectSound.Play();
            gameManager.AddScore(420);
            gameManager.PushFreddyHigher();
        }
        Destroy(gameObject);
    }

}
