using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        var instances = FindObjectsOfType<NetworkManager>();
        foreach (var instance in instances)
        {
            if (instance != this)
            {
                Destroy(instance.gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom(string roomName)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

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
