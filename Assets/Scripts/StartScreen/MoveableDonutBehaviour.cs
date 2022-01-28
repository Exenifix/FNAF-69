using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableDonutBehaviour : MonoBehaviour
{
    private float moveSpeed, rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(0.2f, 1.5f);
        rotationSpeed = Random.Range(0.1f, 0.5f) * (Random.Range(0, 2) == 0 ? -1 : 1);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
        if (gameObject.transform.position.y < -5.44f) {
            Destroy(gameObject);
        }
    }
}
