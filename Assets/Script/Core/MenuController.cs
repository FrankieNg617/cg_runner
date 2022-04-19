using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MenuController : MonoBehaviourPunCallbacks
{
    //  REF
    [SerializeField] Button createRoomBtn;

    [SerializeField] Button joinRoomBtn;
    [SerializeField] Button startGameBtn;
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject lobbyScreen;


    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            createRoomBtn.interactable = false;
            joinRoomBtn.interactable = false;
        }
    }

    #region PunCallbacks
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        createRoomBtn.interactable = true;
        joinRoomBtn.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        ChangeScreen(lobbyScreen);
        if (!PhotonNetwork.IsMasterClient)
            startGameBtn.gameObject.SetActive(false);

        this.photonView.RPC("UpdateLobby", RpcTarget.All);
    }

    public override void OnLeftRoom()
    {
        UpdateLobby();
    }
    #endregion

    #region UICallbacks
    public void CreateRoom(TMP_InputField inputField)
    {
        NetworkManager.instance.CreateRoom(inputField.text);
    }
    public void JoinRoom(TMP_InputField inputField)
    {
        NetworkManager.instance.JoinRoom(inputField.text);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(false);
        ChangeScreen(mainScreen);
    }

    public void UpdatePlayerName(TMP_InputField inputField)
    {
        PhotonNetwork.NickName = inputField.text;
    }

    public void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        print(NetworkManager.instance);
        NetworkManager.instance.photonView.RPC("LoadLevel", RpcTarget.AllViaServer, "MPGameScene");
    }
    #endregion

    public void ChangeScreen(GameObject screen)
    {
        mainScreen.SetActive(false);
        lobbyScreen.SetActive(false);

        screen.SetActive(true);
    }

    [PunRPC]
    public void UpdateLobby()
    {
        LobbyUI lobby = lobbyScreen.GetComponent<LobbyUI>();
        lobby.Init();
        foreach (var player in PhotonNetwork.PlayerList)
        {
            lobby.AddPlayer(player.NickName);
        }
    }
}
