using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public float speed = 30;
    public Rigidbody2D rigidbody2d;
    public Vector3 startPosition;
    public GameObject gameManager;

    void Start()
    {
        startPosition = transform.position;
        gameManager = GameObject.Find("GameManager");
    }

    public void Reset()
    {
        rigidbody2d.velocity = Vector2.zero;
        transform.position = startPosition;
    }

    // need to use FixedUpdate for rigidbody
    void FixedUpdate()
    {
        // only let the local player control the racket.
        // don't control other player's rackets
        if (isLocalPlayer)
        {
            rigidbody2d.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;
            //if (gameManager.GetComponent<GameManager>().needResetRackets)
            //{
            //    Reset();
            //    gameManager.GetComponent<GameManager>().needResetRackets = false;
            //}
        }


    }
}
