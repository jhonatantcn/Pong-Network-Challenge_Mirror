using Mirror;
using TMPro;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    // Método que solicita o reset da bola e envia uma requisição aos clientes para resetarem a posição das raquetes.
    // Este método é chamado sempre que ocorre um novo ponto na partida
    public void ResetPositions()
    {
        GameObject.FindWithTag("Ball").GetComponent<Ball>().Reset();
        RpcRacketsPositionUpdate();
    }

    // Requisição aos clientes para resetarem a posição das raquetes.
    [ClientRpc]
    public void RpcRacketsPositionUpdate()
    {
        // Este código é executado dentro de cada cliente e solicita o reset das duas raquetes na tela de cada cliente,
        // o foreach é usado para atingir todas os GameObjects com a tag "Racket", neste jogo são duas.
        foreach (GameObject racket in GameObject.FindWithTag("Racket"))
        {
            racket.GetComponent<Player>().Reset();
        }
    }

    // Método para sair do jogo
#if UNITY_EDITOR // Se estiver dentro do editor, somente mostre a mensagem de debug "Quit Game!".
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
    }
#else // Se não estiver dentro do editor, feche a aplicação.
    public void QuitGame()
    {
        Application.Quit();
    }
#endif
}