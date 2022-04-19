using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class NetworkGameManager : MonoBehaviourPunCallbacks
{
    //REF
    GameManage gameManage;

    //STATE
    int playersCntInGame = 0;

    private void Awake()
    {
        gameManage = FindObjectOfType<GameManage>();
    }


    private void Start()
    {
        photonView.RPC("OnJoinedGame", RpcTarget.AllBuffered);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    private void OnJoinedGame()
    {
        playersCntInGame++;
        if (playersCntInGame == PhotonNetwork.PlayerList.Length)
        {
            gameManage.photonView.RPC("StartCountDown", RpcTarget.AllViaServer);
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        GameObject player = PhotonNetwork.Instantiate("NetworkCharacter", Vector3.zero, Quaternion.identity);

        player.GetComponent<NetworkCharacterControl>().photonView.RPC("Init", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
