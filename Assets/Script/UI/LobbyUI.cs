using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyUI : MonoBehaviour
{
    //  TMP: Only used character names
    //  TODO: Change entire texture character
    [SerializeField] List<GameObject> characters;

    public void Init()
    {
        foreach (var character in characters)
        {
            character.SetActive(false);
        }
    }

    public void AddPlayer(string playerName)
    {
        foreach (var character in characters)
        {
            if (!character.activeInHierarchy)
            {
                character.GetComponentInChildren<TextMeshProUGUI>().text = playerName;
                character.SetActive(true);
                break;
            }
        }
    }
}
