using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Icon : EventTrigger
{
    string characterID;

    //Event
    public UnityEvent<string> onCharacterSelected;

    public void Init(string id, RenderTexture characterIconTexture)
    {
        this.characterID = id;
        GetComponent<RawImage>().texture = characterIconTexture;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        onCharacterSelected.Invoke(characterID);
    }

    private void OnDestroy()
    {
        onCharacterSelected.RemoveAllListeners();
    }
}
