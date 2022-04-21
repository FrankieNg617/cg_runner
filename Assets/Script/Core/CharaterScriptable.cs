using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharaterScriptable", menuName = "cg_runner/Character Dictionary", order = 0)]
public class CharaterScriptable : ScriptableObject
{
    [SerializeField] RawImage baseIcon;

    [Header("List of character")]
    [SerializeField] List<CharacterData> Characters;

    [System.Serializable]
    public struct CharacterData
    {
        public string id;
        public GameObject characterPrefab;
        public RenderTexture textureIcon;
    }
}