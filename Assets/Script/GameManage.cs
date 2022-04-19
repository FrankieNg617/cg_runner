using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class GameManage : MonoBehaviourPunCallbacks
{
    public GameObject gameOverPanel;
    private AudioManage audioManage;

    [SerializeField] bool isMultiplayer = false;

    [SerializeField] TextMeshProUGUI countDownTxt;
    [SerializeField] int countDownSeconds = 5;

    public event Action onGameStart;
    public event Action onGameOver;

    private void Awake()
    {
        audioManage = FindObjectOfType<AudioManage>();
    }

    private void Start()
    {
        if (!isMultiplayer) StartCountDown();
    }

    [PunRPC]
    private void StartCountDown()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        int curSec = countDownSeconds;
        countDownTxt.gameObject.SetActive(true);
        countDownTxt.text = curSec.ToString();
        while (curSec > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            curSec--;
            countDownTxt.text = curSec.ToString();
        }
        countDownTxt.gameObject.SetActive(false);
        audioManage.PlaySound("MainTheme");
        if (onGameStart != null) onGameStart();
    }

    public void EndGame()
    {
        audioManage.StopSound("MainTheme");
        audioManage.PlaySound("GameOverTheme");
        audioManage.PlaySound("GameOverVoice");
        if (onGameOver != null) onGameOver();
        gameOverPanel.SetActive(true);
    }
}
