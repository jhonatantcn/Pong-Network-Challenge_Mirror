using Mirror;
using TMPro;
using UnityEngine;

public class WallBehindPlayer2 : NetworkBehaviour
{
    [SyncVar] public int scoreP2 = 0;

    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.gameObject.CompareTag("Ball"))
        {
            // Player 2 Scored...
            //this.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
            CmdServerScored();
            GameObject.Find("GameManager").GetComponent<GameManager>().ResetPosition();
        }
    }

    //[Command]
    void CmdServerScored() // CmdAjoutPoint
    {
        // Debug.Log("Teste 21...");
        //if (!isLocalPlayer)
        //    return;

        scoreP2 += 1;
        RpcScoreUpdate(scoreP2);
    }

    [ClientRpc]
    public void RpcScoreUpdate(int score) //RpcMajScore
    {
        // Debug.Log("Teste 22...");
        GameObject.FindGameObjectWithTag("ScorePlayer2").GetComponent<TextMeshProUGUI>().text = "" + score;
    }
}

