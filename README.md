# Uma versão de rede local do jogo Pong Multiplayer

# Passos para criação do Jogo:

## Utilizei a engine Unity 2020.1.16f1;
## Importei para o projeto a API Mirror (Asset Store) para gerenciar a rede local;
## Utilizei o projeto base para o Pong contido na API Mirror;
## Criei a scene “Main” (título inicial) e a scene “Pong” (jogo);
## Foi adicionada na scene “Main” uma iluminação do tipo “Point”, para criar foco no título do jogo;
## O tamanho (atributo Size) da câmera com projeção ortográfica da scene “Pong” foi calculada de acordo com o recomendado em https://producaodejogos.com/camera-ortografica-no-unity/, no entanto o resultado recomendado de Size (para mostrar somente a mesa de jogo) foi multiplicado por 2 propositalmente para enquadrar o dobro do tamanho da mesa;
## Usei TextMesh Pro em vez de Text nos textos, pois seu uso é melhor recomendado em https://blogs.unity3d.com/2018/10/16/making-the-most-of-textmesh-pro-in-unity-2018/;
## A bola é jogada randomicamente  de seu ponto de origem (centro da tela) para a direita(cima ou baixo) ou esquerda(cima ou baixo) ;
## Os scripts estão todos comentados, sendo os principais NetworkManagerPong, GameManager, Ball, Player, WallBehindPlayer1 e WallBehindPlayer2.
