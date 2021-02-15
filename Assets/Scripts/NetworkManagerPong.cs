using Mirror;
using UnityEngine;

// NetworkManager personalizado que simplesmente atribui as posi��es corretas da raquete quando
// os jogadores aparecem.

[AddComponentMenu("")]
public class NetworkManagerPong : NetworkManager
{
    public Transform leftRacketSpawn; // Posi��o da raquete esquerda
    public Transform rightRacketSpawn; // Posi��o da raquete direita
    GameObject ball;  // GameObject declarado

    // M�todo chamado no servidor quando um cliente adiciona um novo jogador com ClientScene.AddPlayer.
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // Adiciona o jogador na posi��o correta de spawn
        // Caso o n�mero de jogadores seja zero somente aparece a raquete esquerda, caso n�o, aparece a direita
        Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
        // O novo player � instanciado e ganha posi��o
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        // O novo player � adicionado a conex�o
        NetworkServer.AddPlayerForConnection(conn, player);

        // Faz o spawn da bola caso existam dois jogadores
        if (numPlayers == 2)
        {
            ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball")); // Bola instanciada
            NetworkServer.Spawn(ball); // Spawn da bola
        }
    }

    // M�todo chamado quando um cliente se disconecta
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // A bola � destru�da
        if (ball != null)
            NetworkServer.Destroy(ball);

        // chama a funcionalidade base do NetworkManager (que destr�i o jogador)
        base.OnServerDisconnect(conn);
    }
}
