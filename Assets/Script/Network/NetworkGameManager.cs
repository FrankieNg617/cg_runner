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
    public List<GameObject> playersInGame;
    int playersCntInGame = 0;

    private void Awake()
    {
        gameManage = FindObjectOfType<GameManage>();
        playersInGame = new List<GameObject>();
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
        string characterID = PhotonNetwork.LocalPlayer.CustomProperties["characterID"] as string;
        CharaterScriptable characterDictionary = Resources.Load<CharaterScriptable>("CharacterDictionary");
        var characterPrefab = characterDictionary.GetCharacterPrefabByID(characterID).prefab;

        GameObject player = PhotonNetwork.Instantiate(characterPrefab.name, Vector3.zero, Quaternion.identity);
        playersInGame.Add(player);
        player.GetComponent<NetworkCharacterControl>().photonView.RPC("Init", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
