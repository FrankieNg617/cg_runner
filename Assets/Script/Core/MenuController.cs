using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;

public class MenuController : MonoBehaviourPunCallbacks
{
    //  REF
    [SerializeField] Button createRoomBtn;
    [SerializeField] Button joinRoomBtn;
    [SerializeField] Button startGameBtn;
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject selectCharacterScreen;
    [SerializeField] GameObject lobbyScreen;

    [SerializeField] CharacterSelectController selectController;

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
        ChangeScreen(selectCharacterScreen);
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

    public void SelectCharacter()
    {
        ChangeScreen(lobbyScreen);

        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.IsLocal)
            {
                if (!player.CustomProperties.ContainsKey("characterID"))
                {
                    player.CustomProperties.Add("characterID", selectController.curPreviewCharacterID);
                }
                player.CustomProperties["characterID"] = selectController.curPreviewCharacterID;
            }
        }

        this.photonView.RPC("UpdateLobby", RpcTarget.All);
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
        FindObjectOfType<NetworkManager>().photonView.RPC("LoadLevel", RpcTarget.AllViaServer, "MPGameScene");
    }
    #endregion

    public void ChangeScreen(GameObject screen)
    {
        mainScreen.SetActive(false);
        selectCharacterScreen.SetActive(false);
        lobbyScreen.SetActive(false);

        screen.SetActive(true);
    }

    [PunRPC]
    public void UpdateLobby()
    {
        LobbyUI lobby = lobbyScreen.GetComponent<LobbyUI>();
        lobby.Init();
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            Player player = PhotonNetwork.PlayerList[i];

            if (player.CustomProperties.ContainsKey("characterID") && player.IsLocal)
            {
                object[] argv = { player.ActorNumber - 1, player.NickName };
                lobby.photonView.RPC("AddPlayer", RpcTarget.AllBuffered, argv);

                lobby.SetCharacter(i, (string)player.CustomProperties["characterID"]);
                return;
            }
        }
    }
}
