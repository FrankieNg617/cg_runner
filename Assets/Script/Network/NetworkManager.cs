using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {
    }

    private void Start()
    {
        print("Connect using setting");
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom(string roomName)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 10;

        PhotonNetwork.CreateRoom(roomName, options);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    [PunRPC]
    public void LoadLevel(string levelName)
    {
        PhotonNetwork.LoadLevel(levelName);
    }

    #region Network Callback
    public override void OnConnectedToMaster()
    {
        Debug.Log("NetworkManager: Connected to master server");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("NetworkManager: Created a new room called " + PhotonNetwork.CurrentRoom.Name);
    }
    #endregion
}
