using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehind : MonoBehaviour
{
    public bool isWallBehindPlayer1;
    
    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.gameObject.CompareTag("Ball"))
        {
            if (!isWallBehindPlayer1)
            {
                Debug.Log("Player 1 Scored...");
                GameObject.Find("GameManager").GetComponent<GameManager>().ScoredPlayer1();
            }
            else
            {
                Debug.Log("Player 2 Scored...");
                GameObject.Find("GameManager").GetComponent<GameManager>().ScoredPlayer2();
            }
        }
    }
}
