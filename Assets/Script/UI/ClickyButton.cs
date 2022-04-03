using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image img;
    public Sprite origin, pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        img.sprite = pressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.sprite = origin;
        StartCoroutine(delayOnPlayButtonClick());
    }

    public IEnumerator delayOnPlayButtonClick()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("GameScene");
    }


}
