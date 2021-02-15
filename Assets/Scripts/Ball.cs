using Mirror;
using UnityEngine;

public class Ball : NetworkBehaviour
{
    public float speed = 30; // velocidade pr� definida para a bola (mas pode ser alterada no editor Unity)
    public Rigidbody2D rigidbody2d; // F�sica da bola
    public Vector3 startPosition; // Posi��o inicial da bola
    public float startAngularVelocity; // Velocidade angular inicial da bola

    // Start � chamado antes da primeira atualiza��o de frame
    void Start()
    {
        startPosition = transform.position; // startPosition recebe a posi��o inicial da bola
        startAngularVelocity = rigidbody2d.angularVelocity; // startAngularVelocity recebe a velocidade angular inicial da bola
    }

    // Reseta a posi��o da bola assim que um ponto � feito e a joga novamente
    public void Reset()
    {
        rigidbody2d.velocity = Vector2.zero; // Cancela a velocidade linear (unidades por segundo) da f�sica da bola para anular movimenta��es anteriores
        rigidbody2d.angularVelocity = startAngularVelocity; // Cancela a velocidade angular (graus por segundo) da f�sica da bola para anular movimenta��es anteriores
        transform.position = startPosition; // Retorna a bola a sua posi��o inicial (no centro da tela)
        Launch(); // Lan�a novamente a bola
    }

    // Este m�todo � acionado assim que o GameObject "Ball" � instanciado no script "NetworkManagerPong.cs"
    public override void OnStartServer()
    {
        base.OnStartServer();

        // A f�sica da bola � simulada somente no servidor
        rigidbody2d.simulated = true;

        Launch(); // Lan�a a bola
    }

    // M�todo que lan�a a bola randomicamente
    private void Launch()
    {
        // sorteia X e Y randomicamente, caso o valor rand�mico seja igual a 0, ent�o recebe -1, caso n�o recebe 1.
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        // � aplicada uma velocidade a bola, usando as dire��es de x e y
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

    // apenas chame este m�todo no servidor
    [ServerCallback]
    void OnCollisionEnter2D(Collision2D col)
    {
        // Nota: 'col' cont�m as informa��es de colis�o.
        // Se a bola (Ball) colidiu com uma raquete, ent�o:
        // col.gameObject � a raquete
        // col.transform.position � a posi��o da raquete
        // col.collider � o colisor da raquete

        // bateu em uma raquete? ent�o � necess�rio calcular o fator de acerto
        if (col.transform.GetComponent<Player>())
        {
            // Calcula a dire��o y via fator de acerto
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calcula a dire��o x via colis�o oposta
            float x = col.relativeVelocity.x > 0 ? 1 : -1;

            // Calcule a dire��o, fa�a comprimento = 1 via .normalized
            Vector2 dir = new Vector2(x, y).normalized;

            // Definir velocidade com dir * speed
            rigidbody2d.velocity = dir * speed;
        }
    }
}
