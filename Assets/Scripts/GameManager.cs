using UnityEngine;
using TMPro;
using Mirror.Examples.Pong;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    [Header("Player 1")]
    public GameObject racketPlayer1;
    public GameObject wallBehindPlayer1;

    [Header("Player 2")]
    public GameObject racketPlayer2;
    public GameObject wallBehindPlayer2;

    [Header("Score")]
    public GameObject textPlayer1;
    public GameObject textPlayer2;

    private int ScorePlayer1;
    private int ScorePlayer2;

    public void ScoredPlayer1()
    {
        ScorePlayer1++;
        textPlayer1.GetComponent<TextMeshProUGUI>().text = ScorePlayer1.ToString();
        ResetPosition();
    }

    public void ScoredPlayer2()
    {
        ScorePlayer2++;
        textPlayer2.GetComponent<TextMeshProUGUI>().text = ScorePlayer2.ToString();
        ResetPosition();
    }

    private void ResetPosition()
    {
        ball = GameObject.FindWithTag("Ball");
        ball.GetComponent<Ball>().Reset();
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