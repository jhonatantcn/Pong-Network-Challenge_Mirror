using Mirror;
using UnityEngine;

public class Ball : NetworkBehaviour
{
    public float speed = 30; // velocidade pré definida para a bola (mas pode ser alterada no editor Unity)
    public Rigidbody2D rigidbody2d; // Física da bola
    public Vector3 startPosition; // Posição inicial da bola
    public float startAngularVelocity; // Velocidade angular inicial da bola

    // Start é chamado antes da primeira atualização de frame
    void Start()
    {
        startPosition = transform.position; // startPosition recebe a posição inicial da bola
        startAngularVelocity = rigidbody2d.angularVelocity; // startAngularVelocity recebe a velocidade angular inicial da bola
    }

    // Reseta a posição da bola assim que um ponto é feito e a joga novamente
    public void Reset()
    {
        rigidbody2d.velocity = Vector2.zero; // Cancela a velocidade linear (unidades por segundo) da física da bola para anular movimentações anteriores
        rigidbody2d.angularVelocity = startAngularVelocity; // Cancela a velocidade angular (graus por segundo) da física da bola para anular movimentações anteriores
        transform.position = startPosition; // Retorna a bola a sua posição inicial (no centro da tela)
        Launch(); // Lança novamente a bola
    }

    // Este método é acionado assim que o GameObject "Ball" é instanciado no script "NetworkManagerPong.cs"
    public override void OnStartServer()
    {
        base.OnStartServer();

        // A física da bola é simulada somente no servidor
        rigidbody2d.simulated = true;

        Launch(); // Lança a bola
    }

    // Método que lança a bola randomicamente
    private void Launch()
    {
        // sorteia X e Y randomicamente, caso o valor randômico seja igual a 0, então recebe -1, caso não recebe 1.
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        // é aplicada uma velocidade a bola, usando as direções de x e y
        rigidbody2d.velocity = new Vector2(speed * x, speed * y);
    }

    // Fator de acerto
    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        // ascii art:
        // || 1 <- no topo da raquete
        // ||
        // || 0 <- no meio da raquete
        // ||
        // || -1 <- na parte inferior da raquete
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    // apenas chame este método no servidor
    [ServerCallback]
    void OnCollisionEnter2D(Collision2D col)
    {
        // Nota: 'col' contém as informações de colisão.
        // Se a bola (Ball) colidiu com uma raquete, então:
        // col.gameObject é a raquete
        // col.transform.position é a posição da raquete
        // col.collider é o colisor da raquete

        // bateu em uma raquete? então é necessário calcular o fator de acerto
        if (col.transform.GetComponent<Player>())
        {
            // Calcula a direção y via fator de acerto
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calcula a direção x via colisão oposta
            float x = col.relativeVelocity.x > 0 ? 1 : -1;

            // Calcule a direção, faça comprimento = 1 via .normalized
            Vector2 dir = new Vector2(x, y).normalized;

            // Definir velocidade com dir * speed
            rigidbody2d.velocity = dir * speed;
        }
    }
}
