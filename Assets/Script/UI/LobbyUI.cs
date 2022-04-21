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

    public void Init()
    {
        roomNameField.text = PhotonNetwork.CurrentRoom != null ? PhotonNetwork.CurrentRoom.Name : "";
    }

    [PunRPC]
    public void AddPlayer(int position, string playerName)
    {
        charactersUI[position].SetName(playerName);
    }

    public void SetCharacter(int position, string id)
    {
        charactersUI[position].SetCharacter(id);
    }
}
