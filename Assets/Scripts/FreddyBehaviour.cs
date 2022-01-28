using UnityEngine;

public class FreddyBehaviour : MonoBehaviour
{
    public int pushPower;
    private Rigidbody2D rigidBody;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidBody.AddForce(new Vector2(0, 400));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushUp()
    {
        PushUp(pushPower);
    }

    public void PushUp(int power) {
        rigidBody.AddForce(new Vector2(0, power));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("bottle"))
        {
            gameManager.EndGame();
        }
    }
}
