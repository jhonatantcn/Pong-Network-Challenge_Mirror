using Mirror;
using TMPro;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    //[Header("Walls")]
    //public GameObject wallBehindPlayer1;
    //public GameObject wallBehindPlayer2;

    // [Header("Score")]
    // public GameObject textPlayer1;
    // public GameObject textPlayer2;

    // private int ScorePlayer1;
    // private int ScorePlayer2;

    // public bool needResetRackets;

    //[ClientRpc]
    // public void ScoredPlayer1()
    // {
        // ScorePlayer1++;
        // textPlayer1.GetComponent<TextMeshProUGUI>().text = ScorePlayer1.ToString();
        // needResetRackets = true; // As Rackets dos dois players
        // ResetBallPosition();
    // }

    //[ClientRpc]
    // public void ScoredPlayer2()
    // {
        // ScorePlayer2++;
        // textPlayer2.GetComponent<TextMeshProUGUI>().text = ScorePlayer2.ToString();
        // needResetRackets = true; // As Rackets dos dois players
        // ResetBallPosition();
    // }

    public void ResetPosition()
    {
        ball = GameObject.FindWithTag("Ball");
        ball.GetComponent<Ball>().Reset();
        RpcRacketsPositionUpdate();
    }

    [ClientRpc]
    public void RpcRacketsPositionUpdate() //RpcMajScore
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Racket"))
        {
            go.GetComponent<Player>().Reset();
        }
    }

        // Function to Quit Game
#if UNITY_EDITOR // If inside the editor, just show the debug message "Quit Game!".
        public void QuitGame()
    {
        Debug.Log("Quit Game!");
    }
#else // If not, close the application.
    public void QuitGame()
    {
        Application.Quit();
        //System.Diagnostics.Process.GetCurrentProcess().Kill();
    }
#endif

}