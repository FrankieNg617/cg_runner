using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharaterScriptable", menuName = "cg_runner/Character Dictionary", order = 0)]
public class CharaterScriptable : ScriptableObject
{
    [Header("List of character")]
    [SerializeField] List<CharacterData> Characters;

    [System.Serializable]
    public struct CharacterData
    {
        public string id;
        public GameObject prefab;
        public RenderTexture textureIcon;
    }

    public List<CharacterData> GetAllCharactersData()
    {
        return Characters;
    }

    public CharacterData GetCharacterPrefabByID(string id)
    {
        foreach (var data in Characters)
        {
            if (data.id == id)
            {
                return data;
            }
        }
        return Characters[0];
    }
}