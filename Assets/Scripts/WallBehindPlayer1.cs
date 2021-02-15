using Mirror;
using TMPro;
using UnityEngine;

public class WallBehindPlayer1 : NetworkBehaviour
{
    [SyncVar] public int scoreP2 = 0; // Score do Player2 (direita)

    // A opção trigger foi acionada no GameObject "WallBehindPlayer1", então ao colidir este método é executado
    private void OnTriggerEnter2D(Collider2D Collision)
    {
        // Caso a colisão seja com a bola
        if (Collision.gameObject.CompareTag("Ball"))
        {
            // Player 2 pontua...
            CmdServerScored();
            // E as posições são resetadas (bola e raquetes)
            GameObject.Find("GameManager").GetComponent<GameManager>().ResetPositions();
        }
    }

    // Contagem da pontuação do player 2 feita no servidor
    void CmdServerScored()
    {
        scoreP2 += 1;
        RpcScoreUpdate(scoreP2); // É solicitada a atualização da pontuação na tela do cliente
    }

    // Atualização da pontuação na tela do cliente
    [ClientRpc]
    public void RpcScoreUpdate(int score)
    {
        GameObject.FindGameObjectWithTag("ScorePlayer2").GetComponent<TextMeshProUGUI>().text = "" + score;
    }
}
