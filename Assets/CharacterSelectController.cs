using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectController : MonoBehaviour
{
    //  REF
    [Header("Reference")]
    [SerializeField] CharaterScriptable characterDictionary;
    [SerializeField] Icon baseIconPrefab;
    [SerializeField] GameObject iconParent;

    [Header("Preview Area")]
    [SerializeField] GameObject previewCharacterParent;
    [SerializeField] TextMeshProUGUI nameField;

    //  STATE
    GameObject curPreviewCharacter;
    string curPreviewCharacterID;

    private void Start()
    {
        InstantiateIcons();
    }

    private void InstantiateIcons()
    {
        foreach (var characterData in characterDictionary.GetAllCharactersData())
        {
            Icon icon = Instantiate(baseIconPrefab, iconParent.transform.position, Quaternion.identity, iconParent.transform);
            icon.Init(characterData.id, characterData.textureIcon);
            icon.onCharacterSelected.AddListener(selectCharacter);
        }
    }

    private void selectCharacter(string id)
    {
        if (curPreviewCharacter != null) Destroy(curPreviewCharacter);
        var characterData = characterDictionary.GetCharacterPrefabByID(id);
        curPreviewCharacter = Instantiate(characterData.prefab, previewCharacterParent.transform.position, Quaternion.Euler(0, 180, 0), previewCharacterParent.transform);
        curPreviewCharacter.GetComponentInChildren<Animator>().SetTrigger("Display");
        nameField.text = characterData.prefab.name;
    }
}
