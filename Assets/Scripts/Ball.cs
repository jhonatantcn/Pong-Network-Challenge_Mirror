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
            rigidbody2d.velocity = Vector2.zero; // Cancela a velocidade linear (unidades por segundo) da física da bola para anular movimentações anteriores
            rigidbody2d.angularVelocity = startAngularVelocity; // Cancela a velocidade angular (graus por segundo) da física da bola para anular movimentações anteriores
            transform.position = startPosition; // Retorna a bola a sua posição inicial (no centro da tela)
            Launch(); // Lança novamente a bola
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            // only simulate ball physics on server
            rigidbody2d.simulated = true;

            Launch(); // Lança a bola
        }

        // Fnção que lança a bola randomicamente
        private void Launch()
        {
            // sorte X e Y randomicamente, caso o valor randômico seja igual a 0, então recebe -1, caso não recebe 1.
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
