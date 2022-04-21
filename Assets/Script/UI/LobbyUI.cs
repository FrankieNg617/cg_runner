using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LobbyUI : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TextMeshProUGUI roomNameField;

    [SerializeField] List<LobbyCharacterUi> charactersUI;
    [SerializeField] TextMeshProUGUI noPlayerField;

    public void Init()
    {
        roomNameField.text = PhotonNetwork.CurrentRoom != null ? PhotonNetwork.CurrentRoom.Name : "";
    }

    [PunRPC]
    public void AddPlayer(int position, string playerName)
    {
        if (position <= charactersUI.Count)
        {
            charactersUI[position].SetName(playerName);
        }
        noPlayerField.text = PhotonNetwork.PlayerList.Length + " player!";
    }

    public void SetCharacter(int position, string id)
    {
        charactersUI[position].SetCharacter(id);
    }
}
