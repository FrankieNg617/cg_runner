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
    public AudioClip compressClip, uncompressClip;
    public AudioSource source;

    public void OnPointerDown(PointerEventData eventData)
    {
        img.sprite = pressed;
        source.PlayOneShot(compressClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.sprite = origin;
        source.PlayOneShot(uncompressClip);
        StartCoroutine(delayOnPlayButtonClick());
    }

    public IEnumerator delayOnPlayButtonClick()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("GameScene");
    }


}
