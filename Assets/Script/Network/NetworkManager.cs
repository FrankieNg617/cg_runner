using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void LoadLevel(string levelName)
    {
        PhotonNetwork.LoadLevel(levelName);
    }

    #region Network Callback
    public override void OnConnectedToMaster()
    {
        Debug.Log("NetworkManager: Connected to master server");
        CreateRoom("testroom");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("NetworkManager: Created a new room called " + PhotonNetwork.CurrentRoom.Name);
    }
    #endregion
}
