using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkCharacterControl : MonoBehaviourPunCallbacks
{
    public int id;
    private Player photonPlayer;

    [PunRPC]
    public void Init(Player player)
    {
        photonPlayer = player;
        id = player.ActorNumber;
        if (!photonView.IsMine)
        {
            GetComponent<CharacterControl>().enabled = false;
        }
        else
        {
            gameObject.tag = "Player";
        }
    }
}
