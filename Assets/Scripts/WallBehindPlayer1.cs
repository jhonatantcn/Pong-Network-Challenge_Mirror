using Mirror;
using TMPro;
using UnityEngine;

public class WallBehindPlayer1 : NetworkBehaviour
{
    [SyncVar] public int scoreP1 = 0;

    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.gameObject.CompareTag("Ball"))
        {
            // Player 1 Scored...
            //this.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
            CmdServerScored();
            GameObject.Find("GameManager").GetComponent<GameManager>().ResetPosition();
        }
    }

    //[Command]
    void CmdServerScored() // CmdAjoutPoint
    {
        //Debug.Log("Teste 11...");
        //if (!isLocalPlayer)
        //    return;

        scoreP1 += 1;
        RpcScoreUpdate(scoreP1);
    }

    [ClientRpc]
    public void RpcScoreUpdate(int score) //RpcMajScore
    {
        //Debug.Log("Teste 12...");
        GameObject.FindGameObjectWithTag("ScorePlayer1").GetComponent<TextMeshProUGUI>().text = "" + score;
    }
}
