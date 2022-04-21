using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyCharacterUi : MonoBehaviour
{
    [SerializeField] CharaterScriptable characterDictionary;

    [SerializeField] GameObject previewCharacterParent;

    [SerializeField] TextMeshProUGUI nameField;


    private GameObject curCharacter;

    public void SetName(string name)
    {
        nameField.text = name;
    }

    public string GetName()
    {
        return nameField.text;
    }

    public void SetCharacter(string id)
    {
        if (curCharacter != null) PhotonNetwork.Destroy(curCharacter);
        var characterData = characterDictionary.GetCharacterPrefabByID(id);
        curCharacter = PhotonNetwork.Instantiate(characterData.prefab.name, previewCharacterParent.transform.position, Quaternion.Euler(0, 180, 0));
        curCharacter.transform.parent = previewCharacterParent.transform;
    }
}
