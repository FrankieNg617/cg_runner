using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManage : MonoBehaviourPunCallbacks
{
    public GameObject gameOverPanel;
    private AudioManage audioManage;

    public bool isMultiplayer = false;

    [SerializeField] TextMeshProUGUI countDownTxt;
    [SerializeField] int countDownSeconds = 5;

    public event Action onGameStart;
    public event Action onGameOver;

    public static int numberOfCoins;
    public Text score;
    public Text finalScore;
    public Text highScore;

    public float magnetDuration;
    public GameObject coinDetector;

    private void Awake()
    {
        audioManage = FindObjectOfType<AudioManage>();
        numberOfCoins = 0;
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
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
        onGameOver();
        gameOverPanel.SetActive(true);

        finalScore.text = numberOfCoins.ToString();
        if (numberOfCoins > PlayerPrefs.GetInt("HighScore", 0))
        {
            //Store the highest score into the system 
            PlayerPrefs.SetInt("HighScore", numberOfCoins);
            highScore.text = numberOfCoins.ToString();
        }
    }

    public void onMagnet()
    {
        coinDetector.SetActive(true);
        StartCoroutine(magnetTimer(magnetDuration));
    }

    IEnumerator magnetTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        coinDetector.SetActive(false);
    }

    void Update()
    {
        score.text = numberOfCoins.ToString();
    }
}
