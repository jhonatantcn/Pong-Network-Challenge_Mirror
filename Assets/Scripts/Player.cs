using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public float speed = 30; // velocidade pré definida para cada raquete (mas pode ser alterada no editor Unity)
    public Rigidbody2D rigidbody2d; // Física de cada raquete
    public Vector3 startPosition; // Posição inicial de cada raquete
    public GameObject gameManager; // GameObject declarado

    void Start()
    {
        startPosition = transform.position; // startPosition recebe a posição inicial de cada raquete
        gameManager = GameObject.Find("GameManager"); // o GameObject declarado recebe o GameObject Genrenciador de jogo
    }

    public void Reset()
    {
        rigidbody2d.velocity = Vector2.zero; // Cancela a velocidade linear (unidades por segundo) da física de cada raquete para anular movimentações anteriores
        transform.position = startPosition; // Retorna cada raquete a sua posição inicial depois de cada ponto em jogo
    }

    // precisa usar FixedUpdate para rigidbody
    void FixedUpdate()
    {
        // deixe apenas o jogador local controlar a raquete.
        // não controla a raquete do outro jogador
        if (isLocalPlayer)
        {
            rigidbody2d.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime; // Atribui velocidade vertical * velocidade da raquete * intervalo (seg.) de realização da física
        }
    }
}
