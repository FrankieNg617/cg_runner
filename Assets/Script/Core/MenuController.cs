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

    NetworkManager networkManager;

    private void Awake()
    {
        networkManager = FindObjectOfType<NetworkManager>();
    }

    private void Start()
    {
        createRoomBtn.interactable = false;
        joinRoomBtn.interactable = false;

        if (!PhotonNetwork.IsMasterClient)
            startGameBtn.gameObject.SetActive(false);
    }

    #region PunCallbacks
    public override void OnConnectedToMaster()
    {
        createRoomBtn.interactable = true;
        joinRoomBtn.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        ChangeScreen(lobbyScreen);
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
        networkManager.CreateRoom(inputField.text);
    }
    public void JoinRoom(TMP_InputField inputField)
    {
        networkManager.JoinRoom(inputField.text);
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
        networkManager.photonView.RPC("LoadLevel", RpcTarget.AllViaServer, "MPGameScene");
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
