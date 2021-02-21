using Mirror;
using TMPro;
using UnityEngine;

public class WallBehindPlayer2 : NetworkBehaviour
{
    [SyncVar] public int scoreP1 = 0; // Score do Player1 (esquerda)

    // A opção trigger foi acionada no GameObject "WallBehindPlayer2", então ao colidir este método é executado
    private void OnTriggerEnter2D(Collider2D Collision)
    {
        // Caso a colisão seja com a bola
        if (Collision.gameObject.CompareTag("Ball"))
        {
            // Player 1 pontua...
            CmdServerScored();
            // E as posições são resetadas (bola e raquetes)
            GameObject.Find("GameManager").GetComponent<GameManager>().ResetPositions();
        }
    }

    // Contagem da pontuação do player 2 feita no servidor
    void CmdServerScored()
    {
        scoreP1 += 1;
        RpcScoreUpdate(scoreP1); // É solicitada a atualização da pontuação na tela do cliente
    }

    // Atualização da pontuação na tela do cliente
    [ClientRpc]
    public void RpcScoreUpdate(int score)
    {
        GameObject.FindWithTag("ScorePlayer1").GetComponent<TextMeshProUGUI>().text = "" + score;
    }
}

