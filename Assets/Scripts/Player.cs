using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public float speed = 30; // velocidade pr� definida para cada raquete (mas pode ser alterada no editor Unity)
    public Rigidbody2D rigidbody2d; // F�sica de cada raquete
    public Vector3 startPosition; // Posi��o inicial de cada raquete
    public GameObject gameManager; // GameObject declarado

    void Start()
    {
        startPosition = transform.position; // startPosition recebe a posi��o inicial de cada raquete
        gameManager = GameObject.Find("GameManager"); // o GameObject declarado recebe o GameObject Genrenciador de jogo
    }

    public void Reset()
    {
        rigidbody2d.velocity = Vector2.zero; // Cancela a velocidade linear (unidades por segundo) da f�sica de cada raquete para anular movimenta��es anteriores
        transform.position = startPosition; // Retorna cada raquete a sua posi��o inicial depois de cada ponto em jogo
    }

    // precisa usar FixedUpdate para rigidbody
    void FixedUpdate()
    {
        // deixe apenas o jogador local controlar a raquete.
        // n�o controla a raquete do outro jogador
        if (isLocalPlayer)
        {
            rigidbody2d.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime; // Atribui velocidade vertical * velocidade da raquete * intervalo (seg.) de realiza��o da f�sica
        }
    }
}
