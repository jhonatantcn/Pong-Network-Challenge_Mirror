using UnityEngine;
using Mirror;

    public class Ball : NetworkBehaviour
    {
        public float speed = 30;
        public Rigidbody2D rigidbody2d;
        public Vector3 startPosition;
        public float startAngularVelocity;

        void Start()
        {
            startPosition = transform.position;
            startAngularVelocity = rigidbody2d.angularVelocity;
        }

        public void Reset()
        {
            rigidbody2d.velocity = Vector2.zero; // Cancela a velocidade linear (unidades por segundo) da f�sica da bola para anular movimenta��es anteriores
            rigidbody2d.angularVelocity = startAngularVelocity; // Cancela a velocidade angular (graus por segundo) da f�sica da bola para anular movimenta��es anteriores
            transform.position = startPosition; // Retorna a bola a sua posi��o inicial (no centro da tela)
            Launch(); // Lan�a novamente a bola
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            // only simulate ball physics on server
            rigidbody2d.simulated = true;

            Launch(); // Lan�a a bola
        }

        // Fn��o que lan�a a bola randomicamente
        private void Launch()
        {
            // sorte X e Y randomicamente, caso o valor rand�mico seja igual a 0, ent�o recebe -1, caso n�o recebe 1.
            float x = Random.Range(0, 2) == 0 ? -1 : 1;
            float y = Random.Range(0, 2) == 0 ? -1 : 1;
            rigidbody2d.velocity = new Vector2(speed * x, speed * y);
        }

        float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
        {
            // ascii art:
            // ||  1 <- at the top of the racket
            // ||
            // ||  0 <- at the middle of the racket
            // ||
            // || -1 <- at the bottom of the racket
            return (ballPos.y - racketPos.y) / racketHeight;
        }

        // only call this on server
        [ServerCallback]
        void OnCollisionEnter2D(Collision2D col)
        {
            // Note: 'col' holds the collision information. If the
            // Ball collided with a racket, then:
            //   col.gameObject is the racket
            //   col.transform.position is the racket's position
            //   col.collider is the racket's collider

            // did we hit a racket? then we need to calculate the hit factor
            if (col.transform.GetComponent<Player>())
            {
                // Calculate y direction via hit Factor
                float y = HitFactor(transform.position,
                                    col.transform.position,
                                    col.collider.bounds.size.y);

                // Calculate x direction via opposite collision
                float x = col.relativeVelocity.x > 0 ? 1 : -1;

                // Calculate direction, make length=1 via .normalized
                Vector2 dir = new Vector2(x, y).normalized;

                // Set Velocity with dir * speed
                rigidbody2d.velocity = dir * speed;
            }
        }
    }
