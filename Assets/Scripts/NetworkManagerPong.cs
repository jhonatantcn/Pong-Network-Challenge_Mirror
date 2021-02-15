using Mirror;
using UnityEngine;

// NetworkManager personalizado que simplesmente atribui as posições corretas da raquete quando
// os jogadores aparecem.

[AddComponentMenu("")]
public class NetworkManagerPong : NetworkManager
{
    public Transform leftRacketSpawn; // Posição da raquete esquerda
    public Transform rightRacketSpawn; // Posição da raquete direita
    GameObject ball;  // GameObject declarado

    // Método chamado no servidor quando um cliente adiciona um novo jogador com ClientScene.AddPlayer.
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // Adiciona o jogador na posição correta de spawn
        // Caso o número de jogadores seja zero somente aparece a raquete esquerda, caso não, aparece a direita
        Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
        // O novo player é instanciado e ganha posição
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        // O novo player é adicionado a conexão
        NetworkServer.AddPlayerForConnection(conn, player);

        // Faz o spawn da bola caso existam dois jogadores
        if (numPlayers == 2)
        {
            ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball")); // Bola instanciada
            NetworkServer.Spawn(ball); // Spawn da bola
        }
    }

    // Método chamado quando um cliente se disconecta
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // A bola é destruída
        if (ball != null)
            NetworkServer.Destroy(ball);

        // chama a funcionalidade base do NetworkManager (que destrói o jogador)
        base.OnServerDisconnect(conn);
    }
}
